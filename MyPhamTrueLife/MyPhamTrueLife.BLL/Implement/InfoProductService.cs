using Microsoft.EntityFrameworkCore;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Implement
{
    public class InfoProductService : IInfoProductService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoProductService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> InsertInfoProductAsync(InfoProductInsertLogin value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            else
            {
                var infoProuct = new InfoProduct();
                infoProuct.ProductName = value.ProductName;
                infoProuct.Describe = value.Describe;
                infoProuct.Amount = 0;
                infoProuct.IsExpiry = value.IsExpiry;
                infoProuct.StatusProduct = "HETHANG";
                infoProuct.Trademark = value.Trademark;
                infoProuct.NatureId = value.NatureId;
                infoProuct.IsAroma = value.IsAroma;
                infoProuct.Avatar = value.Avatar;
                infoProuct.TypeProductId = value.TypeProductId;
                infoProuct.CreateUser = userId;
                infoProuct.CreateAt = DateTime.Now;
                infoProuct.DeleteFlag = false;
                var result = await _unitOfWork.Repository<InfoProduct>().AddAsync(infoProuct);
                if (value.ListImage != null)
                {
                    foreach (var image in value.ListImage)
                    {
                        var infoImageProduct = new InfoImageProduct();
                        infoImageProduct.ProductId = result.Entity.ProductId;
                        infoImageProduct.Img = image;
                        infoImageProduct.CreateUser = userId;
                        infoImageProduct.CreateAt = DateTime.Now;
                        infoImageProduct.DeleteFlag = false;
                        await _unitOfWork.Repository<InfoImageProduct>().AddAsync(infoImageProduct);
                    }
                }
                if (value.ListCapacity != null)
                {
                    foreach (var capacityId in value.ListCapacity)
                    {
                        var infoCapacityProduct = new InfoCapacityProduct();
                        infoCapacityProduct.ProductId = result.Entity.ProductId;
                        infoCapacityProduct.CapacityId = capacityId;
                        infoCapacityProduct.CreateUser = userId;
                        infoCapacityProduct.CreateAt = DateTime.Now;
                        infoCapacityProduct.DeleteFlag = false;
                        await _unitOfWork.Repository<InfoCapacityProduct>().AddAsync(infoCapacityProduct);
                    }
                }
            }
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> InsertInfoProductNewAsync(ThongTinThemSanPham value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            var infoProuct = new InfoProduct();
            infoProuct.ProductName = value.ProductName;
            infoProuct.Describe = value.Describe;
            infoProuct.Amount = 0;
            infoProuct.IsExpiry = value.IsExpiry;
            infoProuct.StatusProduct = "HETHANG";
            infoProuct.Trademark = value.Trademark;
            infoProuct.NatureId = value.NatureId;
            infoProuct.IsAroma = value.IsExpiry == true ? true : false;
            infoProuct.Avatar = value.Avatar;
            infoProuct.TypeProductId = value.TypeProductId;
            infoProuct.CreateUser = userId;
            infoProuct.CreateAt = DateTime.Now;
            infoProuct.DeleteFlag = false;
            var result = await _unitOfWork.Repository<InfoProduct>().AddAsync(infoProuct);
            await _unitOfWork.SaveChangesAsync();
            //Thêm hình ảnh
            if (value.ListImage != null)
            {
                foreach (var image in value.ListImage)
                {
                    var infoImageProduct = new InfoImageProduct();
                    infoImageProduct.ProductId = infoProuct.ProductId;
                    infoImageProduct.Img = image;
                    infoImageProduct.CreateUser = userId;
                    infoImageProduct.CreateAt = DateTime.Now;
                    infoImageProduct.DeleteFlag = false;
                    await _unitOfWork.Repository<InfoImageProduct>().AddAsync(infoImageProduct);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            if (value.ListCapacityPrice != null)
            {
                foreach (var item in value.ListCapacityPrice)
                {
                    var infoPriceProduct = new InfoPriceProduct();
                    infoPriceProduct.ProductId = infoProuct.ProductId;
                    infoPriceProduct.Price = item.Price;
                    infoPriceProduct.CreateUser = userId;
                    infoPriceProduct.CreateAt = DateTime.Now;
                    infoPriceProduct.DeleteFlag = false;
                    infoPriceProduct.StartAt = DateTime.Now;
                    infoPriceProduct.CapacityId = item.Cappacity;
                    await _unitOfWork.Repository<InfoPriceProduct>().AddAsync(infoPriceProduct);
                    await _unitOfWork.SaveChangesAsync();

                    var infoCapacityProduct = new InfoCapacityProduct();
                    infoCapacityProduct.ProductId = infoProuct.ProductId;
                    infoCapacityProduct.CapacityId = item.Cappacity;
                    infoCapacityProduct.CreateUser = userId;
                    infoCapacityProduct.CreateAt = DateTime.Now;
                    infoCapacityProduct.DeleteFlag = false;
                    await _unitOfWork.Repository<InfoCapacityProduct>().AddAsync(infoCapacityProduct);
                    await _unitOfWork.SaveChangesAsync();

                    if (value.IsExpiry == true)
                    {
                        var infoExpiry = new InfoExpiryProduct();
                        infoExpiry.ProductId = infoProuct.ProductId;
                        infoExpiry.CapacityId = item.Cappacity;
                        infoExpiry.StartAt = value.StartAt;
                        infoExpiry.EndAt = value.EndAt;
                        infoExpiry.Amount = 0;
                        infoExpiry.CreateUser = userId;
                        infoExpiry.CreateAt = DateTime.Now;
                        infoExpiry.DeleteFlag = false;
                        await _unitOfWork.Repository<InfoExpiryProduct>().AddAsync(infoExpiry);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
            }
            else
            {
                var infoPriceProduct = new InfoPriceProduct();
                infoPriceProduct.ProductId = infoProuct.ProductId;
                infoPriceProduct.Price = value.Price;
                infoPriceProduct.CreateUser = userId;
                infoPriceProduct.CreateAt = DateTime.Now;
                infoPriceProduct.DeleteFlag = false;
                infoPriceProduct.StartAt = DateTime.Now;
                await _unitOfWork.Repository<InfoPriceProduct>().AddAsync(infoPriceProduct);
                await _unitOfWork.SaveChangesAsync();
                if (value.IsExpiry == true)
                {
                    var infoExpiry = new InfoExpiryProduct();
                    infoExpiry.ProductId = infoProuct.ProductId;
                    infoExpiry.StartAt = value.StartAt;
                    infoExpiry.EndAt = value.EndAt;
                    infoExpiry.Amount = 0;
                    infoExpiry.CreateUser = userId;
                    infoExpiry.CreateAt = DateTime.Now;
                    infoExpiry.DeleteFlag = false;
                    await _unitOfWork.Repository<InfoExpiryProduct>().AddAsync(infoExpiry);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<ProductDetailAdmin> ProductDetailAsync(int id)
        {
            var product = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true && x.ProductId == id).AsNoTracking().FirstOrDefaultAsync();
            if (product == null)
            {
                return null;
            }
            var info = new ProductDetailAdmin();
            info.ProductId = product.ProductId;
            info.ProductName = product.ProductName;
            info.Avatar = product.Avatar;
            info.IsExpiry = product.IsExpiry;
            info.PriceDiscount = product.ProductId;
            info.Trademark = product.Trademark;
            info.StatusProduct = product.StatusProduct;
            info.NatureId = product.NatureId;
            info.Describe = product.Describe;
            if (product.NatureId != null)
            {
                info.NatureName = await _unitOfWork.Repository<InfoNature>().Where(x => x.DeleteFlag != true && x.NatureId == product.NatureId).AsNoTracking().Select(z=>z.NatureName).FirstOrDefaultAsync();
            }
            info.listImage = await _unitOfWork.Repository<InfoImageProduct>().Where(x=>x.DeleteFlag != true && x.ProductId == product.ProductId).AsNoTracking().ToListAsync();
            info.listInfoExpiryProducts = await _unitOfWork.Repository<InfoExpiryProduct>().Where(x => x.DeleteFlag != true && x.ProductId == product.ProductId).AsNoTracking().ToListAsync();
            var list = new List<CapacityProductRes>();

            var listCapaCity = await _unitOfWork.Repository<InfoCapacityProduct>().Where(x => x.DeleteFlag != true && x.ProductId == product.ProductId).AsNoTracking().ToListAsync();
            if (listCapaCity != null && list.Count > 0)
            {
                foreach (var item in listCapaCity)
                {
                    var capacity = await _unitOfWork.Repository<InfoCapacity>().Where(x => x.DeleteFlag != true && x.CapacityId == item.CapacityId).AsNoTracking().FirstOrDefaultAsync();
                    if (capacity != null)
                    {
                        var inf = new CapacityProductRes();
                        inf.CapacityId = capacity.CapacityId;
                        inf.CapacityName = capacity.CapacityName;
                        inf.Unit = capacity.Unit;
                        var priceProduct = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.CapacityId == capacity.CapacityId && x.ProductId == product.ProductId).OrderByDescending(z => z.StartAt).AsNoTracking().FirstOrDefaultAsync();
                        if (priceProduct != null)
                        {
                            inf.PriceProductId = priceProduct.PriceProductId;
                            inf.Price = priceProduct.Price;
                        }
                        list.Add(inf);
                    }    
                }
            }
            else
            {
                var priceProduct = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId == product.ProductId).OrderByDescending(z => z.StartAt).AsNoTracking().FirstOrDefaultAsync();

                info.Price = priceProduct != null ? priceProduct.Price : 0;
            }    
            info.listCapacityProductRes = list;
            return info;
        }

        public async Task<bool> UpdateProductAsync(ProductDetailAdmin value, int staffId)
        {
            var product = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true && x.ProductId == value.ProductId).AsNoTracking().FirstOrDefaultAsync();
            if (product == null)
            {
                return false;
            }
            product.ProductName = value.ProductName;
            product.Trademark = value.Trademark;
            product.StatusProduct = value.StatusProduct;
            product.NatureId = value.NatureId;
            product.Describe = value.Describe;
            _unitOfWork.Repository<InfoProduct>().UpdateRange(product);
            await _unitOfWork.SaveChangeAsync();

            //Thêm hình ảnh
            if (value.listImage != null)
            {
                foreach (var image in value.listImage)
                {
                    var img = await _unitOfWork.Repository<InfoImageProduct>().Where(x => x.DeleteFlag != true && x.ImgProductId == image.ImgProductId).AsNoTracking().FirstOrDefaultAsync();
                    if (img != null)
                    {
                        img.Img = image.Img;
                        img.UpdateUser = staffId;
                        img.UpdateAt = DateTime.Now;
                        img.DeleteFlag = false;
                        _unitOfWork.Repository<InfoImageProduct>().UpdateRange(img);
                        await _unitOfWork.SaveChangesAsync();
                    }
                    else
                    {
                        var infoImageProduct = new InfoImageProduct();
                        infoImageProduct.ProductId = value.ProductId;
                        infoImageProduct.Img = image.Img;
                        infoImageProduct.CreateUser = staffId;
                        infoImageProduct.CreateAt = DateTime.Now;
                        infoImageProduct.DeleteFlag = false;
                        await _unitOfWork.Repository<InfoImageProduct>().AddAsync(infoImageProduct);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
                
            }

            if (value.listCapacityProductRes != null)
            {
                foreach (var item in value.listCapacityProductRes)
                {
                    var priceProduct = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.PriceProductId == item.PriceProductId).AsNoTracking().FirstOrDefaultAsync();
                    if (priceProduct != null)
                    {
                        priceProduct.Price = item.Price;
                        priceProduct.UpdateUser = staffId;
                        priceProduct.UpdateAt = DateTime.Now;
                        priceProduct.DeleteFlag = false;
                        _unitOfWork.Repository<InfoPriceProduct>().UpdateRange(priceProduct);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
            }
            else
            {
                var infoPriceProduct = new InfoPriceProduct();
                infoPriceProduct.ProductId = value.ProductId;
                infoPriceProduct.Price = value.Price;
                infoPriceProduct.CreateUser = staffId;
                infoPriceProduct.CreateAt = DateTime.Now;
                infoPriceProduct.DeleteFlag = false;
                infoPriceProduct.StartAt = DateTime.Now;
                await _unitOfWork.Repository<InfoPriceProduct>().AddAsync(infoPriceProduct);
                await _unitOfWork.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> DeleteProductAsync(int productId, int staffId)
        {
            var product = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true && x.ProductId == productId).AsNoTracking().FirstOrDefaultAsync();
            if (product != null)
            {
                product.DeleteFlag = true;
                product.UpdateAt = DateTime.Now;
                product.UpdateUser = staffId;
                _unitOfWork.Repository<InfoProduct>().UpdateRange(product);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            return false;
        }
    }
}
