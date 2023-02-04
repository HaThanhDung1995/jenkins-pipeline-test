using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoChangeImportOrder
    {
        public int ChangeImportOrderId { get; set; }
        public int? ImportSellId { get; set; }
        public DateTime? Date { get; set; }
        public string Describe { get; set; }
        public string Status { get; set; }
        public string StatusOfSupplier { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoImportSell ImportSell { get; set; }
    }
}
