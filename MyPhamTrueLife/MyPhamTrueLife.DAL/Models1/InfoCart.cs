using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoCart
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? CapacityId { get; set; }
        public int? Quantity { get; set; }
        public int? PromotionId { get; set; }
        public string VoucherCode { get; set; }
        public int? Total { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoCapacity Capacity { get; set; }
        public virtual InfoProduct Product { get; set; }
        public virtual InfoPromotion Promotion { get; set; }
        public virtual InfoUser User { get; set; }
    }
}
