using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoOrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? Amount { get; set; }
        public int? Prize { get; set; }
        public int? CapacityId { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAd { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoCapacity Capacity { get; set; }
        public virtual InfoOrder Order { get; set; }
        public virtual InfoProduct Product { get; set; }
    }
}
