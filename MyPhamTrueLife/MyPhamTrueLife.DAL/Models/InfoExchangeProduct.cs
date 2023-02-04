using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoExchangeProduct
    {
        public int ExchangeProductId { get; set; }
        public int? UserId { get; set; }
        public int? OrderId { get; set; }
        public bool? IsAccept { get; set; }
        public int? StaffId { get; set; }
        public bool? IsExchange { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoOrder Order { get; set; }
        public virtual InfoStaff Staff { get; set; }
        public virtual InfoUser User { get; set; }
    }
}
