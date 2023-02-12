using MyPhamTrueLife.DAL.Models1;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL.Models.Utils
{
    public class InfoOrderReq
    {
    }
    public class InfoOrderInsertRequest
    {
        public int? UserId { get; set; }
        public int? Total { get; set; }
        public int? ServerId { get; set; }
        public int? AddressDeliveryId { get; set; }
        public bool? IsPay { get; set; }
        public List<InfoCart> listCart { get; set; }
    }

    public class InfoOrderList : InfoOrder
    {
        public string FullName { get; set; }
        public string FullNameStaff { get; set; }
        //Thông tin user
        public InfoUser infoUser { get; set; }
        //Thông tin nhân viên
        public InfoStaff infoStaff { get; set; }
        //Thông tin địa chỉ giao hàng
        public IndoAddressDeliveryReq infoAddressDeliveryUser { get; set; }
    }

    public class InfoOrderUpdateStatus
    {
        public string Status { get; set; }
        public bool? IsPay { get; set; }
        public int StaffId { get; set; } 
        public int OrderId { get; set; }
    }

    public class XemChiTietDonHangRes
    {
        //Thông tin đơn hàng
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public bool? IsAccept { get; set; }
        public int? StaffId { get; set; }
        public int? Total { get; set; }
        public string Status { get; set; }
        public bool? IsPay { get; set; }
        public string StatusOrder { get; set; }
        public int? SeverId { get; set; }
        public DateTime? DateTimeD { get; set; }
        public int? AddressDeliveryId { get; set; }
        //Thông tin user
        public InfoUser infoUser { get; set; }
        //Thông tin nhân viên
        public InfoStaff infoStaff { get; set; }
        //Thông tin địa chỉ giao hàng
        public InfoAddressDeliveryUser infoAddressDeliveryUser { get; set; }
        //Thông tin dịch vụ
        public InfoSever infoSever { get; set; }
        //danh sách chi tiết đơn hàng
        public List<ThongTinChiTietDonHang> thongTinChiTietDonHangs { get; set; }
    }

    public class ThongTinChiTietDonHang
    {
        public int OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Amount { get; set; }
        public int? Prize { get; set; }
        public int? CapacityId { get; set; }
        public string CapacityName { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string Avatar { get; set; }
        public string ProductName { get; set; }
    }
}
