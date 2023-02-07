using System;
using System.Collections.Generic;

#nullable disable

namespace MyPhamTrueLife.DAL.Models1
{
    public partial class InfoUser
    {
        public InfoUser()
        {
            InfoAddressDeliveryUsers = new HashSet<InfoAddressDeliveryUser>();
            InfoCarts = new HashSet<InfoCart>();
            InfoComents = new HashSet<InfoComent>();
            InfoEvaluates = new HashSet<InfoEvaluate>();
            InfoExchangeProducts = new HashSet<InfoExchangeProduct>();
            InfoNotificationsUsers = new HashSet<InfoNotificationsUser>();
            InfoOrders = new HashSet<InfoOrder>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? TypeUserId { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? CreateUser { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? UpdateUser { get; set; }
        public bool? DeleteFlag { get; set; }

        public virtual InfoDistrict District { get; set; }
        public virtual InfoProvince Province { get; set; }
        public virtual InfoTypeUser TypeUser { get; set; }
        public virtual ICollection<InfoAddressDeliveryUser> InfoAddressDeliveryUsers { get; set; }
        public virtual ICollection<InfoCart> InfoCarts { get; set; }
        public virtual ICollection<InfoComent> InfoComents { get; set; }
        public virtual ICollection<InfoEvaluate> InfoEvaluates { get; set; }
        public virtual ICollection<InfoExchangeProduct> InfoExchangeProducts { get; set; }
        public virtual ICollection<InfoNotificationsUser> InfoNotificationsUsers { get; set; }
        public virtual ICollection<InfoOrder> InfoOrders { get; set; }
    }
}
