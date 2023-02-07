using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoProductPortfolio
    {
        public InfoProductPortfolio()
        {
            InfoTypeProducts = new HashSet<InfoTypeProduct>();
        }

        public int ProductPortfolioId { get; set; }
        public string ProductPortfolioName { get; set; }
        public string Describe { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual ICollection<InfoTypeProduct> InfoTypeProducts { get; set; }
    }
}
