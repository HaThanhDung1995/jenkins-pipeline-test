using Microsoft.EntityFrameworkCore;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPhamTrueLife.BLL.Ultil;

namespace MyPhamTrueLife.BLL.Implement
{
    public class ProductService : IProductService
    {
        public readonly dbDevNewContext _unitOfWork;
        public ProductService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ListProductPortfolioUser>> ListProductPortfolioUserAsync()
        {
            var listItem = new List<ListProductPortfolioUser>();
            var portfolio = await _unitOfWork.Repository<InfoProductPortfolio>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            if (portfolio != null)
            {
                foreach (var it in portfolio)
                {
                    var item = new ListProductPortfolioUser();
                    item.PortfolioName = it.ProductPortfolioName;
                    item.listTypeProductUsers = new List<ListTypeProductUser>();
                    var typeProduct = await _unitOfWork.Repository<InfoTypeProduct>().Where(x => x.DeleteFlag != true && x.ProductPortfolioId.Equals(it.ProductPortfolioId)).AsNoTracking().ToListAsync();
                    if (typeProduct != null)
                    {
                        foreach (var i in typeProduct)
                        {
                            var type = new ListTypeProductUser();
                            type.TypeProductId = i.TypeProductId;
                            type.TypeProductName = i.TypePeoductName;
                            item.listTypeProductUsers.Add(type);
                        }
                    }
                    listItem.Add(item);
                }
            }
            return listItem;
        }

