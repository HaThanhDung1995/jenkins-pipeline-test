using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPhamTrueLife.BLL.Interface;
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
    public class InfoOrderController : BaseApiController
    {
        private readonly IInfoOrderService _infoOrderService;
        public InfoOrderController(IInfoOrderService infoOrderService)
        {
            _infoOrderService = infoOrderService;
        }

        [HttpPost]
        [Route("get-list")]
        [AllowAnonymous]
        public async Task<ResponseResult<ResponseList>> list(int page = 1, int limit = 25)
        {
            try
            {
                var result = await _infoOrderService.GetListOrderAdminAsync(page, limit);
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, "Danh sách đơn hàng.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [Route("cap-nhat-trang-thai-duyet")]
        [HttpPut]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> UpdateTrangThaiDuyet([FromBody] InfoOrderUpdateStatus value)
        {
            try
            {
                value.Status = "DADUYET";
                var result = await _infoOrderService.UpdateOrderAsync(value);
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [Route("cap-nhat-trang-thai-da-thanh-toan")]
        [HttpPut]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> UpdateTrangThaiDaThanhToan([FromBody] InfoOrderUpdateStatus value)
        {
            try
            {
                var result = await _infoOrderService.UpdateOrderAsync(value);
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
