using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoTypeUser
    {
        public InfoTypeUser()
        {
            InfoUsers = new HashSet<InfoUser>();
        }

        public int TypeUserId { get; set; }
        public string TypeUserName { get; set; }
        public int? DiscountPercen { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual ICollection<InfoUser> InfoUsers { get; set; }
    }
}
