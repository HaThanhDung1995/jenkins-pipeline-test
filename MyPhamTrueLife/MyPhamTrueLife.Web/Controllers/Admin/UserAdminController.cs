using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using MyPhamTrueLife.Web.Base;
using MyPhamTrueLife.Web.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyPhamTrueLife.Web.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserAdminController : BaseApiController
    {
        private readonly IUserAdminService _userAdmin;
        private readonly IConfiguration _config;
        public UserAdminController(IUserAdminService userAdmin, IConfiguration config)
        {
            _userAdmin = userAdmin;
            _config = config;
        }
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ResponseResult<LoginResponse>> Login([FromBody] Login user)
        {
            try
            {
                if (user == null)
                {
                    return new ResponseResult<LoginResponse>(RetCodeEnum.ApiError, "Tài khoản và mật khẩu đăng nhập không được trống.", null);
                }
                if (string.IsNullOrEmpty(user.UserName))
                {
                    return new ResponseResult<LoginResponse>(RetCodeEnum.ApiError, "Tài khoản đăng nhập không được trống.", null);
                }
                if (string.IsNullOrEmpty(user.PassWord))
                {
                    return new ResponseResult<LoginResponse>(RetCodeEnum.ApiError, "Mật khẩu đăng nhập không được trống.", null);
                }
                var result = await _userAdmin.LoginAsync(user);
                if (result == null)
                {
                    return new ResponseResult<LoginResponse>(RetCodeEnum.ApiError, "Tài khoản và mật khẩu không chính xác.", null);
                }
                //result.Token = CreateToken(result);
                return new ResponseResult<LoginResponse>(RetCodeEnum.Ok, "Đăng nhập thành công.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<LoginResponse>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
