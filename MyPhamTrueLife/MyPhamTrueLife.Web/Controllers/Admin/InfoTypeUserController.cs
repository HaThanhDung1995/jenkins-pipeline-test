using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPhamTrueLife.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.Web.Models.Response;
using MyPhamTrueLife.DAL.Models.Utils;
using MyPhamTrueLife.Web.Base;
using Microsoft.AspNetCore.Authorization;

namespace MyPhamTrueLife.Web.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InfoTypeUserController : BaseApiController
    {
        private readonly IInfoTypeUserService _infoTypeUser;
        public InfoTypeUserController(IInfoTypeUserService infoTypeUser)
        {
            _infoTypeUser = infoTypeUser;
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Post([FromBody] InfoTypeUserReq value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoTypeUser.InsertTypeUserAsync(value, userId);
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
        public async Task<ResponseResult<string>> Update([FromBody] InfoTypeUserReq value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoTypeUser.UpdateTypeUserAsync(value, userId);
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
        public async Task<ResponseResult<string>> Delete(int typeUserId, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoTypeUser.DeleteTypeUserAsync(typeUserId, userId);
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [Route("detail")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<InfoTypeUser>> Detail(int typeUserId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoTypeUser.DetailTypeUserAsync(typeUserId);
                return new ResponseResult<InfoTypeUser>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InfoTypeUser>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("list")]
        [AllowAnonymous]
        public async Task<ResponseResult<ResponseList>> ListInfoTypeUserAsync(int page = 1, int limit = 25)
        {
            try
            {
                var result = await _infoTypeUser.ListTypeUserAsync(page, limit);
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, "Danh sách loại người dùng.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}

