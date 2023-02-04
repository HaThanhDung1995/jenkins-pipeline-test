using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL.Models.Utils
{
    public class UserReq
    {
    }
    public class Login
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
    public class LoginResponse
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
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string TypeUserName { get; set; }
        public int TypeUserId { get; set; }
        public int positionStaffId { get; set; }
        public string Token { get; set; }
    }

    public class RegisterUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
