using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IProductService
    {
        Task<List<ListProductPortfolioUser>> ListProductPortfolioUserAsync();
        Task<ResponseList> TopSixSellingProducts();
        Task<ResponseList> TopSixNewProducts();
        Task<ResponseList> TopSixProductPromotion();
        Task<ResponseList> ShowListProductFilter(ProductFilterRequest value, int page = 1, int limit = 25);
        Task<ProductDetail> ProductDetailAsync(int id, int? nature, int? capacity);
    }
}
