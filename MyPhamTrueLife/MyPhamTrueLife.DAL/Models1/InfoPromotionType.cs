using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoPromotionType
    {
        public InfoPromotionType()
        {
            InfoPromotions = new HashSet<InfoPromotion>();
        }

        public int PromotionType { get; set; }
        public string Name { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual ICollection<InfoPromotion> InfoPromotions { get; set; }
    }
}
