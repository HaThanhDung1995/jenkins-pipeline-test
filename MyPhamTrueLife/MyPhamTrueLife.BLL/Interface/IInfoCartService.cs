using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoCartService
    {
        Task<bool> AddProductToCart(InfoCartRequest value);
        Task<bool> ExceptProductsToCart(int cartId);
        Task<bool> PlusProductToCart(int cartId);
        Task<bool> DeleteProductToCart(int cartId);
        Task<ResponseList> ListCartByUser(int usreId, int page = 1, int limit = 25);
    }
}
