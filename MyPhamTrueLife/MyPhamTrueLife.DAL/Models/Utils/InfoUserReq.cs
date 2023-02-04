using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL.Models.Utils
{
    public class InfoUserReq
    {
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
        public int? CoefficientsSalary { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? TypeUserId { get; set; }
    }
}
