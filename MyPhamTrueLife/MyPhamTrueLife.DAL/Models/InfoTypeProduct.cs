using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoTypeProduct
    {
        public InfoTypeProduct()
        {
            InfoProducts = new HashSet<InfoProduct>();
            InfoSuppliers = new HashSet<InfoSupplier>();
        }

        public int TypeProductId { get; set; }
        public string TypePeoductName { get; set; }
        public int? ProductPortfolioId { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoProductPortfolio ProductPortfolio { get; set; }
        public virtual ICollection<InfoProduct> InfoProducts { get; set; }
        public virtual ICollection<InfoSupplier> InfoSuppliers { get; set; }
    }
}
