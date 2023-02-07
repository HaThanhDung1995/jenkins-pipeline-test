using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoCalendar
    {
        public InfoCalendar()
        {
            InfoDetailCalendars = new HashSet<InfoDetailCalendar>();
        }

        public int CalendarId { get; set; }
        public int? MonthI { get; set; }
        public int? YearI { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual ICollection<InfoDetailCalendar> InfoDetailCalendars { get; set; }
    }
}
