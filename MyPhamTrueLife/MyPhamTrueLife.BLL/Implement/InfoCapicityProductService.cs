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
    public class InfoCapicityProductService : IInfoCapicityProductService
    {
        private readonly dbDevNewContext _unitOfWork;
        public InfoCapicityProductService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteCapacityProductAsync(int capicityProductId, int userId)
        {
            if (capicityProductId <= 0 || userId <= 0)
            {
                return false;
            }
            var typeNature = await _unitOfWork.Repository<InfoCapacityProduct>().Where(x => x.DeleteFlag != true && x.CapacityProductId.Equals(capicityProductId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeNature == null)
            {
                return false;
            }
            typeNature.DeleteFlag = true;
            typeNature.UpdateAt = DateTime.Now;
            typeNature.UpdateUser = userId;

            _unitOfWork.Repository<InfoCapacityProduct>().UpdateRange(typeNature);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<InfoCapacityProduct> DetailCapacityProductAsync(int capicityProductId)
        {
            if (capicityProductId <= 0)
            {
                return null;
            }
            var info = await _unitOfWork.Repository<InfoCapacityProduct>().Where(x => x.DeleteFlag != true && x.CapacityProductId.Equals(capicityProductId)).AsNoTracking().FirstOrDefaultAsync();
            return info;
        }

        public async Task<bool> InsertCapacityProductAsync(InfoCapacityProduct value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            value.CreateAt = DateTime.Now;
            value.CreateUser = userId;
            await _unitOfWork.Repository<InfoCapacityProduct>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<ResponseList> ListCapacityProductAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listTypeNature = await _unitOfWork.Repository<InfoCapacityProduct>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listTypeNature.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listTypeNature = listTypeNature.Skip(start).Take(limit).ToList();
            listData.ListData = listTypeNature;
            return listData;
        }

        public async Task<bool> UpdateCapacityProductAsync(InfoCapacityProduct value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            var typeNature = await _unitOfWork.Repository<InfoCapacityProduct>().Where(x => x.DeleteFlag != true && x.CapacityProductId.Equals(value.CapacityProductId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeNature == null)
            {
                return false;
            }
            typeNature.CapacityId = value.CapacityId;
            typeNature.ProductId = value.ProductId;
            typeNature.DeleteFlag = false;
            typeNature.UpdateAt = DateTime.Now;
            typeNature.UpdateUser = userId;
            _unitOfWork.Repository<InfoCapacityProduct>().UpdateRange(typeNature);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
