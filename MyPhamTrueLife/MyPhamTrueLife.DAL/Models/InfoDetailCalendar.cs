using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoDetailCalendar
    {
        public int CalendarId { get; set; }
        public int DayI { get; set; }
        public int ShiftI { get; set; }
        public int StaffId { get; set; }
        public bool? IsDo { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoCalendar Calendar { get; set; }
        public virtual InfoStaff Staff { get; set; }
    }
}
