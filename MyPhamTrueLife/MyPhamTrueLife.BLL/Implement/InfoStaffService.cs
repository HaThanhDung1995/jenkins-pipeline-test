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
    public class InfoStaffService : IInfoStaffService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoStaffService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> InsertStaffAsync(InfoStaff value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            value.CreateAt = DateTime.Now;
            value.CreateUser = userId;
            await _unitOfWork.Repository<InfoStaff>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> UpdateStaffAsync(InfoStaff value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            var Staff = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId.Equals(value.StaffId)).AsNoTracking().FirstOrDefaultAsync();
            if (Staff == null)
            {
                return false;
            }
            Staff.StaffName = value.StaffName;
            Staff.FullName = value.FullName;
            Staff.Birthday = value.Birthday;
            Staff.Email = value.Email;
            Staff.Phone = value.Phone;
            Staff.Gender = value.Gender;
            Staff.Avatar = value.Avatar;
            Staff.Address = value.Address;
            Staff.Numberfactor = value.Numberfactor;
            Staff.ProvinceId = value.ProvinceId;
            Staff.DistrictId = value.DistrictId;
            Staff.PositionStaffId = value.PositionStaffId;
            Staff.TypeStaffId = value.TypeStaffId;
            Staff.DeleteFlag = false;
            Staff.UpdateAt = DateTime.Now;
            Staff.UpdateUser = userId;
            _unitOfWork.Repository<InfoStaff>().UpdateRange(Staff);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> DeleteStaffAsync(int staffId, int userId)
        {
            if (staffId <= 0 || userId <= 0)
            {
                return false;
            }
            var Staff = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId.Equals(staffId)).AsNoTracking().FirstOrDefaultAsync();
            if (Staff == null)
            {
                return false;
            }
            Staff.DeleteFlag = true;
            Staff.UpdateAt = DateTime.Now;
            Staff.UpdateUser = userId;
            _unitOfWork.Repository<InfoStaff>().UpdateRange(Staff);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<ResponseList> ListStaffAsync(int page = 1, int limit = 25)
        {
            var listStaffNew = new List<InfoStaffReq>();
            var listData = new ResponseList();
            listData.ListData = null;
            var listStaff = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            foreach(var info in listStaff)
            {
                var infoNew = new InfoStaffReq();
                infoNew.StaffId = info.StaffId;
                infoNew.StaffName = info.StaffName;
                infoNew.Password = info.Password;
                infoNew.FullName = info.FullName;
                infoNew.Birthday = info.Birthday;
                infoNew.Email = info.Email;
                infoNew.Phone = info.Phone;
                infoNew.Gender = info.Gender;
                infoNew.Avatar = info.Avatar;
                infoNew.Address = info.Address;
                infoNew.IsActive = info.IsActive;
                infoNew.Numberfactor = info.Numberfactor;
                infoNew.ProvinceId = info.ProvinceId;
                infoNew.ProvinceName = await _unitOfWork.Repository<InfoProvince>().Where(x => x.ProvinceId == info.ProvinceId).Select(z => z.Name).AsNoTracking().FirstOrDefaultAsync();
                infoNew.DistrictId = info.DistrictId;
                infoNew.DistrictName = await _unitOfWork.Repository<InfoDistrict>().Where(x => x.DistrictId == info.DistrictId).Select(z => z.Name).AsNoTracking().FirstOrDefaultAsync(); ;
                infoNew.PositionStaffId = info.PositionStaffId;
                infoNew.PositionStaffName = await _unitOfWork.Repository<InfoPositionStaff>().Where(x => x.PositionStaffId == info.PositionStaffId).Select(z => z.PositionStaffName).AsNoTracking().FirstOrDefaultAsync(); ;
                infoNew.TypeStaffId = info.TypeStaffId;
                infoNew.TypeStaffName = await _unitOfWork.Repository<InfoTypeStaff>().Where(x => x.TypeStaffId == info.TypeStaffId).Select(z => z.TypeStaffName).AsNoTracking().FirstOrDefaultAsync(); ;
                listStaffNew.Add(infoNew);
            }
            var totalRows = listStaff.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listStaff = listStaff.Skip(start).Take(limit).ToList();
            listData.ListData = listStaffNew;
            return listData;
        }
        public async Task<InfoStaffReq> DetailStaffAsync(int staffId)
        {
            var infoNew = new InfoStaffReq();
            if (staffId <= 0)
            {
                return null;
            }
            var info = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId.Equals(staffId)).AsNoTracking().FirstOrDefaultAsync();
            if (info != null)
            {
                infoNew.StaffId = info.StaffId;
                infoNew.StaffName = info.StaffName;
                infoNew.Password = info.Password;
                infoNew.FullName = info.FullName;
                infoNew.Birthday = info.Birthday;
                infoNew.Email = info.Email;
                infoNew.Phone = info.Phone;
                infoNew.Gender = info.Gender;
                infoNew.Avatar = info.Avatar;
                infoNew.Address = info.Address;
                infoNew.IsActive = info.IsActive;
                infoNew.Numberfactor = info.Numberfactor;
                infoNew.ProvinceId = info.ProvinceId;
                infoNew.ProvinceName = await _unitOfWork.Repository<InfoProvince>().Where(x=>x.ProvinceId == info.ProvinceId).Select(z=>z.Name).AsNoTracking().FirstOrDefaultAsync();
                infoNew.DistrictId = info.DistrictId;
                infoNew.DistrictName = await _unitOfWork.Repository<InfoDistrict>().Where(x => x.DistrictId == info.DistrictId).Select(z => z.Name).AsNoTracking().FirstOrDefaultAsync(); 
                infoNew.PositionStaffId = info.PositionStaffId;
                infoNew.PositionStaffName = await _unitOfWork.Repository<InfoPositionStaff>().Where(x => x.PositionStaffId == info.PositionStaffId).Select(z => z.PositionStaffName).AsNoTracking().FirstOrDefaultAsync(); 
                infoNew.TypeStaffId = info.TypeStaffId;
                infoNew.TypeStaffName = await _unitOfWork.Repository<InfoTypeStaff>().Where(x => x.TypeStaffId == info.TypeStaffId).Select(z => z.TypeStaffName).AsNoTracking().FirstOrDefaultAsync(); 
            }
            return infoNew;
        }
    }
}
