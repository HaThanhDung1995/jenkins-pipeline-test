using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoNature
    {
        public InfoNature()
        {
            InfoProducts = new HashSet<InfoProduct>();
        }

        public int NatureId { get; set; }
        public string NatureName { get; set; }
        public int? TypeNatureId { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoTypeNature TypeNature { get; set; }
        public virtual ICollection<InfoProduct> InfoProducts { get; set; }
    }
}
