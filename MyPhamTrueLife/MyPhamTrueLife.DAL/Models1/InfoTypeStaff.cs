using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoTypeStaff
    {
        public InfoTypeStaff()
        {
            InfoStaffs = new HashSet<InfoStaff>();
        }

        public int TypeStaffId { get; set; }
        public string TypeStaffName { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual ICollection<InfoStaff> InfoStaffs { get; set; }
    }
}
