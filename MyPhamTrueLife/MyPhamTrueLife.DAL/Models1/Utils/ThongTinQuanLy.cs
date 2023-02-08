using MyPhamTrueLife.DAL.Models1;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL.Models.Utils
{
    class ThongTinQuanLy
    {
    }

    public class ThongTinThemSanPham
    {
        public string ProductName { get; set; }
        public string Describe { get; set; }
        public bool? IsExpiry { get; set; }
        public string Trademark { get; set; }
        public int? NatureId { get; set; }
        public string Avatar { get; set; }
        public int? TypeProductId { get; set; }
        //Gía
        public int? Price { get; set; }
        //Hạn sử dụng
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        //danh sách hình ảnh
        public List<string> ListImage { get; set; }
        //danh sách dung tích
        public List<GiaTheoDungTichThem> ListCapacityPrice { get; set; }
    }
    public class GiaTheoDungTichThem
    {
        public int? Cappacity { get; set; }
        public int? Price { get; set; }
    }

    public class LichTaoViecChoAdmin
    {
        public InfoDetailCalendar infoDetailCalendar { get; set; }
        public bool? Status { get; set; }
    }

    public class LichLamViecCuaNhanVien
    {
        public int DetailCalendarId { get; set; }
        public int? CalendarId { get; set; }
        public int? DayI { get; set; }
        public int? ShiftI { get; set; }
        public int? StaffId { get; set; }
        public bool? IsDo { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public InfoStaffReqNew infoStaffReq { get; set; }
    }

    public class InfoStaffReqNew
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }
        public int? Numberfactor { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? PositionStaffId { get; set; }
        public int? TypeStaffId { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }
    }
}
