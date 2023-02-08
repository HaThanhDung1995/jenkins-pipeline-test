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
    }

    public class InfoOrderUpdateStatus
    {
        public string Status { get; set; }
        public bool? IsPay { get; set; }
        public int StaffId { get; set; } 
        public int OrderId { get; set; }
    }
}
