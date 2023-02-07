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
    public class InfoProductPortfolioService : IInfoProductPortfolioService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoProductPortfolioService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> InsertProductPortfolioAsync(InfoProductPortfolio value, int userId)
        {
            bool flag = false;
            if (value == null || userId <= 0)
            {
                return flag;
            }
            else
            {
                value.CreateAt = DateTime.Now;
                value.CreateUser = userId;
                value.DeleteFlag = false;
                await _unitOfWork.Repository<InfoProductPortfolio>().AddAsync(value);
                await _unitOfWork.SaveChangeAsync();
                flag = true;
            }
            return flag;
        }
        public async Task<bool> UpdateProductPortfolioAsync(InfoProductPortfolio value, int userId)
        {
            bool flag = false;
            if (value == null || userId <= 0)
            {
                return flag;
            }
            else
            {
                var info = await _unitOfWork.Repository<InfoProductPortfolio>().Where(x => x.DeleteFlag != true && x.ProductPortfolioId == value.ProductPortfolioId).AsNoTracking().FirstOrDefaultAsync();
                info.ProductPortfolioName = value.ProductPortfolioName;
                info.Describe = value.Describe;
                info.UpdateAt = DateTime.Now;
                info.UpdateUser = userId;
                info.DeleteFlag = false;
                _unitOfWork.Repository<InfoProductPortfolio>().UpdateRange(info);
                await _unitOfWork.SaveChangeAsync();
                flag = true;
            }
            return flag;
        }
        public async Task<bool> DeleteProductPortfolioAsync(int productPortfolioId, int userId)
        {
            bool flag = false;
            if (productPortfolioId <= 0 || userId <= 0)
            {
                return flag;
            }
            else
            {
                var info = await _unitOfWork.Repository<InfoProductPortfolio>().Where(x => x.DeleteFlag != true && x.ProductPortfolioId == productPortfolioId).AsNoTracking().FirstOrDefaultAsync();
                info.UpdateAt = DateTime.Now;
                info.UpdateUser = userId;
                info.DeleteFlag = true;
                _unitOfWork.Repository<InfoProductPortfolio>().UpdateRange(info);
                await _unitOfWork.SaveChangeAsync();
                flag = true;
            }
            return flag;
        }
        public async Task<ResponseList> ListProductPortfolioAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listProductPortfolio = await _unitOfWork.Repository<InfoProductPortfolio>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listProductPortfolio.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listProductPortfolio = listProductPortfolio.Skip(start).Take(limit).ToList();
            listData.ListData = listProductPortfolio;
            return listData;
        }
        public async Task<InfoProductPortfolio> DetailProductPortfolioAsync(int productPortfolioId)
        {
            if (productPortfolioId <= 0)
            {
                return null;
            }
            else
            {
                var info = await _unitOfWork.Repository<InfoProductPortfolio>().Where(x => x.DeleteFlag != true && x.ProductPortfolioId == productPortfolioId).AsNoTracking().FirstOrDefaultAsync();
                return info;
            }
        }
    }
}
