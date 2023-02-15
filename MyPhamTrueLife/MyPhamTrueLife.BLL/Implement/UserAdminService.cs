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
    public class UserAdminService : IUserAdminService
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

        public async Task<bool> TaoLichLamViec(int userId, int? month, int? year)
        {
            if (userId <= 0)
            {
                return false;
            }
            if (month == null || year == null)
            {
                var date = DateTime.Now;
                month = date.Month;
                year = date.Year;
            }
            
            //Tất cả ngày trong tháng
            var tmp = DateTime.DaysInMonth(year.Value, month.Value);


            var infoCalenda = await _unitOfWork.Repository<InfoCalendar>().Where(x => x.DeleteFlag != true && x.YearI == year && x.MonthI == month).AsNoTracking().FirstOrDefaultAsync();
            if (infoCalenda == null)
            {
                var infoCalendaInsert = new InfoCalendar();
                infoCalendaInsert.MonthI = month;
                infoCalendaInsert.YearI = year;
                infoCalendaInsert.CreateAt = DateTime.Now;
                infoCalendaInsert.CreateUser = userId;
                infoCalendaInsert.DeleteFlag = false;
                var result = await _unitOfWork.Repository<InfoCalendar>().AddAsync(infoCalendaInsert);
                await _unitOfWork.SaveChangesAsync();

                for (int i = 1; i <= tmp; i++)
                {
                    var infoDetailCalenda = new InfoDetailCalendar();
                    infoDetailCalenda.CalendarId = infoCalendaInsert.CalendarId;
                    infoDetailCalenda.DayI = i;
                    infoDetailCalenda.ShiftI = 1;
                    infoDetailCalenda.StaffId = 1;
                    infoDetailCalenda.CreateAt = DateTime.Now;
                    infoDetailCalenda.CreateUser = userId;
                    infoDetailCalenda.DeleteFlag = false;
                    await _unitOfWork.Repository<InfoDetailCalendar>().AddAsync(infoDetailCalenda);
                    await _unitOfWork.SaveChangesAsync();

                    var infoDetailCalenda1 = new InfoDetailCalendar();
                    infoDetailCalenda1.CalendarId = infoCalendaInsert.CalendarId;
                    infoDetailCalenda1.DayI = i;
                    infoDetailCalenda1.ShiftI = 2;
                    infoDetailCalenda1.StaffId = 1;
                    infoDetailCalenda1.CreateAt = DateTime.Now;
                    infoDetailCalenda1.CreateUser = userId;
                    infoDetailCalenda1.DeleteFlag = false;
                    await _unitOfWork.Repository<InfoDetailCalendar>().AddAsync(infoDetailCalenda1);
                    await _unitOfWork.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<ResponseList> LayLichLamViecChoCaHai(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listInfo = await _unitOfWork.Repository<InfoCalendar>().Where(x => x.DeleteFlag != true).OrderByDescending(z => z.CreateAt).AsNoTracking().ToListAsync();
            var totalRows = listInfo.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listInfo = listInfo.Skip(start).Take(limit).ToList();
            listData.ListData = listInfo;
            return listData;
        }

        public async Task<ResponseList> XemChiTietLichLamViecChoCaHai(int userId, int? calendarId, int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var result = new List<DanhSachLichLamViecNes>();
            var infoStaff = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId == userId).AsNoTracking().FirstOrDefaultAsync();
            if (infoStaff != null)
            {
                int? month = null;
                int? year = null;
                var calendar = await _unitOfWork.Repository<InfoCalendar>().Where(x => x.DeleteFlag != true && x.CalendarId == calendarId).AsNoTracking().FirstOrDefaultAsync();
                if (calendar == null)
                {
                    month = DateTime.Now.Month;
                    year = DateTime.Now.Year;
                }
                else
                {
                    month = calendar.MonthI;
                    year = calendar.YearI;
                }
                var infoDetailCalendar = await _unitOfWork.Repository<InfoDetailCalendar>().Where(x => x.DeleteFlag != true && x.CalendarId == calendar.CalendarId).AsNoTracking().ToListAsync();
                foreach (var item in infoDetailCalendar)
                {
                    if (item.StaffId != null)
                    {
                        var infoStaff1 = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId == item.StaffId).AsNoTracking().FirstOrDefaultAsync();
                        if (infoStaff1 != null)
                        {
                            var info = new DanhSachLichLamViecNes();
                            info.StaffId = infoStaff1.StaffId;
                            info.StaffName = infoStaff1.FullName;
                            info.PositionStaffId = infoStaff1.PositionStaffId.Value;
                            info.PositionStaffName = infoStaff1.PositionStaffId == null ? "" : await _unitOfWork.Repository<InfoPositionStaff>().Where(x => x.DeleteFlag != true && x.PositionStaffId == infoStaff1.PositionStaffId).AsNoTracking().Select(z => z.PositionStaffName).FirstOrDefaultAsync();
                            info.DayI = item.DayI.Value;
                            info.ShiftI = item.ShiftI;
                            info.IsDo = item.IsDo;
                            info.StartAt = item.StartAt;
                            info.EndAt = item.EndAt;
                            result.Add(info);
                        }
                    }
                }
                if (infoStaff.PositionStaffId != null)
                {
                    if (infoStaff.PositionStaffId == 1 || infoStaff.PositionStaffId == 3 || infoStaff.PositionStaffId == 5 || infoStaff.PositionStaffId == 6)
                    {
                        if (infoStaff.PositionStaffId == 1)
                        {
                            result = result.Where(x => x.StaffId != 1).ToList();
                        }
                        if (infoStaff.PositionStaffId == 3 || infoStaff.PositionStaffId == 5 || infoStaff.PositionStaffId == 6)
                        {
                            result = result.Where(x => x.StaffId == infoStaff.StaffId).ToList();
                        }
                        var totalRows = result.Count();
                        listData.Paging = new Paging(totalRows, page, limit);
                        int start = listData.Paging.start;
                        result = result.Skip(start).Take(limit).ToList();
                        listData.ListData = result;
                    }
                }
            }
            return listData;
        }

        public async Task<List<LichTaoViecChoAdmin>> LayLichLamDeDangKy(int calendarId)
        {
            var listInfo = new List<LichTaoViecChoAdmin>();
            var infoCalenda = await _unitOfWork.Repository<InfoCalendar>().Where(x => x.DeleteFlag != true && x.CalendarId == calendarId).AsNoTracking().FirstOrDefaultAsync();
            if (infoCalenda == null)
            {
                return listInfo;
            }
            var tmp = DateTime.DaysInMonth(infoCalenda.YearI.Value, infoCalenda.MonthI.Value);
            for (int i = 1; i <= tmp; i++)
            {
                var infoDetail = await _unitOfWork.Repository<InfoDetailCalendar>().Where(x => x.DeleteFlag != true && x.DayI == i && x.CalendarId == infoCalenda.CalendarId).AsNoTracking().ToListAsync();
                if (infoDetail != null && infoDetail.Count > 0)
                {
                    var info = new LichTaoViecChoAdmin();
                    info.infoDetailCalendar = infoDetail.FirstOrDefault(z => z.ShiftI == 1);
                    info.DetailCalendarId = info.infoDetailCalendar.DetailCalendarId;
                    if (infoDetail.Count >= 5)
                    {
                        info.Status = true;
                    }
                    else
                    {
                        info.Status = false;
                    }
                    listInfo.Add(info);
                    var info1 = new LichTaoViecChoAdmin();
                    info1.infoDetailCalendar = infoDetail.FirstOrDefault(z => z.ShiftI == 2);
                    info1.DetailCalendarId = info.infoDetailCalendar.DetailCalendarId;
                    if (infoDetail.Count >= 5)
                    {
                        info1.Status = true;
                    }
                    else
                    {
                        info1.Status = false;
                    }
                    listInfo.Add(info1);
                }
            }
            return listInfo;
        }

        public async Task<int> DangKyLichLamViecCuaNhanVien(InfoDetailCalendar value, int staffId)
        {
            if (value == null)
            {
                return 1;
            }
            var infoStaff = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId == staffId).AsNoTracking().FirstOrDefaultAsync();
            if (infoStaff == null)
            {
                return 2;
            }
            var listInfoStaffId = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.PositionStaffId == infoStaff.PositionStaffId).AsNoTracking().Select(z => z.StaffId).ToListAsync();
            var infoDetailCalendar = await _unitOfWork.Repository<InfoDetailCalendar>().Where(x => x.DeleteFlag != true && listInfoStaffId.Contains(x.StaffId.Value) && x.CalendarId == value.CalendarId && x.DayI == value.DayI && x.ShiftI == value.ShiftI).AsNoTracking().CountAsync();
            if (infoStaff.PositionStaffId == 3)
            {
                //Nhân viên bán hàng
                if (infoDetailCalendar == 2)
                {
                    return -1;
                }
            }
            if (infoStaff.PositionStaffId == 5)
            {
                //Nhân viên kho
                if (infoDetailCalendar == 2)
                {
                    return -1;
                }
            }
            if (infoStaff.PositionStaffId == 6)
            {
                //Thu ngân
                if (infoDetailCalendar == 1)
                {
                    return -1;
                }
            }
            var infoDetailCalenda1 = new InfoDetailCalendar();
            infoDetailCalenda1.CalendarId = value.CalendarId;
            infoDetailCalenda1.DayI = value.DayI;
            infoDetailCalenda1.ShiftI = value.ShiftI;
            infoDetailCalenda1.StaffId = staffId;
            infoDetailCalenda1.CreateAt = DateTime.Now;
            infoDetailCalenda1.CreateUser = staffId;
            infoDetailCalenda1.DeleteFlag = false;
            await _unitOfWork.Repository<InfoDetailCalendar>().AddAsync(infoDetailCalenda1);
            await _unitOfWork.SaveChangesAsync();
            var infoDetailCalenda1r = await _unitOfWork.Repository<InfoDetailCalendar>().Where(x => x.DeleteFlag != true && x.CalendarId == value.CalendarId && x.DayI == value.DayI && x.ShiftI == x.ShiftI).AsNoTracking().CountAsync();
            if (infoDetailCalenda1r >= 5)
            {
                var infoDetailCalenda1r1 = await _unitOfWork.Repository<InfoDetailCalendar>().Where(x => x.DeleteFlag != true && x.CalendarId == value.CalendarId && x.DayI == value.DayI && x.ShiftI == x.ShiftI && x.StaffId == 1).AsNoTracking().FirstOrDefaultAsync();
                infoDetailCalenda1r1.DeleteFlag = true;
                _unitOfWork.Repository<InfoDetailCalendar>().UpdateRange(infoDetailCalenda1r1);
                await _unitOfWork.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<List<LichLamViecCuaNhanVien>> LayLichLamCuaNhanVien(int staffId, int? month, int? year)
        {
            var listInfo = new List<LichLamViecCuaNhanVien>();
            var infoStaff = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId == staffId).AsNoTracking().FirstOrDefaultAsync();
            if (infoStaff != null)
            {
                var staff = new InfoStaffReqNew();
                PropertyCopier<InfoStaff, InfoStaffReqNew>.Copy(infoStaff, staff);

                var tmp = DateTime.DaysInMonth(year.Value, month.Value);
                var infoCalenda = await _unitOfWork.Repository<InfoCalendar>().Where(x => x.DeleteFlag != true && x.MonthI == month.Value && x.YearI == year.Value).AsNoTracking().FirstOrDefaultAsync();

                if (infoCalenda != null)
                {
                    for (int i = 1; i <= tmp; i++)
                    {
                        var infoDetail = await _unitOfWork.Repository<InfoDetailCalendar>().Where(x => x.DeleteFlag != true && x.DayI == i && x.CalendarId == infoCalenda.CalendarId && x.StaffId == staff.StaffId).AsNoTracking().ToListAsync();
                        if (infoDetail != null && infoDetail.Count > 0)
                        {
                            foreach (var item in infoDetail)
                            {
                                var info = new LichLamViecCuaNhanVien();
                                info.infoStaffReq = staff;
                                info.DetailCalendarId = item.DetailCalendarId;
                                info.CalendarId = item.CalendarId;
                                info.DayI = item.DayI;
                                info.ShiftI = item.ShiftI;
                                info.StaffId = item.StaffId;
                                info.IsDo = item.IsDo;
                                info.StartAt = item.StartAt;
                                info.EndAt = item.EndAt;
                                listInfo.Add(info);
                            }
                        }
                    }
                }
            }
            return listInfo;
        }

        public async Task<int> DiemDanhVaKetCaCuaNhanVien(int? staffId, int? detailcalendarId, bool? IsAtendan)
        {
            if (staffId == null || detailcalendarId == null)
            {
                return 1;
            }
            var infoStaff = await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId == staffId.Value).AsNoTracking().FirstOrDefaultAsync();
            if (infoStaff == null)
            {
                return 1;
            }
            var infoDetailStaff = await _unitOfWork.Repository<InfoDetailCalendar>().Where(x => x.DeleteFlag != true && x.DetailCalendarId == detailcalendarId.Value).AsNoTracking().FirstOrDefaultAsync();
            if (infoDetailStaff == null)
            {
                return 1;
            }
            var infoCalendar = await _unitOfWork.Repository<InfoCalendar>().Where(x => x.DeleteFlag != true && x.CalendarId == infoDetailStaff.CalendarId).AsNoTracking().FirstOrDefaultAsync();
            if (infoCalendar == null)
            {
                return 1;
            }
            if (IsAtendan == true)
            {
                if (infoDetailStaff.StartAt != null)
                {
                    return 2;
                }
                DateTime date = new DateTime(infoCalendar.YearI.Value, infoCalendar.MonthI.Value, infoDetailStaff.DayI.Value, 07, 00, 00);
                if (infoDetailStaff.ShiftI == 2)
                {
                    date = new DateTime(infoCalendar.YearI.Value, infoCalendar.MonthI.Value, infoDetailStaff.DayI.Value, 12, 00, 00);
                }
                if (DateTime.Now < date || DateTime.Now.Date != date.Date)
                {
                    return 3;
                }
                infoDetailStaff.StartAt = DateTime.Now;
                infoDetailStaff.UpdateAt = DateTime.Now;
                infoDetailStaff.UpdateUser = staffId;
                infoDetailStaff.DeleteFlag = false;
            }
            else
            {
                if (infoDetailStaff.StartAt == null)
                {
                    return 4;
                }
                DateTime date = new DateTime(infoCalendar.YearI.Value, infoCalendar.MonthI.Value, infoDetailStaff.DayI.Value, 12, 00, 00);
                if (infoDetailStaff.ShiftI == 2)
                {
                    date = new DateTime(infoCalendar.YearI.Value, infoCalendar.MonthI.Value, infoDetailStaff.DayI.Value, 17, 00, 00);
                }
                if (DateTime.Now < date || DateTime.Now.Date != date.Date)
                {
                    return 3;
                }
                infoDetailStaff.EndAt = DateTime.Now;
                infoDetailStaff.UpdateAt = DateTime.Now;
                infoDetailStaff.IsDo = true;
                infoDetailStaff.UpdateUser = staffId;
                infoDetailStaff.DeleteFlag = false;
            }
            _unitOfWork.Repository<InfoDetailCalendar>().UpdateRange(infoDetailStaff);
            await _unitOfWork.SaveChangeAsync();
            return 0;
        }
    }
}
