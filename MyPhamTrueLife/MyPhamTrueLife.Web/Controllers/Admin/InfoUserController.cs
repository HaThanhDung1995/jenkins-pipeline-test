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
using System.Linq;
using System.Threading.Tasks;

namespace MyPhamTrueLife.Web.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InfoUserController : BaseApiController
    {
        private readonly IUserService _user;
        private readonly IConfiguration _config;
        private readonly IInfoUserService _infoUserService;
        public InfoUserController(IUserService user, IConfiguration config, IInfoUserService infoUserService)
        {
            _infoUserService = infoUserService;
            _user = user;
            _config = config;
        }
        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Post([FromBody] InfoUserReq value, int userId)
        {
            try
            {
                var result = await _infoUserService.InsertInfoUserAssync(value, userId);
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        [HttpPut]
        [Route("update")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> UpdateInfoUserAsync([FromBody] InfoUser value)
        {
            try
            {
                var result = await _infoUserService.UpdateInfoUserAsync(value);
                if (result == false)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Cập nhật thất bại.", result.ToString());
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Cập nhật thành công.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, "");
            }
        }

        [HttpPost]
        [Route("detail")]
        [AllowAnonymous]
        public async Task<ResponseResult<InfoUser>> DetailInfoUserAsync(int userId)
        {
            try
            {
                var result = await _infoUserService.DetailInfoUserAsync(userId);
                return new ResponseResult<InfoUser>(RetCodeEnum.Ok, "Chi tiết người dùng.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpDelete]
        [Route("delete")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> DeleteInfoUserAsync(int userId, int userIdDelete)
        {
            try
            {
                var result = await _infoUserService.DeleteInfoUserAsync(userId, userIdDelete);
                return new ResponseResult<string>(RetCodeEnum.Ok, "Chi tiết người dùng.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("list")]
        [AllowAnonymous]
        public async Task<ResponseResult<ResponseList>> ListInfoUserAsync(int page = 1, int limit = 25)
        {
            try
            {
                var result = await _infoUserService.ListInfoUserAsync(page, limit);
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, "Danh sách người dùng.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}