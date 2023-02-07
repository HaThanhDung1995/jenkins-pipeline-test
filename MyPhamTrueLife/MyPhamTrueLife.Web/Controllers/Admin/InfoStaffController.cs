using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using MyPhamTrueLife.Web.Base;
using MyPhamTrueLife.Web.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhamTrueLife.Web.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InfoStaffController : BaseApiController
    {
        private readonly IInfoStaffService _infoStaff;
        public InfoStaffController(IInfoStaffService infoStaff)
        {
            _infoStaff = infoStaff;
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Post([FromBody] InfoStaff value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoStaff.InsertStaffAsync(value, userId);
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Thêm nhân viên thất bại", result.ToString());
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Update([FromBody] InfoStaff value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoStaff.UpdateStaffAsync(value, userId);
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Cập nhật nhân viên thất bại", result.ToString());
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Delete(int staffId, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoStaff.DeleteStaffAsync(staffId, userId);
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("list")]
        [AllowAnonymous]
        public async Task<ResponseResult<ResponseList>> list(int page = 1, int limit = 25)
        {
            try
            {
                var result = await _infoStaff.ListStaffAsync(page, limit);
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, "Danh sách loại người dùng.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }


        [Route("detail")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<InfoStaffReq>> Detail(int staffId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoStaff.DetailStaffAsync(staffId);
                return new ResponseResult<InfoStaffReq>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InfoStaffReq>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
