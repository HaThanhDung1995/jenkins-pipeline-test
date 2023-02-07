using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoImportSell
    {
        public InfoImportSell()
        {
            InfoChangeImportOrders = new HashSet<InfoChangeImportOrder>();
            InfoDetailImportSells = new HashSet<InfoDetailImportSell>();
        }

        public int ImportSellId { get; set; }
        public int? SupplierId { get; set; }
        public bool? IsAccept { get; set; }
        public int? StaffId { get; set; }
        public int? Total { get; set; }
        public string Status { get; set; }
        public bool? IsPay { get; set; }
        public DateTime? DateTimeD { get; set; }
        public bool? IsAddTosTock { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoStaff Staff { get; set; }
        public virtual InfoSupplier Supplier { get; set; }
        public virtual ICollection<InfoChangeImportOrder> InfoChangeImportOrders { get; set; }
        public virtual ICollection<InfoDetailImportSell> InfoDetailImportSells { get; set; }
    }
}
