using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoCapacityProduct
    {
        public int CapacityProductId { get; set; }
        public int? CapacityId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoCapacity Capacity { get; set; }
        public virtual InfoProduct Product { get; set; }
    }
}
