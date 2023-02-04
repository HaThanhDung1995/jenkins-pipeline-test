using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoTypeNature
    {
        public InfoTypeNature()
        {
            InfoNatures = new HashSet<InfoNature>();
        }

        public int TypeNatureId { get; set; }
        public string TypeNatureName { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual ICollection<InfoNature> InfoNatures { get; set; }
    }
}
