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
    public class UserService : IUserService
    {
        public readonly dbDevNewContext _unitOfWork;
        public UserService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponse> LoginAsync(Login value)
        {
            if (!string.IsNullOrEmpty(value.PassWord))
            {
                value.PassWord = FunctionUtils.CreateSHA256(!string.IsNullOrEmpty(value.UserName) ? value.UserName : value.UserName, value.PassWord);
            }
            var info = await _unitOfWork.Repository<InfoUser>().Where(x => x.UserName.Equals(value.UserName) && x.Password.Equals(value.PassWord) && x.DeleteFlag != true)
                .AsNoTracking().FirstOrDefaultAsync();
            if (info == null)
            {
                return null;
            }
            var inf = new LoginResponse();
            inf.UserId = info.UserId;
            inf.UserName = info.UserName;
            inf.Password = info.Password;
            inf.FullName = info.FullName;
            inf.Birthday = info.Birthday;
            inf.Email = info.Email;
            inf.Phone = info.Phone;
            inf.Gender = info.Gender;
            inf.Avatar = info.Avatar;
            inf.Address = info.Address;
            inf.IsActive = info.IsActive;
            inf.ProvinceName = await _unitOfWork.Repository<InfoProvince>().Where(x => x.ProvinceId.Equals(info.ProvinceId))
                .AsNoTracking().Select(z => z.Name).FirstOrDefaultAsync();
            inf.DistrictName = await _unitOfWork.Repository<InfoDistrict>().Where(x => x.ProvinceId.Equals(info.DistrictId))
                .AsNoTracking().Select(z => z.Name).FirstOrDefaultAsync();
            inf.TypeUserName = await _unitOfWork.Repository<InfoTypeUser>().Where(x => x.TypeUserId.Equals(info.TypeUserId))
                .AsNoTracking().Select(z => z.TypeUserName).FirstOrDefaultAsync();
            return inf;
        }
        public async Task<InfoUser> ResgisterUserAsync(RegisterUser value)
        {
            if (value == null)
            {
                return null;
            }
            var info = new InfoUser();
            if (!string.IsNullOrEmpty(value.Password))
            {
                info.Password = FunctionUtils.CreateSHA256(!string.IsNullOrEmpty(value.UserName) ? value.UserName : value.UserName, value.Password);
            }
            info.Email = value.Email;
            info.Phone = value.Phone;
            info.UserName = value.UserName;
            info.CreateAt = DateTime.Now;
            info.DeleteFlag = false;
            info.TypeUserId = 1;
            var inf = await _unitOfWork.Repository<InfoUser>().AddAsync(info);
            await _unitOfWork.SaveChangeAsync();
            return inf.Entity;
        }
        public async Task<int> ForgotPassWordAsync(string UserName)
        {
            var info = await _unitOfWork.Repository<InfoUser>().Where(x => x.UserName.Equals(UserName)).AsNoTracking().FirstOrDefaultAsync();
            if (info == null)
            {
                return 1;
            }
            if (info != null && info.DeleteFlag == true)
            {
                return 2;
            }
            info.Password = FunctionUtils.CreateSHA256(!string.IsNullOrEmpty(info.UserName) ? info.UserName : info.UserName, "123");
            info.UpdateAt = DateTime.Now;
            info.UpdateUser = info.UserId;
            info.DeleteFlag = false;
            _unitOfWork.Repository<InfoUser>().Update(info);
            await _unitOfWork.SaveChangeAsync();
            return 0;
        }
        public async Task<bool> CheckUserNameAsync(string userName)
        {
            var info = await _unitOfWork.Repository<InfoUser>().Where(x => x.UserName.Equals(userName)).AsNoTracking().FirstOrDefaultAsync();
            if (info != null)
            {
                return false;
            }
            return true;
        }
        public async Task<InfoUser> GetByUserNameAsync(string userName)
        {
            var info = await _unitOfWork.Repository<InfoUser>().Where(x => x.UserName.Equals(userName)).AsNoTracking().FirstOrDefaultAsync();
            return info;
        }
    }
}
