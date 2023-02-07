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
    public class InfoPositionStaffService : IInfoPositionStaffService
    {
        private readonly dbDevNewContext _unitOfWork;
        public InfoPositionStaffService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeletePositionStaffAsync(int natureId, int userId)
        {
            if (natureId <= 0 || userId <= 0)
            {
                return false;
            }
            var typeNature = await _unitOfWork.Repository<InfoPositionStaff>().Where(x => x.DeleteFlag != true && x.PositionStaffId.Equals(natureId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeNature == null)
            {
                return false;
            }
            typeNature.DeleteFlag = true;
            typeNature.UpdateAt = DateTime.Now;
            typeNature.UpdateUser = userId;
            _unitOfWork.Repository<InfoPositionStaff>().UpdateRange(typeNature);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<InfoPositionStaff> DetailPositionStaffAsync(int natureId)
        {
            if (natureId <= 0)
            {
                return null;
            }
            var info = await _unitOfWork.Repository<InfoPositionStaff>().Where(x => x.DeleteFlag != true && x.PositionStaffId.Equals(natureId)).AsNoTracking().FirstOrDefaultAsync();
            return info;
        }

        public async Task<bool> InsertPositionStaffAsync(InfoPositionStaff value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            value.CreateAt = DateTime.Now;
            value.CreateUser = userId;
            await _unitOfWork.Repository<InfoPositionStaff>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<ResponseList> ListPositionStaffAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listTypeNature = await _unitOfWork.Repository<InfoPositionStaff>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listTypeNature.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listTypeNature = listTypeNature.Skip(start).Take(limit).ToList();
            listData.ListData = listTypeNature;
            return listData;
        }

        public async Task<bool> UpdatePositionStaffAsync(InfoPositionStaff value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            var typeNature = await _unitOfWork.Repository<InfoPositionStaff>().Where(x => x.DeleteFlag != true && x.PositionStaffId.Equals(value.PositionStaffId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeNature == null)
            {
                return false;
            }
            typeNature.PositionStaffName = value.PositionStaffName;
            typeNature.PositionStaffId = value.PositionStaffId;
            typeNature.DeleteFlag = false;
            typeNature.UpdateAt = DateTime.Now;
            typeNature.UpdateUser = userId;
            _unitOfWork.Repository<InfoPositionStaff>().UpdateRange(typeNature);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
