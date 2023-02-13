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
                value.IsPay = null;
                var result = await _infoOrderService.UpdateOrderAsync(value);
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Cập nhật trạng thái thất bại", result.ToString());
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [Route("cap-nhat-trang-thai-don-hang")]
        [HttpPut]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> UpdateTrangThaiDonHang([FromBody] InfoOrderUpdateStatus value)
        {
            try
            {
                value.IsPay = null;
                var result = await _infoOrderService.UpdateOrderAsync(value);
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Cập nhật trạng thái thất bại", result.ToString());
                }
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
                value.Status = string.Empty;
                var result = await _infoOrderService.UpdateOrderAsync(value);
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Cập nhật trạng thái thất bại", result.ToString());
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [Route("xem-chi-tiet-don-hang")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<XemChiTietDonHangRes>> Detail(int orderId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoOrderService.XemChiTietDonHang(orderId);
                return new ResponseResult<XemChiTietDonHangRes>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<XemChiTietDonHangRes>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
