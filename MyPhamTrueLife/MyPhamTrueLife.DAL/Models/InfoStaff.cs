using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models
{
    public partial class InfoStaff
    {
        public InfoStaff()
        {
            InfoDetailCalendars = new HashSet<InfoDetailCalendar>();
            InfoExchangeProducts = new HashSet<InfoExchangeProduct>();
            InfoImportSells = new HashSet<InfoImportSell>();
            InfoNotificationsStaffs = new HashSet<InfoNotificationsStaff>();
            InfoOrders = new HashSet<InfoOrder>();
        }

        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }
        public int? Numberfactor { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? PositionStaffId { get; set; }
        public int? TypeStaffId { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoDistrict District { get; set; }
        public virtual InfoPositionStaff PositionStaff { get; set; }
        public virtual InfoProvince Province { get; set; }
        public virtual InfoTypeStaff TypeStaff { get; set; }
        public virtual ICollection<InfoDetailCalendar> InfoDetailCalendars { get; set; }
        public virtual ICollection<InfoExchangeProduct> InfoExchangeProducts { get; set; }
        public virtual ICollection<InfoImportSell> InfoImportSells { get; set; }
        public virtual ICollection<InfoNotificationsStaff> InfoNotificationsStaffs { get; set; }
        public virtual ICollection<InfoOrder> InfoOrders { get; set; }
    } 
}
