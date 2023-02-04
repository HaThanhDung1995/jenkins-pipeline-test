using Microsoft.EntityFrameworkCore;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Implement
{
    public class InfoCapicityService : IInfoCapicityService
    {
        private readonly dbDevNewContext _unitOfWork;
        public InfoCapicityService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteCapacityAsync(int capicityId, int userId)
        {
            if (capicityId <= 0 || userId <= 0)
            {
                return false;
            }
            var capicity = await _unitOfWork.Repository<InfoCapacity>().Where(x => x.DeleteFlag != true && x.CapacityId.Equals(capicityId)).AsNoTracking().FirstOrDefaultAsync();
            if (capicity == null)
            {
                return false;
            }
            capicity.DeleteFlag = true;
            capicity.UpdateAt = DateTime.Now;
            capicity.UpdateUser = userId;
            _unitOfWork.Repository<InfoCapacity>().UpdateRange(capicity);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<InfoCapacity> DetailCapacityAsync(int capicityId)
        {
            if (capicityId <= 0)
            {
                return null;
            }
            var info = await _unitOfWork.Repository<InfoCapacity>().Where(x => x.DeleteFlag != true && x.CapacityId.Equals(capicityId)).AsNoTracking().FirstOrDefaultAsync();
            return info;
        }

        public async Task<bool> InsertCapacityAsync(InfoCapacity value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            value.CreateAt = DateTime.Now;
            value.CreateUser = userId;
            await _unitOfWork.Repository<InfoCapacity>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<ResponseList> ListCapacityAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listCapicity = await _unitOfWork.Repository<InfoCapacity>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listCapicity.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listCapicity = listCapicity.Skip(start).Take(limit).ToList();
            listData.ListData = listCapicity;
            return listData;
        }

        public async Task<bool> UpdateCapacityAsync(InfoCapacity value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            var typeNature = await _unitOfWork.Repository<InfoCapacity>().Where(x => x.DeleteFlag != true && x.CapacityId.Equals(value.CapacityId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeNature == null)
            {
                return false;
            }
            typeNature.CapacityName = value.CapacityName;
            typeNature.Unit = value.Unit;
            typeNature.DeleteFlag = false;
            typeNature.UpdateAt = DateTime.Now;
            typeNature.UpdateUser = userId;
            _unitOfWork.Repository<InfoCapacity>().UpdateRange(typeNature);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
