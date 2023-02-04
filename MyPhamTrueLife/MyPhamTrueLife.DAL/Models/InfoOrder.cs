using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoOrder
    {
        public InfoOrder()
        {
            InfoExchangeProducts = new HashSet<InfoExchangeProduct>();
            InfoOrderDetails = new HashSet<InfoOrderDetail>();
        }

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
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoAddressDeliveryUser AddressDelivery { get; set; }
        public virtual InfoSever Sever { get; set; }
        public virtual InfoStaff Staff { get; set; }
        public virtual InfoUser User { get; set; }
        public virtual ICollection<InfoExchangeProduct> InfoExchangeProducts { get; set; }
        public virtual ICollection<InfoOrderDetail> InfoOrderDetails { get; set; }
    }
}
