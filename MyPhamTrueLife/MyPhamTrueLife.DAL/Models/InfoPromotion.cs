using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoPromotion
    {
        public InfoPromotion()
        {
            InfoCarts = new HashSet<InfoCart>();
            InfoPromotionBuyBonus = new HashSet<InfoPromotionBuyBonu>();
            InfoPromotionDetails = new HashSet<InfoPromotionDetail>();
        }

        public int PromotionId { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public int? PromotionType { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoPromotionType PromotionTypeNavigation { get; set; }
        public virtual ICollection<InfoCart> InfoCarts { get; set; }
        public virtual ICollection<InfoPromotionBuyBonu> InfoPromotionBuyBonus { get; set; }
        public virtual ICollection<InfoPromotionDetail> InfoPromotionDetails { get; set; }
    }
}
