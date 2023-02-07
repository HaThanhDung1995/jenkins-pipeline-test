using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoPositionStaff
    {
        public InfoPositionStaff()
        {
            InfoStaffs = new HashSet<InfoStaff>();
        }

        public int PositionStaffId { get; set; }
        public string PositionStaffName { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual ICollection<InfoStaff> InfoStaffs { get; set; }
    }
}