        public async Task<ResponseList> TopSixSellingProducts()
        {
            var listData = new ResponseList();
            listData.ListData = null;

            var listProductSeling = new List<ListProductSelling>();
            var listProduct = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            if (listProduct != null)
            {
                var order = await _unitOfWork.Repository<InfoOrder>().Where(x => x.DeleteFlag != true).AsNoTracking().CountAsync();
                if (order <= 0)
                {
                    listProduct = listProduct.OrderByDescending(x => x.CreateAt).Take(6).ToList();
                    foreach (var item in listProduct)
                    {
                        var product = new ListProductSelling();
                        var listPrice = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(item.ProductId)).AsNoTracking().OrderByDescending(x => x.StartAt).ToListAsync();
                        product.Price = listPrice[0].Price.Value;
                        product.ProductName = item.ProductName;
                        product.ProductId = item.ProductId;
                        product.Avatar = item.Avatar;
                        listProductSeling.Add(product);
                    }
                }
                else
                {
                    var listCountProduct = new List<CountProduct>();
                    foreach (var ite in listProduct)
                    {
                        var inf = new CountProduct();
                        inf.ProductId = ite.ProductId;
                        inf.Quantity = (int)await _unitOfWork.Repository<InfoOrderDetail>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(ite.ProductId)).AsNoTracking().SumAsync(z => z.Amount);
                    }
                    listCountProduct = listCountProduct.OrderByDescending(x => x.Quantity).Take(6).ToList();
                    foreach (var item in listCountProduct)
                    {
                        var product = new ListProductSelling();
                        var pro = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(item.ProductId)).AsNoTracking().FirstOrDefaultAsync();
                        if (pro != null)
                        {
                            var listPrice = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(item.ProductId)).AsNoTracking().OrderByDescending(x => x.StartAt).ToListAsync();
                            product.Price = listPrice[0].Price.Value;
                            product.ProductName = pro.ProductName;
                            product.ProductId = pro.ProductId;
                            product.Avatar = pro.Avatar;

                            listProductSeling.Add(product);
                        }
                    }
                }
            }
            listData.ListData = listProductSeling;
            return listData;
        }

        public async Task<ResponseList> TopSixNewProducts()
        {
            var listData = new ResponseList();
            listData.ListData = null;

            var listProductSeling = new List<ListProductSelling>();
            var listProduct = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            if (listProduct != null)
            {
                listProduct = listProduct.OrderByDescending(x => x.CreateAt).Take(6).ToList();
                foreach (var item in listProduct)
                {
                    var product = new ListProductSelling();
                    var listPrice = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(item.ProductId)).AsNoTracking().OrderByDescending(x => x.StartAt).ToListAsync();
                    product.Price = listPrice[0].Price.Value;
                    product.ProductName = item.ProductName;
                    product.ProductId = item.ProductId;
                    product.Avatar = item.Avatar;

                    listProductSeling.Add(product);
                }
            }

            listData.ListData = listProductSeling;
            return listData;
        }

        public async Task<ResponseList> TopSixProductPromotion()
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listProductPromotions = new List<ListProductPromotion>();

            var listPromotion = await _unitOfWork.Repository<InfoPromotion>().Where(x => x.DeleteFlag != true && x.StartAt <= DateTime.Now && x.EndAt >= DateTime.Now).AsNoTracking().ToListAsync();
            if (listPromotion == null || listPromotion.Count <= 0)
            {
                return listData;
            }
            foreach (var item in listPromotion)
            {
                var listPromotionDetail = await _unitOfWork.Repository<InfoPromotionDetail>().Where(x => x.DeleteFlag != true && x.PromotionId.Equals(item.PromotionId)).AsNoTracking().ToListAsync();
                foreach (var ite in listPromotionDetail)
                {
                    var product = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(ite.ProductId)).AsNoTracking().FirstOrDefaultAsync();
                    var productPromotion = new ListProductPromotion();
                    var listPrice = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(ite.ProductId)).AsNoTracking().OrderByDescending(x => x.StartAt).ToListAsync();
                    listPrice = listPrice.OrderByDescending(x => x.Price).ToList();
                    productPromotion.Price = listPrice[0].Price.Value;
                    if (ite.DiscountAmount != null && ite.DiscountAmount > 0)
                    {
                        int? price = listPrice[0].Price - ite.DiscountAmount;
                        productPromotion.PriceDiscount = price.Value;
                    }
                    if (ite.DiscountPercent != null && ite.DiscountPercent > 0)
                    {
                        int? price = listPrice[0].Price - ((listPrice[0].Price * ite.DiscountPercent) / 100);
                        productPromotion.PriceDiscount = price.Value;
                    }
                    productPromotion.ProductName = product.ProductName;
                    productPromotion.ProductId = product.ProductId;
                    productPromotion.Avatar = product.Avatar;

                    listProductPromotions.Add(productPromotion);
                }
            }
            listData.ListData = listProductPromotions.Take(6);
            return listData;
        }

        public async Task<ResponseList> ShowListProductFilter(ProductFilterRequest value, int page = 1, int limit = 25)
        {
            var result = new ResponseList();
            var listProductSeling = new List<ListProductSelling>();
            var listProduct = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            if (listProduct != null)
            {
                //Lấy danh sách sản phẩm theo tên sản phẩm
                if (!string.IsNullOrEmpty(value.ProductName))
                {
                    listProduct = listProduct.Where(x => x.ProductName.Contains(value.ProductName)).ToList();
                }
                //Lấy danh sách sản phẩm theo giá sản phẩm
                //Lấy danh sách sản phẩm theo loại sản phẩm
                if (value.ProductTypeId > 0)
                {
                    listProduct = listProduct.Where(x => x.TypeProductId.Equals(value.ProductTypeId)).ToList();
                }
                if (listProduct != null && listProduct.Count > 0)
                {
                    foreach (var item in listProduct)
                    {
                        var product = new ListProductSelling();
                        var listPrice = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(item.ProductId)).AsNoTracking().OrderByDescending(x => x.StartAt).ToListAsync();
                        if (listPrice != null || listPrice.Count > 0)
                        {
                            product.Price = listPrice[0].Price.Value;
                            product.ProductName = item.ProductName;
                            product.ProductId = item.ProductId;
                            product.Avatar = item.Avatar;
                            product.PriceDiscount = product.Price;
                            listProductSeling.Add(product);
                        }
                    }

                    var totalRows = listProductSeling.Count();
                    result.Paging = new Paging(totalRows, page, limit);
                    int start = result.Paging.start;
                    listProductSeling = listProductSeling.Skip(start).Take(limit).ToList();
                }
            }
            result.ListData = listProductSeling;
            return result;
        }

        public async Task<List<ListProductSelling>> ShowListProductFilterSupplier(int supplierId)
        {
            var result = new ResponseList();
            var listProductSeling = new List<ListProductSelling>();
            var supplier = await _unitOfWork.Repository<InfoSupplier>().Where(x => x.DeleteFlag != true && x.SupplierId == supplierId).AsNoTracking().FirstOrDefaultAsync();
            if (supplier != null)
            {
                var listProduct = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
                if (listProduct != null)
                {
                    if (supplier.TypeProductId > 0)
                    {
                        listProduct = listProduct.Where(x => x.TypeProductId.Equals(supplier.TypeProductId)).ToList();
                    }
                    if (listProduct != null && listProduct.Count > 0)
                    {
                        foreach (var item in listProduct)
                        {
                            var product = new ListProductSelling();
                            var listPrice = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(item.ProductId)).AsNoTracking().OrderByDescending(x => x.StartAt).ToListAsync();
                            if (listPrice != null || listPrice.Count > 0)
                            {
                                product.Price = listPrice[0].Price.Value;
                                product.ProductName = item.ProductName;
                                product.ProductId = item.ProductId;
                                product.Avatar = item.Avatar;
                                product.PriceDiscount = product.Price;
                                listProductSeling.Add(product);
                            }
                        }
                    }
                }
            }
            result.ListData = listProductSeling;
            return listProductSeling;
        }

        public async Task<ProductDetail> ProductDetailAsync(int id, int? nature, int? capacity)
        {
            if (id <= 0)
            {
                return null;
            }
            var info = new ProductDetail();
            info.listCapacity = new List<InfoCapacity>();
            var product = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
            if (product == null)
            {
                return null;
            }
            info.ProductId = product.ProductId;
            info.ProductName = product.ProductName;
            info.Trademark = product.Trademark;
            info.StatusProduct = product.StatusProduct;
            info.NatureId = product.NatureId;
            if (product.NatureId != null)
            {

                info.NatureName = await _unitOfWork.Repository<InfoNature>().Where(x => x.DeleteFlag != true && x.NatureId == product.NatureId).AsNoTracking().Select(z => z.NatureName).FirstOrDefaultAsync();
                info.ProductName += " - " + info.NatureName;
            }
            info.Describe = product.Describe;
            info.listImage = new List<string>();
            info.listImage.Add(product.Avatar);
            var images = await _unitOfWork.Repository<InfoImageProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(product.ProductId)).AsNoTracking().ToListAsync();
            if (images != null && images.Count > 0)
            {
                foreach (var item in images)
                {
                    info.listImage.Add(item.Img);
                }
            }
            if (product.IsExpiry != true)
            {
                var pricePro = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(product.ProductId)).AsNoTracking().OrderByDescending(z => z.StartAt).ToListAsync();
                if (pricePro != null && pricePro.Count > 0 && pricePro[0] != null)
                {
                    info.Price = pricePro[0].Price + (int)(pricePro[0].Price / 10);
                    info.PriceDiscount = pricePro[0].Price;
                }
            }
            else
            {
                if (product.IsExpiry == false)
                {
                    var productNatureS = await _unitOfWork.Repository<InfoExpiryProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(product.ProductId) && x.Amount > 0 && x.StartAt == null && x.EndAt == null).AsNoTracking().ToListAsync();
                    foreach (var item in productNatureS)
                    {
                        var capacity1 = await _unitOfWork.Repository<InfoCapacity>().Where(x => x.DeleteFlag != true && x.CapacityId > 0 && x.CapacityId.Equals(item.CapacityId)).AsNoTracking().FirstOrDefaultAsync();
                        if (capacity1 != null)
                        {
                            info.listCapacity.Add(capacity1);
                        }
                    }
                    var priceProNature = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(product.ProductId)).AsNoTracking().OrderByDescending(z => z.StartAt).ToListAsync();
                    if (priceProNature != null && priceProNature.Count > 0 && priceProNature[0] != null)
                    {
                        info.Price = priceProNature[0].Price + (int)(priceProNature[0].Price / 10);
                        info.PriceDiscount = priceProNature[0].Price;
                    }
                }
                if (product.IsExpiry == true)
                {
                    var productNatureS = await _unitOfWork.Repository<InfoExpiryProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(product.ProductId) && x.Amount > 0 && x.StartAt <= DateTime.Now && x.EndAt >= DateTime.Now).AsNoTracking().ToListAsync();
                    foreach (var item in productNatureS)
                    {
                        var capacity1 = await _unitOfWork.Repository<InfoCapacity>().Where(x => x.DeleteFlag != true && x.CapacityId > 0 && x.CapacityId.Equals(item.CapacityId)).AsNoTracking().FirstOrDefaultAsync();
                        if (capacity1 != null)
                        {
                            info.listCapacity.Add(capacity1);
                        }
                    }
                    var priceProNature = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(product.ProductId)).AsNoTracking().OrderByDescending(z => z.StartAt).ToListAsync();
                    if (priceProNature != null && priceProNature.Count > 0 && priceProNature[0] != null)
                    {
                        info.Price = priceProNature[0].Price + (int)(priceProNature[0].Price / 10);
                        info.PriceDiscount = priceProNature[0].Price;
                    }
                }
            }
            //Danh sách sản phẩm liên quan
            var listProduct = await _unitOfWork.Repository<InfoProduct>().Where(x => x.ProductId != product.ProductId && x.DeleteFlag != true && x.TypeProductId.Equals(product.TypeProductId)).AsNoTracking().ToListAsync();
            if (listProduct != null)
            {
                info.listProductRelateTo = new List<ListProductSelling>();
                listProduct = listProduct.OrderByDescending(x => x.CreateAt).ToList();
                foreach (var item in listProduct)
                {
                    var product1 = new ListProductSelling();
                    var listPrice = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(item.ProductId)).AsNoTracking().OrderByDescending(x => x.StartAt).ToListAsync();
                    product1.Price = listPrice[0].Price.Value;
                    product1.ProductName = item.ProductName;
                    product1.ProductId = item.ProductId;
                    product1.Avatar = item.Avatar;
                    info.listProductRelateTo.Add(product1);
                }
            }
            //Danh sách cmt
            var comment = await _unitOfWork.Repository<InfoComent>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(product.ProductId)).AsNoTracking().ToListAsync();
            if (comment != null)
            {
                info.listComent = new List<InfoCommentClient>();
                foreach (var item in comment)
                {
                    var infoNew = new InfoCommentClient();
                    PropertyCopier<InfoComent, InfoCommentClient>.Copy(item, infoNew);
                    var userInfo = await _unitOfWork.Repository<InfoUser>().Where(x => x.DeleteFlag != true && x.UserId == item.UserId).AsNoTracking().FirstOrDefaultAsync();
                    if (userInfo != null)
                    {
                        infoNew.FullName = userInfo.FullName;
                        infoNew.Avatar = userInfo.Avatar;
                        info.listComent.Add(infoNew);
                    }
                }
            }
            //Danh sách đánh giá 
            var evaluate = await _unitOfWork.Repository<InfoEvaluate>().Where(x => x.ProductId.Equals(product.ProductId)).AsNoTracking().ToListAsync();
            if (evaluate != null)
            {
                info.listEvaluate = new List<InfoEvaluateClient>();
                foreach (var item in evaluate)
                {
                    var infoNew = new InfoEvaluateClient();
                    PropertyCopier<InfoEvaluate, InfoEvaluateClient>.Copy(item, infoNew);
                    var userInfo = await _unitOfWork.Repository<InfoUser>().Where(x => x.DeleteFlag != true && x.UserId == item.UserId).AsNoTracking().FirstOrDefaultAsync();
                    if (userInfo != null)
                    {
                        infoNew.FullName = userInfo.FullName;
                        infoNew.Avatar = userInfo.Avatar;
                        info.listEvaluate.Add(infoNew);
                    }
                }
            }
            info.QuantityOrder = await _unitOfWork.Repository<InfoOrderDetail>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(product.ProductId)).AsNoTracking().SumAsync(z => z.Amount);
            if (info.listCapacity.Count <= 0)
            {
                info.listCapacity = null;
            }
            return info;
        }
    }
}
