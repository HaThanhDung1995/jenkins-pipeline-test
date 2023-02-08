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
}
