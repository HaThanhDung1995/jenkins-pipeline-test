using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoNotificationsStaff
    {
        public int NotificationsId { get; set; }
        public int? StaffId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool? Seen { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoStaff Staff { get; set; }
    }
}
