using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoProvince
    {
        public InfoProvince()
        {
            InfoAddressDeliveryUsers = new HashSet<InfoAddressDeliveryUser>();
            InfoDistricts = new HashSet<InfoDistrict>();
            InfoStaffs = new HashSet<InfoStaff>();
            InfoSuppliers = new HashSet<InfoSupplier>();
            InfoUsers = new HashSet<InfoUser>();
        }

        public int ProvinceId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual ICollection<InfoAddressDeliveryUser> InfoAddressDeliveryUsers { get; set; }
        public virtual ICollection<InfoDistrict> InfoDistricts { get; set; }
        public virtual ICollection<InfoStaff> InfoStaffs { get; set; }
        public virtual ICollection<InfoSupplier> InfoSuppliers { get; set; }
        public virtual ICollection<InfoUser> InfoUsers { get; set; }
    }
}
