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

        public async Task<bool> TaoLichLamViec(int userId)
        {
            if (userId <= 0)
            {
                return false;
            }
            var infoCalenda = await _unitOfWork.Repository<InfoCalendar>().Where(x => x.DeleteFlag != true).OrderByDescending(z => z.CreateAt).AsNoTracking().ToListAsync();
            if (infoCalenda == null || infoCalenda.Count <= 0)
            {
                var date = DateTime.Now;
                int month = date.Month;
                int year = date.Year;
                var infoCalendaInsert = new InfoCalendar();
                infoCalendaInsert.MonthI = month;
                infoCalendaInsert.YearI = year;
                infoCalendaInsert.CreateAt = DateTime.Now;
                infoCalendaInsert.CreateUser = userId;
                infoCalendaInsert.DeleteFlag = false;
                var relsut = await _unitOfWork.Repository<InfoCalendar>().AddAsync(infoCalendaInsert);
                await _unitOfWork.SaveChangeAsync();
                //Thêm từng ca
                //ca 1
                var infoDetailCalenda1 = new InfoDetailCalendar();
                infoDetailCalenda1.CalendarId = relsut.Entity.CalendarId;
                infoDetailCalenda1.DayI = date.Day;
                infoDetailCalenda1.ShiftI = 1;
                infoDetailCalenda1.StaffId = 1;
                infoDetailCalenda1.CreateAt = DateTime.Now;
                infoDetailCalenda1.CreateUser = userId;
                infoDetailCalenda1.DeleteFlag = false;
                await _unitOfWork.Repository<InfoDetailCalendar>().AddAsync(infoDetailCalenda1);
                await _unitOfWork.SaveChangeAsync();
                //_unitOfWork.SaveChange();
                //ca 2
                var infoDetailCalenda2 = new InfoDetailCalendar();
                infoDetailCalenda2.CalendarId = relsut.Entity.CalendarId;
                infoDetailCalenda2.DayI = date.Day;
                infoDetailCalenda2.ShiftI = 2;
                infoDetailCalenda2.StaffId = 1;
                infoDetailCalenda2.CreateAt = DateTime.Now;
                infoDetailCalenda2.CreateUser = userId;
                infoDetailCalenda2.DeleteFlag = false;
                await _unitOfWork.Repository<InfoDetailCalendar>().AddAsync(infoDetailCalenda2);
                await _unitOfWork.SaveChangeAsync();
                //_unitOfWork.SaveChange();
                for (int i = 0; i < 14; i++)
                {
                    date.AddDays(1);
                    if (date.Month != month || date.Year != year)
                    {
                        var infoCalendaInsert1 = new InfoCalendar();
                        infoCalendaInsert1.MonthI = date.Month;
                        infoCalendaInsert1.YearI = date.Year;
                        infoCalendaInsert1.CreateAt = DateTime.Now;
                        infoCalendaInsert1.CreateUser = userId;
                        infoCalendaInsert1.DeleteFlag = false;
                        relsut = await _unitOfWork.Repository<InfoCalendar>().AddAsync(infoCalendaInsert1);
                        await _unitOfWork.SaveChangeAsync();
                        month = date.Month;
                        year = date.Year;
                    }
                    else
                    {
                        //Thêm từng ca
                        //ca 1
                        var infoDetailCalenda3 = new InfoDetailCalendar();
                        infoDetailCalenda3.CalendarId = relsut.Entity.CalendarId;
                        infoDetailCalenda3.DayI = date.Day;
                        infoDetailCalenda3.ShiftI = 1;
                        infoDetailCalenda3.StaffId = 1;
                        infoDetailCalenda3.CreateAt = DateTime.Now;
                        infoDetailCalenda3.CreateUser = userId;
                        infoDetailCalenda3.DeleteFlag = false;
                        await _unitOfWork.Repository<InfoDetailCalendar>().AddAsync(infoDetailCalenda3);
                        await _unitOfWork.SaveChangeAsync();
                        //ca 2
                        var infoDetailCalenda4 = new InfoDetailCalendar();
                        infoDetailCalenda4.CalendarId = relsut.Entity.CalendarId;
                        infoDetailCalenda4.DayI = date.Day;
                        infoDetailCalenda4.ShiftI = 2;
                        infoDetailCalenda4.StaffId = 1;
                        infoDetailCalenda4.CreateAt = DateTime.Now;
                        infoDetailCalenda4.CreateUser = userId;
                        infoDetailCalenda4.DeleteFlag = false;
                        await _unitOfWork.Repository<InfoDetailCalendar>().AddAsync(infoDetailCalenda4);
                        await _unitOfWork.SaveChangeAsync();
                    }
                }
            }
            else
            {
                var date = infoCalenda.FirstOrDefault();
                var infoDetail = await _unitOfWork.Repository<InfoDetailCalendar>().Where(x => x.CalendarId == date.CalendarId).OrderByDescending(z=>z.DayI).AsNoTracking().FirstOrDefaultAsync();
                DateTime date1 = new DateTime(date.YearI.Value, date.MonthI.Value, infoDetail.DayI, 1,1,1);
                int month = date1.Month;
                int year = date1.Year;
                var infoCalendaInsert = new InfoCalendar();
                infoCalendaInsert.MonthI = month;
                infoCalendaInsert.YearI = year;
                infoCalendaInsert.CreateAt = DateTime.Now;
                infoCalendaInsert.CreateUser = userId;
                infoCalendaInsert.DeleteFlag = false;
                var relsut = await _unitOfWork.Repository<InfoCalendar>().AddAsync(infoCalendaInsert);
                await _unitOfWork.SaveChangeAsync();
                //Thêm từng ca
                //ca 1
                var infoDetailCalenda1 = new InfoDetailCalendar();
                infoDetailCalenda1.CalendarId = relsut.Entity.CalendarId;
                infoDetailCalenda1.DayI = date1.Day;
                infoDetailCalenda1.ShiftI = 1;
                infoDetailCalenda1.StaffId = 1;
                infoDetailCalenda1.CreateAt = DateTime.Now;
                infoDetailCalenda1.CreateUser = userId;
                infoDetailCalenda1.DeleteFlag = false;
                await _unitOfWork.Repository<InfoDetailCalendar>().AddAsync(infoDetailCalenda1);
                await _unitOfWork.SaveChangeAsync();
                //ca 2
                var infoDetailCalenda2 = new InfoDetailCalendar();
                infoDetailCalenda2.CalendarId = relsut.Entity.CalendarId;
                infoDetailCalenda2.DayI = date1.Day;
                infoDetailCalenda2.ShiftI = 2;
                infoDetailCalenda2.StaffId = 1;
                infoDetailCalenda2.CreateAt = DateTime.Now;
                infoDetailCalenda2.CreateUser = userId;
                infoDetailCalenda2.DeleteFlag = false;
                await _unitOfWork.Repository<InfoDetailCalendar>().AddAsync(infoDetailCalenda2);
                await _unitOfWork.SaveChangeAsync();
                for (int i = 0; i < 14; i++)
                {
                    date1.AddDays(1);
                    if (date1.Month != month || date1.Year != year)
                    {
                        var infoCalendaInsert1 = new InfoCalendar();
                        infoCalendaInsert1.MonthI = date1.Month;
                        infoCalendaInsert1.YearI = date1.Year;
                        infoCalendaInsert1.CreateAt = DateTime.Now;
                        infoCalendaInsert1.CreateUser = userId;
                        infoCalendaInsert1.DeleteFlag = false;
                        relsut = await _unitOfWork.Repository<InfoCalendar>().AddAsync(infoCalendaInsert1);
                        await _unitOfWork.SaveChangeAsync();
                        month = date1.Month;
                        year = date1.Year;
                    }
                    else
                    {
                        //Thêm từng ca
                        //ca 1
                        var infoDetailCalenda3 = new InfoDetailCalendar();
                        infoDetailCalenda3.CalendarId = relsut.Entity.CalendarId;
                        infoDetailCalenda3.DayI = date1.Day;
                        infoDetailCalenda3.ShiftI = 1;
                        infoDetailCalenda3.StaffId = 1;
                        infoDetailCalenda3.CreateAt = DateTime.Now;
                        infoDetailCalenda3.CreateUser = userId;
                        infoDetailCalenda3.DeleteFlag = false;
                        await _unitOfWork.Repository<InfoDetailCalendar>().AddAsync(infoDetailCalenda3);
                        await _unitOfWork.SaveChangeAsync();
                        //ca 2
                        var infoDetailCalenda4 = new InfoDetailCalendar();
                        infoDetailCalenda4.CalendarId = relsut.Entity.CalendarId;
                        infoDetailCalenda4.DayI = date1.Day;
                        infoDetailCalenda4.ShiftI = 2;
                        infoDetailCalenda4.StaffId = 1;
                        infoDetailCalenda4.CreateAt = DateTime.Now;
                        infoDetailCalenda4.CreateUser = userId;
                        infoDetailCalenda4.DeleteFlag = false;
                        await _unitOfWork.Repository<InfoDetailCalendar>().AddAsync(infoDetailCalenda4);
                        await _unitOfWork.SaveChangeAsync();
                    }
                }

            }
            _unitOfWork.SaveChange();
            return true;
        }
    }
}
