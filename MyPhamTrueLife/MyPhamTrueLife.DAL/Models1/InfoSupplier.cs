using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoSupplier
    {
        public InfoSupplier()
        {
            InfoImportSells = new HashSet<InfoImportSell>();
            InfoProductOutOfTimes = new HashSet<InfoProductOutOfTime>();
            InfoRefundProducts = new HashSet<InfoRefundProduct>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Adrress { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? TypeProductId { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoDistrict District { get; set; }
        public virtual InfoProvince Province { get; set; }
        public virtual InfoTypeProduct TypeProduct { get; set; }
        public virtual ICollection<InfoImportSell> InfoImportSells { get; set; }
        public virtual ICollection<InfoProductOutOfTime> InfoProductOutOfTimes { get; set; }
        public virtual ICollection<InfoRefundProduct> InfoRefundProducts { get; set; }
    }
}
