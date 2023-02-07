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
    public class InfoTypeStaffService : IInfoTypeStaffService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoTypeStaffService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> InsertTypeStaffAsync(InfoTypeStaff value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            value.CreateAt = DateTime.Now;
            value.CreateUser = userId;
            await _unitOfWork.Repository<InfoTypeStaff>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> UpdateTypeStaffAsync(InfoTypeStaff value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            var typeStaff = await _unitOfWork.Repository<InfoTypeStaff>().Where(x => x.DeleteFlag != true && x.TypeStaffId.Equals(value.TypeStaffId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeStaff == null)
            {
                return false;
            }
            typeStaff.TypeStaffName = value.TypeStaffName;
            typeStaff.DeleteFlag = false;
            typeStaff.UpdateAt = DateTime.Now;
            typeStaff.UpdateUser = userId;
            _unitOfWork.Repository<InfoTypeStaff>().UpdateRange(typeStaff);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> DeleteTypeStaffAsync(int typeStaffId, int userId)
        {
            if (typeStaffId <= 0 || userId <= 0)
            {
                return false;
            }
            var typeStaff = await _unitOfWork.Repository<InfoTypeStaff>().Where(x => x.DeleteFlag != true && x.TypeStaffId.Equals(typeStaffId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeStaff == null)
            {
                return false;
            }
            typeStaff.DeleteFlag = true;
            typeStaff.UpdateAt = DateTime.Now;
            typeStaff.UpdateUser = userId;
            _unitOfWork.Repository<InfoTypeStaff>().UpdateRange(typeStaff);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<ResponseList> ListTypeStaffAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listTypeStaff = await _unitOfWork.Repository<InfoTypeStaff>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listTypeStaff.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listTypeStaff = listTypeStaff.Skip(start).Take(limit).ToList();
            listData.ListData = listTypeStaff;
            return listData;
        }
        public async Task<InfoTypeStaff> DetailTypeStaffAsync(int typeStaffId)
        {
            if (typeStaffId <= 0)
            {
                return null;
            }
            var info = await _unitOfWork.Repository<InfoTypeStaff>().Where(x => x.DeleteFlag != true && x.TypeStaffId.Equals(typeStaffId)).AsNoTracking().FirstOrDefaultAsync();
            return info;
        }
    }
}
