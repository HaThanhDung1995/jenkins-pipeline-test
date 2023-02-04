using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoCapacity
    {
        public InfoCapacity()
        {
            InfoCapacityProducts = new HashSet<InfoCapacityProduct>();
            InfoCarts = new HashSet<InfoCart>();
            InfoDetailImportSells = new HashSet<InfoDetailImportSell>();
            InfoExpiryProducts = new HashSet<InfoExpiryProduct>();
            InfoOrderDetails = new HashSet<InfoOrderDetail>();
            InfoPriceProducts = new HashSet<InfoPriceProduct>();
            InfoProductOutOfTimes = new HashSet<InfoProductOutOfTime>();
            InfoPromotionProducts = new HashSet<InfoPromotionProduct>();
            InfoRefundProducts = new HashSet<InfoRefundProduct>();
        }

        public int CapacityId { get; set; }
        public string CapacityName { get; set; }
        public string Unit { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual ICollection<InfoCapacityProduct> InfoCapacityProducts { get; set; }
        public virtual ICollection<InfoCart> InfoCarts { get; set; }
        public virtual ICollection<InfoDetailImportSell> InfoDetailImportSells { get; set; }
        public virtual ICollection<InfoExpiryProduct> InfoExpiryProducts { get; set; }
        public virtual ICollection<InfoOrderDetail> InfoOrderDetails { get; set; }
        public virtual ICollection<InfoPriceProduct> InfoPriceProducts { get; set; }
        public virtual ICollection<InfoProductOutOfTime> InfoProductOutOfTimes { get; set; }
        public virtual ICollection<InfoPromotionProduct> InfoPromotionProducts { get; set; }
        public virtual ICollection<InfoRefundProduct> InfoRefundProducts { get; set; }
    }
}
