using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoProductPortfolioService
    {
        Task<bool> InsertProductPortfolioAsync(InfoProductPortfolio value, int userId);
        Task<bool> UpdateProductPortfolioAsync(InfoProductPortfolio value, int userId);
        Task<bool> DeleteProductPortfolioAsync(int productPortfolioId, int userId);
        Task<ResponseList> ListProductPortfolioAsync(int page = 1, int limit = 25);
        Task<InfoProductPortfolio> DetailProductPortfolioAsync(int productPortfolioId);
    }
}
