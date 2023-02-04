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
    public class InfoTypeUserService : IInfoTypeUserService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoTypeUserService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> InsertTypeUserAsync(InfoTypeUserReq value, int userId)
        {
            if (value == null || userId < 0)
            {
                return false;
            }
            var inf = new InfoTypeUser();
            inf.TypeUserName = value.TypeUserName;
            inf.DiscountPercen = value.DiscountPercen;
            inf.CreateAt = DateTime.Now;
            inf.CreateUser = userId;
            inf.DeleteFlag = false;
            await _unitOfWork.InfoTypeUsers.AddAsync(inf);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTypeUserAsync(InfoTypeUserReq value, int userId)
        {
            if (value == null || userId < 0)
            {
                return false;
            }
            var info = await _unitOfWork.Repository<InfoTypeUser>().Where(x => x.DeleteFlag != true && x.TypeUserId.Equals(value.TypeUserId)).AsNoTracking().FirstOrDefaultAsync();
            if (info == null)
            {
                return false;
            }
            info.TypeUserName = value.TypeUserName;
            info.DiscountPercen = value.DiscountPercen;
            info.UpdateAt = DateTime.Now;
            info.UpdateUser = userId;
            info.DeleteFlag = false;
            _unitOfWork.InfoTypeUsers.UpdateRange(info);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteTypeUserAsync(int typeUserId, int userId)
        {
            if (typeUserId <= 0 || userId <= 0)
            {
                return false;
            }
            var info = await _unitOfWork.Repository<InfoTypeUser>().Where(x => x.DeleteFlag != true && x.TypeUserId.Equals(typeUserId)).AsNoTracking().FirstOrDefaultAsync();
            if (info == null)
            {
                return false;
            }
            info.UpdateAt = DateTime.Now;
            info.UpdateUser = userId;
            info.DeleteFlag = false;
            _unitOfWork.InfoTypeUsers.UpdateRange(info);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<ResponseList> ListTypeUserAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listTypeUser = await _unitOfWork.Repository<InfoTypeUser>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listTypeUser.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listTypeUser = listTypeUser.Skip(start).Take(limit).ToList();
            listData.ListData = listTypeUser;
            return listData;
        }
        public async Task<InfoTypeUser> DetailTypeUserAsync(int typeUserId)
        {
            if (typeUserId <= 0)
            {
                return null;
            }
            var info = await _unitOfWork.Repository<InfoTypeUser>().Where(x => x.DeleteFlag != true && x.TypeUserId.Equals(typeUserId)).AsNoTracking().FirstOrDefaultAsync();
            return info;
        }
    }
}
