using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models.Utils;
using MyPhamTrueLife.DAL.Models1;
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
    public class InfoTypeProductController : BaseApiController
    {
        private readonly IInfoTypeProductService _infoTypeProductService;
        public InfoTypeProductController(IInfoTypeProductService infoTypeProductService)
        {
            _infoTypeProductService = infoTypeProductService;
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Post([FromBody] InfoTypeProduct value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoTypeProductService.InsertTypeProductAsync(value, userId);
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
        public async Task<ResponseResult<string>> Update([FromBody] InfoTypeProduct value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoTypeProductService.UpdateTypeProductAsync(value, userId);
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
        public async Task<ResponseResult<string>> Delete(int typeProductId, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoTypeProductService.DeleteTypeProductAsync(typeProductId, userId);
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
                var result = await _infoTypeProductService.ListTypeProductAsync(page, limit);
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
        public async Task<ResponseResult<InfoTypeProduct>> Detail(int typeProductId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoTypeProductService.DetailTypeProductAsync(typeProductId);
                return new ResponseResult<InfoTypeProduct>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InfoTypeProduct>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
