using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoAddressDeliveryUser
    {
        public InfoAddressDeliveryUser()
        {
            InfoOrders = new HashSet<InfoOrder>();
        }

        public int AddressDeliveryId { get; set; }
        public int? UserId { get; set; }
        public string Address { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public bool? DefaultAddress { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoDistrict District { get; set; }
        public virtual InfoProvince Province { get; set; }
        public virtual InfoUser User { get; set; }
        public virtual ICollection<InfoOrder> InfoOrders { get; set; }
    }

    public class IndoAddressDeliveryReq
    {
        public int AddressDeliveryId { get; set; }
        public int? UserId { get; set; }
        public string Address { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public bool? DefaultAddress { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
    }
}
