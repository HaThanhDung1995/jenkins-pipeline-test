using Microsoft.EntityFrameworkCore;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPhamTrueLife.BLL.Ultil;

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

        public async Task<ResponseList> GetListOrderByUserAdminAsync(int userId, int page = 1, int limit = 25)
        {
            var result = new ResponseList();
            var listOrder = _unitOfWork.Repository<InfoOrder>().Where(x => x.DeleteFlag != true);

            var listData = await (from a in listOrder
                                  where a.UserId == userId
                                  select new InfoOrderList()
                                  {
                                      OrderId = a.OrderId,
                                      UserId = a.UserId,
                                      IsAccept = a.IsAccept,
                                      StaffId = a.StaffId,
                                      Total = a.Total,
                                      Status = a.Status,
                                      IsPay = a.IsPay,
                                      StatusOrder = a.StatusOrder,
                                      SeverId = a.SeverId,
                                      DateTimeD = a.DateTimeD,
                                      AddressDeliveryId = a.AddressDeliveryId,
                                      //FullName = b.FullName,
                                      //FullNameStaff = c.FullName,
                                      CreateAt = a.CreateAt
                                  }).AsNoTracking().ToListAsync();
            foreach (var item in listData)
            {
                item.FullName = item.UserId == null ? "" : await _unitOfWork.Repository<InfoUser>().Where(x => x.DeleteFlag != true && x.UserId == item.UserId).AsNoTracking().Select(z => z.FullName).FirstOrDefaultAsync();
                item.FullNameStaff = item.StaffId == null ? "" : await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId == item.StaffId).AsNoTracking().Select(z => z.FullName).FirstOrDefaultAsync();
                var infoAdd = item.AddressDeliveryId == null ? null : await _unitOfWork.Repository<InfoAddressDeliveryUser>().Where(x => x.DeleteFlag != true && x.AddressDeliveryId == item.AddressDeliveryId.Value).AsNoTracking().FirstOrDefaultAsync();

                var addRess = new IndoAddressDeliveryReq();
                PropertyCopier<InfoAddressDeliveryUser, IndoAddressDeliveryReq>.Copy(infoAdd, addRess);
                item.infoAddressDeliveryUser = new IndoAddressDeliveryReq();
                addRess.ProvinceName = await _unitOfWork.Repository<InfoProvince>().Where(x => x.DeleteFlag != true && x.ProvinceId == addRess.ProvinceId).AsNoTracking().Select(z => z.Name).FirstOrDefaultAsync();
                addRess.DistrictName = await _unitOfWork.Repository<InfoDistrict>().Where(x => x.DeleteFlag != true && x.DistrictId == addRess.DistrictId).AsNoTracking().Select(z => z.Name).FirstOrDefaultAsync();
                item.infoAddressDeliveryUser = addRess;
            }
            var totalRows = listData.Count();
            result.Paging = new Paging(totalRows, page, limit);
            int start = result.Paging.start;
            listData = listData.OrderByDescending(z => z.CreateAt).Skip(start).Take(limit).ToList();
            result.ListData = listData;
            return result;
        }
    }
}
