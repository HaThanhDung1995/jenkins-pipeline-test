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
    public class InfoSupplierController : BaseApiController
    {
        private readonly IInfoSupplierService _infoSupplierService;
        public InfoSupplierController(IInfoSupplierService infoSupplierService)
        {
            //_infoStaff = infoStaff;
            _infoSupplierService = infoSupplierService;
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Post([FromBody] InfoSupplier value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoSupplierService.InsertSupplierAsync(value, userId);
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
        public async Task<ResponseResult<string>> Update([FromBody] InfoSupplier value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoSupplierService.UpdateSupplierAsync(value, userId);
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
        public async Task<ResponseResult<string>> Delete(int supplierId, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoSupplierService.DeleteSupplierAsync(supplierId, userId);
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
                var result = await _infoSupplierService.ListSupplierAsync(page, limit);
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
        public async Task<ResponseResult<InfoSupplier>> Detail(int supplierId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoSupplierService.DetailSupplierAsync(supplierId);
                return new ResponseResult<InfoSupplier>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InfoSupplier>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
