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
    public class InfoTypeNatureController : BaseApiController
    {
        //private readonly IInfoStaffService _infoStaff;
        private readonly IInfoTypeNatureService _infoTypeNatureService;
        public InfoTypeNatureController(IInfoTypeNatureService infoTypeNatureService)
        {
            //_infoStaff = infoStaff;
            _infoTypeNatureService = infoTypeNatureService;
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Post([FromBody] InfoTypeNature value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoTypeNatureService.InsertTypeNatureAsync(value, userId);
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
        public async Task<ResponseResult<string>> Update([FromBody] InfoTypeNature value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoTypeNatureService.UpdateTypeNatureAsync(value, userId);
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
                var result = await _infoTypeNatureService.DeleteTypeNatureAsync(staffId, userId);
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
                var result = await _infoTypeNatureService.ListTypeNatureAsync(page, limit);
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
        public async Task<ResponseResult<InfoTypeNature>> Detail(int staffId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoTypeNatureService.DetailTypeNatureAsync(staffId);
                return new ResponseResult<InfoTypeNature>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InfoTypeNature>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
