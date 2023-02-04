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
    public class InfoUserService : IInfoUserService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoUserService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> InsertInfoUserAssync(InfoUserReq value, int userId)
        {
            if (value == null)
            {
                return false;
            }
            var info = new InfoUser();
            info.UserName = value.UserName;
            info.Password = value.Password;
            info.FullName = value.FullName;
            info.Birthday = value.Birthday;
            info.Email = value.Email;
            info.Phone = value.Phone;
            info.Gender = value.Gender;
            info.Avatar = value.Avatar;
            info.Address = value.Address;
            info.IsActive = false;
            info.ProvinceId = value.ProvinceId;
            info.DistrictId = value.DistrictId;
            info.TypeUserId = value.TypeUserId;
            info.CreateAt = DateTime.Now;
            info.CreateUser = userId;
            info.DeleteFlag = false;
            await _unitOfWork.InfoUsers.AddAsync(info);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> UpdateInfoUserAsync(InfoUser value)
        {
            if (value == null)
            {
                return false;
            }
            value.UpdateAt = DateTime.Now;
            value.UpdateUser = value.UserId;
            value.DeleteFlag = false;
            _unitOfWork.Repository<InfoUser>().UpdateRange(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<InfoUser> DetailInfoUserAsync(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }
            var user = await _unitOfWork.Repository<InfoUser>().Where(x => x.DeleteFlag != true && x.UserId.Equals(userId)).AsNoTracking().FirstOrDefaultAsync();
            return user;
        }

        public async Task<ResponseList> ListInfoUserAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listUser = await _unitOfWork.Repository<InfoUser>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listUser.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listUser = listUser.Skip(start).Take(limit).ToList();
            listData.ListData = listUser;
            return listData;
        }

        public async Task<bool> DeleteInfoUserAsync(int userId, int userIDelete)
        {
            if (userId <= 0 || userIDelete <= 0)
            {
                return false;
            }
            var infoUser = await _unitOfWork.Repository<InfoUser>().Where(x => x.DeleteFlag != true && x.UserId.Equals(userId)).AsNoTracking().FirstOrDefaultAsync();
            if (infoUser == null)
            {
                return false;
            }
            infoUser.UpdateAt = DateTime.Now;
            infoUser.UpdateUser = userIDelete;
            infoUser.DeleteFlag = true;
            _unitOfWork.Repository<InfoUser>().UpdateRange(infoUser);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
