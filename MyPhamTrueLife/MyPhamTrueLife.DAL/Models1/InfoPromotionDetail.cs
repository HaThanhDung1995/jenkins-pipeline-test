using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoPromotionDetail
    {
        public int PromotionDetailId { get; set; }
        public int? ProductId { get; set; }
        public int? PromotionId { get; set; }
        public int? DiscountPercent { get; set; }
        public int? DiscountAmount { get; set; }
        public int? SubtotalAmount { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoProduct Product { get; set; }
        public virtual InfoPromotion Promotion { get; set; }
    }
}
