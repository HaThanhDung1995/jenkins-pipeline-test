using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoDetailImportSell
    {
        public int ImportSellId { get; set; }
        public int ProductId { get; set; }
        public int? Amount { get; set; }
        public int? Prize { get; set; }
        public int? CapacityId { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public bool? IsExpiry { get; set; }
        public string Trademark { get; set; }
        public bool? IsColor { get; set; }
        public bool? IsAroma { get; set; }
        public bool? IsNomal { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoCapacity Capacity { get; set; }
        public virtual InfoImportSell ImportSell { get; set; }
        public virtual InfoProduct Product { get; set; }
    }
}
