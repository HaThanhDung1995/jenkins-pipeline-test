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
    public class InfoPromotionController : BaseApiController
    {
        //private readonly IInfoTypeStaffService _infoTypeStaff;
        private readonly IInfoPromotionService _infoPromotionService;
        public InfoPromotionController(IInfoPromotionService infoPromotionService)
        {
            //_infoTypeStaff = infoTypeStaff;
            _infoPromotionService = infoPromotionService;
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Post([FromBody] InfoPromotion value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoPromotionService.InsertInfoPromotionAsync(value, userId);
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
        public async Task<ResponseResult<string>> Update([FromBody] InfoPromotion value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoPromotionService.UpdateInfoPromotionAsync(value, userId);
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
                var result = await _infoPromotionService.DeleteInfoPromotionAsync(typeStaffId, userId);
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
                var result = await _infoPromotionService.ListInfoPromotionAsync(page, limit);
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
        public async Task<ResponseResult<InfoPromotion>> Detail(int typeStaffId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoPromotionService.DetailInfoPromotionAsync(typeStaffId);
                return new ResponseResult<InfoPromotion>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InfoPromotion>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
