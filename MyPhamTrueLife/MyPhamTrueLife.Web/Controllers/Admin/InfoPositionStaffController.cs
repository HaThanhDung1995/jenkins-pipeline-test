using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models;
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
    public class InfoPositionStaffController : BaseApiController
    {
        //private readonly IInfoTypeStaffService _infoTypeStaff;
        private readonly IInfoPositionStaffService _infoPositionStaffService;
        public InfoPositionStaffController( IInfoPositionStaffService infoPositionStaffService)
        {
            _infoPositionStaffService = infoPositionStaffService;
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Post([FromBody] InfoPositionStaff value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoPositionStaffService.InsertPositionStaffAsync(value, userId);
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
        public async Task<ResponseResult<string>> Update([FromBody] InfoPositionStaff value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoPositionStaffService.UpdatePositionStaffAsync(value, userId);
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
        public async Task<ResponseResult<string>> Delete(int typeStaffId, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoPositionStaffService.DeletePositionStaffAsync(typeStaffId, userId);
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
                var result = await _infoPositionStaffService.ListPositionStaffAsync(page, limit);
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
        public async Task<ResponseResult<InfoPositionStaff>> Detail(int typeStaffId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoPositionStaffService.DetailPositionStaffAsync(typeStaffId);
                return new ResponseResult<InfoPositionStaff>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InfoPositionStaff>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
