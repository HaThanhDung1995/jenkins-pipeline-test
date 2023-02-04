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
    public class UserAdminService: IUserAdminService
    {
        public readonly dbDevNewContext _unitOfWork;
        public UserAdminService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponse> LoginAsync(Login value)
        {
            //if (!string.IsNullOrEmpty(value.PassWord))
            //{
            //    value.PassWord = FunctionUtils.CreateSHA256(!string.IsNullOrEmpty(value.UserName) ? value.UserName : value.UserName, value.PassWord);
            //}
            var info = await _unitOfWork.Repository<InfoStaff>().Where(x => x.StaffName.Equals(value.UserName) && x.Password.Equals(value.PassWord) && x.DeleteFlag != true)
                .AsNoTracking().FirstOrDefaultAsync();
            if (info == null)
            {
                return null;
            }
            var inf = new LoginResponse();
            inf.UserId = info.StaffId;
            inf.UserName = info.StaffName;
            inf.Password = info.Password;
            inf.FullName = info.FullName;
            inf.Birthday = info.Birthday;
            inf.Email = info.Email;
            inf.Phone = info.Phone;
            inf.Gender = info.Gender;
            inf.Avatar = info.Avatar;
            inf.Address = info.Address;
            inf.IsActive = info.IsActive;
            inf.TypeUserId = info.TypeStaffId == null ? 0 : info.TypeStaffId.Value;
            inf.positionStaffId = info.PositionStaffId == null ? 0 : info.PositionStaffId.Value;
            inf.ProvinceName = await _unitOfWork.Repository<InfoProvince>().Where(x => x.ProvinceId.Equals(info.ProvinceId))
                .AsNoTracking().Select(z => z.Name).FirstOrDefaultAsync();
            inf.DistrictName = await _unitOfWork.Repository<InfoDistrict>().Where(x => x.ProvinceId.Equals(info.DistrictId))
                .AsNoTracking().Select(z => z.Name).FirstOrDefaultAsync();
            inf.TypeUserName = await _unitOfWork.Repository<InfoTypeStaff>().Where(x => x.TypeStaffId.Equals(info.TypeStaffId))
                .AsNoTracking().Select(z => z.TypeStaffName).FirstOrDefaultAsync();
            return inf;
        }

        public async Task<bool> InsertStaff(InfoStaff value)
        {
            if (value == null)
            {
                return false;
            }
            value.CreateAt = DateTime.Now;
            value.DeleteFlag = false;
            await _unitOfWork.Repository<InfoStaff>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> UpdateStaff(InfoStaff value)
        {
            if (value == null)
            {
                return false;
            }
            value.UpdateAt = DateTime.Now;
            value.DeleteFlag = false;
            _unitOfWork.Repository<InfoStaff>().UpdateRange(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> DeleteStaff(int userId)
        {
            if (userId <= 0)
            {
                return false;
            }
            var infoUser = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId.Equals(userId)).AsNoTracking().FirstOrDefaultAsync();
            if (infoUser == null)
            {
                return false;
            }
            infoUser.UpdateAt = DateTime.Now;
            infoUser.DeleteFlag = true;
            _unitOfWork.Repository<InfoStaff>().UpdateRange(infoUser);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<InfoStaff> DetailStaff(int userId)
        {
            if (userId <= 0)
            {
                return null;
            }
            var user = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId.Equals(userId)).AsNoTracking().FirstOrDefaultAsync();
            return user;
        }
        public async Task<ResponseList> ListStaff(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listUser = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listUser.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listUser = listUser.Skip(start).Take(limit).ToList();
            listData.ListData = listUser;
            return listData;
        }
    }
}
