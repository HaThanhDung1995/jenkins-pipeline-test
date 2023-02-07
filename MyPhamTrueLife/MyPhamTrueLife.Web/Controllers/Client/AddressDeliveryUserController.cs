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
using System.Threading.Tasks;

namespace MyPhamTrueLife.Web.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressDeliveryUserController : BaseApiController
    {
        private readonly IInfoAddressDeliveryUserService _addressDeliveryUserService;
        private readonly IConfiguration _config;
        public AddressDeliveryUserController(IConfiguration config, IInfoAddressDeliveryUserService addressDeliveryUserService)
        {
            _config = config;
            _addressDeliveryUserService = addressDeliveryUserService;
        }

        [HttpPost]
        [Route("CreateInfoAddressDeliveryUser")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> CreateInfoAddressDeliveryUser([FromBody] InfoAddressDeliveryUser value)
        {
            try
            {
                var result = await _addressDeliveryUserService.CreateInfoAddressDeliveryUserAsync(value);
                if (result == false)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Thêm không thành công.", null);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Thêm thành công.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPut]
        [Route("UpdateInfoAddressDeliveryUser")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> UpdateInfoAddressDeliveryUser([FromBody] InfoAddressDeliveryUser value)
        {
            try
            {
                var result = await _addressDeliveryUserService.UpdateInfoAddressDeliveryUserAsync(value);
                if (result == false)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Chỉnh sửa không thành công.", null);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Chỉnh sửa thành công.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpDelete]
        [Route("DeleteInfoAddressDeliveryUser")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> DeleteInfoAddressDeliveryUser(int id)
        {
            try
            {
                var result = await _addressDeliveryUserService.DeleteInfoAddressDeliveryUserAsync(id);
                if (result == false)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Xóa không thành công.", null);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Xóa thành công.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("ListInfoAddressDeliveryUser")]
        [AllowAnonymous]
        public async Task<ResponseResult<List<InfoAddressDeliveryUser>>> ListInfoAddressDeliveryUser(int id)
        {
            try
            {
                var result = await _addressDeliveryUserService.ListInfoAddressDeliveryUserAsync(id);
                return new ResponseResult<List<InfoAddressDeliveryUser>>(RetCodeEnum.Ok, "Danh sách địa chỉ giao hàng.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<InfoAddressDeliveryUser>>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
