using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoDistrict
    {
        public InfoDistrict()
        {
            InfoAddressDeliveryUsers = new HashSet<InfoAddressDeliveryUser>();
            InfoStaffs = new HashSet<InfoStaff>();
            InfoSuppliers = new HashSet<InfoSupplier>();
            InfoUsers = new HashSet<InfoUser>();
        }

        public int DistrictId { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public int? ProvinceId { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoProvince Province { get; set; }
        public virtual ICollection<InfoAddressDeliveryUser> InfoAddressDeliveryUsers { get; set; }
        public virtual ICollection<InfoStaff> InfoStaffs { get; set; }
        public virtual ICollection<InfoSupplier> InfoSuppliers { get; set; }
        public virtual ICollection<InfoUser> InfoUsers { get; set; }
    }
}
