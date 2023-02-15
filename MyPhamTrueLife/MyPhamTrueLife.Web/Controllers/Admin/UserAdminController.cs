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
        [Route("lay-lich-lam-viec-de-dang-ky")]
        [AllowAnonymous]
        public async Task<ResponseResult<List<LichTaoViecChoAdmin>>> LayLichLamViecAdmin(int calendarId)
        {
            try
            {
                var result = await _userAdmin.LayLichLamDeDangKy(calendarId);
                return new ResponseResult<List<LichTaoViecChoAdmin>>(RetCodeEnum.Ok, "Lấy lịch làm việc thành công", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<LichTaoViecChoAdmin>>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("lay-danh-sach-lich-lam-viec")]
        [AllowAnonymous]
        public async Task<ResponseResult<ResponseList>> list(int page = 1, int limit = 25)
        {
            try
            {
                var result = await _userAdmin.LayLichLamViecChoCaHai(page, limit);
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, "Danh sách thời gian làm việc.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
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
                if (result == 3)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Đăng ký lịch làm việc thất bại, bạn đã đăng ký ca làm việc này rồi", result.ToString());
                }
                if (result == 4)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Đăng ký lịch làm việc thất bại, không đăng ký ngày quá khứ", result.ToString());
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
        public async Task<ResponseResult<List<LichLamViecCuaNhanVien>>> LayLichLamViecChoNhanVien(int staffId, int? month, int? year)
        {
            try
            {
                var result = await _userAdmin.LayLichLamCuaNhanVien(staffId, month, year);
                return new ResponseResult<List<LichLamViecCuaNhanVien>>(RetCodeEnum.Ok, "Lấy lịch làm việc thành công", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<LichLamViecCuaNhanVien>>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("xem-chi-tiet-lich-lam-viec")]
        [AllowAnonymous]
        public async Task<ResponseResult<ResponseList>> detail(int userId, int? calendarId, int page = 1, int limit = 25)
        {
            try
            {
                var result = await _userAdmin.XemChiTietLichLamViecChoCaHai(userId, calendarId, page, limit);
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, "Danh sách thời gian làm việc.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("diem-danh-vao-ra")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> DiemDanh(int? staffId, int? detailcalendarId, bool? IsAtendan)
        {
            try
            {
                var result = await _userAdmin.DiemDanhVaKetCaCuaNhanVien(staffId,detailcalendarId, IsAtendan);
                if (result == 1)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Điểm danh thất bại, thông tin bị trống", result.ToString());
                }
                if (result == 2)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Điểm danh thất bại, bạn đã điểm danh trước đó", result.ToString());
                }
                if (result == 3)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Thất bại, chưa đúng ngày", result.ToString());
                }
                if (result == 4)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Điểm danh thất bại, chưa tới giờ", result.ToString());
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Điểm danh thành công", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
