using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models1;
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

        [HttpPost]
        [Route("tao-lich-lam-viec")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> TaoLichLamViec(int userId, int? month, int? year)
        {
            try
            {
                
                var result = await _userAdmin.TaoLichLamViec(userId, month, year);
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Tạo lịch làm việc thất bại", null);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Tạo lịch làm việc thành công", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("lay-lich-lam-viec-admin")]
        [AllowAnonymous]
        public async Task<ResponseResult<List<LichTaoViecChoAdmin>>> LayLichLamViecAdmin(DateTime? dateTime)
        {
            try
            {
                var result = await _userAdmin.LayLichLamDeDangKy(dateTime);
                return new ResponseResult<List<LichTaoViecChoAdmin>>(RetCodeEnum.Ok, "Lấy lịch làm việc thành công", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<LichTaoViecChoAdmin>>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("dang-ky-lich-lam-viec-admin")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> DangKyLichLamViecAdmin(InfoDetailCalendar value, int staffId)
        {
            try
            {
                var result = await _userAdmin.DangKyLichLamViecCuaNhanVien(value, staffId);
                if (result == 1)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Đăng ký lịch làm việc thất bại, thông tin truyền vào bị trống", result.ToString());
                }
                if (result == 2)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Đăng ký lịch làm việc thất bại, thông tin truyền vào bị trống", result.ToString());
                }
                if (result == -1)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Đăng ký lịch làm việc thất bại, loại nhân viên đã đủ cho ca làm việc", result.ToString());
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Đăng ký lịch làm việc thành công", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("lay-lich-lam-viec-cho-nhan-vien")]
        [AllowAnonymous]
        public async Task<ResponseResult<List<LichLamViecCuaNhanVien>>> LayLichLamViecChoNhanVien(int staffId, DateTime? dateAt)
        {
            try
            {
                var result = await _userAdmin.LayLichLamCuaNhanVien(staffId, dateAt);
                return new ResponseResult<List<LichLamViecCuaNhanVien>>(RetCodeEnum.Ok, "Lấy lịch làm việc thành công", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<LichLamViecCuaNhanVien>>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
