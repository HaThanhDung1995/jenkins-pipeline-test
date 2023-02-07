using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoSever
    {
        public InfoSever()
        {
            InfoOrders = new HashSet<InfoOrder>();
        }

        public int SeverId { get; set; }
        public string SeverName { get; set; }
        public string Image { get; set; }
        public int? Prize { get; set; }
        public string Describe { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual ICollection<InfoOrder> InfoOrders { get; set; }
    }
}
