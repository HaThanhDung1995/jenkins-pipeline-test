using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoProduct
    {
        public InfoProduct()
        {
            InfoCapacityProducts = new HashSet<InfoCapacityProduct>();
            InfoCarts = new HashSet<InfoCart>();
            InfoComents = new HashSet<InfoComent>();
            InfoDetailImportSells = new HashSet<InfoDetailImportSell>();
            InfoEvaluates = new HashSet<InfoEvaluate>();
            InfoExpiryProducts = new HashSet<InfoExpiryProduct>();
            InfoImageProducts = new HashSet<InfoImageProduct>();
            InfoOrderDetails = new HashSet<InfoOrderDetail>();
            InfoPriceProducts = new HashSet<InfoPriceProduct>();
            InfoPromotionBuyBonus = new HashSet<InfoPromotionBuyBonu>();
            InfoPromotionDetails = new HashSet<InfoPromotionDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Describe { get; set; }
        public int? Amount { get; set; }
        public bool? IsExpiry { get; set; }
        public string StatusProduct { get; set; }
        public string Trademark { get; set; }
        public int? NatureId { get; set; }
        public bool? IsAroma { get; set; }
        public string Avatar { get; set; }
        public int? TypeProductId { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoNature Nature { get; set; }
        public virtual InfoTypeProduct TypeProduct { get; set; }
        public virtual InfoProductOutOfTime InfoProductOutOfTime { get; set; }
        public virtual InfoPromotionProduct InfoPromotionProduct { get; set; }
        public virtual InfoRefundProduct InfoRefundProduct { get; set; }
        public virtual ICollection<InfoCapacityProduct> InfoCapacityProducts { get; set; }
        public virtual ICollection<InfoCart> InfoCarts { get; set; }
        public virtual ICollection<InfoComent> InfoComents { get; set; }
        public virtual ICollection<InfoDetailImportSell> InfoDetailImportSells { get; set; }
        public virtual ICollection<InfoEvaluate> InfoEvaluates { get; set; }
        public virtual ICollection<InfoExpiryProduct> InfoExpiryProducts { get; set; }
        public virtual ICollection<InfoImageProduct> InfoImageProducts { get; set; }
        public virtual ICollection<InfoOrderDetail> InfoOrderDetails { get; set; }
        public virtual ICollection<InfoPriceProduct> InfoPriceProducts { get; set; }
        public virtual ICollection<InfoPromotionBuyBonu> InfoPromotionBuyBonus { get; set; }
        public virtual ICollection<InfoPromotionDetail> InfoPromotionDetails { get; set; }
    }
}
