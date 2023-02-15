using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyPhamTrueLife.BLL.Implement
{
    public class InfoCartService : IInfoCartService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoCartService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Thêm sản phẩm vào giỏ
        public async Task<bool> AddProductToCart(InfoCartRequest value)
        {
            int quantity = 0;
            var product = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true && x.ProductId == value.ProductId).AsNoTracking().FirstOrDefaultAsync();
            if (product.IsExpiry != true)
            {
                quantity = product.Amount.Value;
            }
            else
            {
                
                if (value.CapacityId != null)
                {
                    quantity = await _unitOfWork.Repository<InfoExpiryProduct>().Where(x => x.DeleteFlag != true && x.ProductId == value.ProductId && x.CapacityId == value.CapacityId).AsNoTracking().Select(x => x.Amount.Value).SumAsync();
                }
                else
                {
                    quantity = await _unitOfWork.Repository<InfoExpiryProduct>().Where(x => x.DeleteFlag != true && x.ProductId == value.ProductId).AsNoTracking().Select(x=>x.Amount.Value).SumAsync();
                }    

            }

            if (value == null || value.UserId <= 0 || quantity <= 0)
            {
                return false;
            }
            var cartNew = await _unitOfWork.Repository<InfoCart>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(value.ProductId) && x.CapacityId == value.CapacityId).AsNoTracking().FirstOrDefaultAsync();
            if (cartNew != null)
            {
                cartNew.Quantity = cartNew.Quantity + value.Quantity;
                cartNew.Total = value.Total;
                cartNew.UpdateAt = DateTime.Now;
                cartNew.UpdateUser = value.UserId;
                cartNew.DeleteFlag = false;
                _unitOfWork.Repository<InfoCart>().UpdateRange(cartNew);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            var cart = new InfoCart();
            cart.UserId = value.UserId;
            cart.ProductId = value.ProductId;
            cart.Quantity = value.Quantity;
            cart.CapacityId = value.CapacityId;
            cart.Total = value.Total;
            cart.CreateAt = DateTime.Now;
            cart.CreateUser = value.UserId;
            cart.DeleteFlag = false;
            await _unitOfWork.Repository<InfoCart>().AddAsync(cart);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        //Trừ số lượng sản phẩm trong cart
        public async Task<bool> ExceptProductsToCart(int cartId)
        {
            if (cartId <= 0)
            {
                return false;
            }
            var cart = await _unitOfWork.Repository<InfoCart>().Where(x => x.DeleteFlag != true && x.CartId.Equals(cartId)).AsNoTracking().FirstOrDefaultAsync();
            if (cart == null)
            {
                return false;
            }
            if (cart.Quantity == 1)
            {
                cart.UpdateAt = DateTime.Now;
                cart.DeleteFlag = true;
            }
            if (cart.Quantity > 1)
            {
                cart.Quantity = cart.Quantity - 1;
                cart.UpdateAt = DateTime.Now;
                cart.DeleteFlag = false;
            }
            _unitOfWork.Repository<InfoCart>().UpdateRange(cart);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        //Cộng số lượng sản phẩm trong cart
        public async Task<bool> PlusProductToCart(int cartId)
        {
            

            if (cartId <= 0 )
            {
                return false;
            }
            var cart = await _unitOfWork.Repository<InfoCart>().Where(x => x.DeleteFlag != true && x.CartId.Equals(cartId)).AsNoTracking().FirstOrDefaultAsync();
            int quantity = 0;
            var product = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true && x.ProductId == cart.ProductId).AsNoTracking().FirstOrDefaultAsync();
            if (product != null)
            {
                quantity += cart.Quantity.Value;
            }
            if (product.IsExpiry != true)
            {
                quantity = product.Amount.Value;
            }
            else
            {

                if (cart.CapacityId != null)
                {
                    quantity = await _unitOfWork.Repository<InfoExpiryProduct>().Where(x => x.DeleteFlag != true && x.ProductId == cart.ProductId && x.CapacityId == cart.CapacityId).AsNoTracking().Select(x => x.Amount.Value).SumAsync();
                }
                else
                {
                    quantity = await _unitOfWork.Repository<InfoExpiryProduct>().Where(x => x.DeleteFlag != true && x.ProductId == cart.ProductId).AsNoTracking().Select(x => x.Amount.Value).SumAsync();
                }

            }

            if (cart == null || quantity <= 0)
            {
                return false;
            }
            if (cart.Quantity == null)
            {
                cart.Quantity = 1;
                cart.UpdateAt = DateTime.Now;
                cart.DeleteFlag = false;
            }
            if (cart.Quantity > 0)
            {
                cart.Quantity = cart.Quantity + 1;
                cart.UpdateAt = DateTime.Now;
                cart.DeleteFlag = false;
            }
            _unitOfWork.Repository<InfoCart>().UpdateRange(cart);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        //Xóa sản phẩm khỏi giỏ hàng
        public async Task<bool> DeleteProductToCart(int cartId)
        {
            if (cartId <= 0)
            {
                return false;
            }
            var cart = await _unitOfWork.Repository<InfoCart>().Where(x => x.DeleteFlag != true && x.CartId.Equals(cartId)).AsNoTracking().FirstOrDefaultAsync();
            if (cart == null)
            {
                return false;
            }
            cart.UpdateAt = DateTime.Now;
            cart.DeleteFlag = true;
            _unitOfWork.Repository<InfoCart>().UpdateRange(cart);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<ResponseList> ListCartByUser(int usreId, int page = 1, int limit = 25)
        {
            var result = new ResponseList();
            var listCart = await _unitOfWork.Repository<InfoCart>().Where(x => x.DeleteFlag != true && x.UserId.Equals(usreId)).AsNoTracking().ToListAsync();
            if (listCart == null || listCart.Count <= 0)
            {
                return null;
            }
            var listInfo = new List<InfoCartUserShow>();
            foreach (var item in listCart)
            {
                var info = new InfoCartUserShow();
                info.CartId = item.CartId;
                info.UserId = item.UserId;
                info.ProductId = item.ProductId;
                info.Quantity = item.Quantity;
                var pro = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(item.ProductId)).AsNoTracking().FirstOrDefaultAsync();
                if (pro != null)
                {
                    info.ProductName = pro.ProductName;
                    info.Avatar = pro.Avatar;
                    if (pro.NatureId != null)
                    {
                        var nature = await _unitOfWork.Repository<InfoNature>().Where(x => x.DeleteFlag != true && x.NatureId.Equals(pro.NatureId)).AsNoTracking().FirstOrDefaultAsync();
                        if (nature != null)
                        {
                            info.ProductName += " - " + nature.NatureName;
                        }
                    }
                }
                if (item.CapacityId != null)
                {
                    var capacity = await _unitOfWork.Repository<InfoCapacity>().Where(x => x.DeleteFlag != true && x.CapacityId.Equals(item.CapacityId)).AsNoTracking().FirstOrDefaultAsync();
                    if (capacity != null)
                    {
                        info.ProductName += " - " + capacity.CapacityName;
                    }
                }
                if (item.CapacityId == null)
                {
                    var priceProduct = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(item.ProductId)).AsNoTracking().OrderByDescending(z => z.StartAt).ToListAsync();
                    if (priceProduct != null && priceProduct.Count > 0)
                    {
                        info.ProductPrice = priceProduct[0].Price;
                        info.Total = info.ProductPrice * info.Quantity;
                    }
                }
                
                if (item.CapacityId != null)
                {
                    var priceProduct = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(item.ProductId) && x.CapacityId.Equals(item.CapacityId)).AsNoTracking().OrderByDescending(z => z.StartAt).ToListAsync();
                    if (priceProduct != null && priceProduct.Count > 0)
                    {
                        info.ProductPrice = priceProduct[0].Price;
                        info.Total = info.ProductPrice * info.Quantity;
                    }
                }


                listInfo.Add(info);
            }
            var totalRows = listInfo.Count();
            result.Paging = new Paging(totalRows, page, limit);
            int start = result.Paging.start;
            listInfo = listInfo.Skip(start).Take(limit).ToList();
            result.ListData = listInfo;
            return result;
        }
    }
}
