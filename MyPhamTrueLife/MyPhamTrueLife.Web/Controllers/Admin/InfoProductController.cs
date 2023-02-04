using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyPhamTrueLife.BLL.Implement;
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
    public class InfoProductController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IInfoProductService _infoProductService;
        private readonly IConfiguration _config;
        public InfoProductController(IConfiguration config, IProductService productService, IInfoProductService infoProductService)
        {
            _config = config;
            _productService = productService;
            _infoProductService = infoProductService;
        }

        //Danh sách tất cả sản phẩm
        [Route("list")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<ResponseList>> ShowListProductFilter(ProductFilterRequest value, int page = 1, int limit = 25)
        {
            try
            {
                var result = await _productService.ShowListProductFilter(value, page, limit);
                if (result == null)
                {
                    return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, "Danh sách tất cả sản phẩm và filter.", null);
                }
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        [Route("detail")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<ProductDetail>> ProductDetailAsync(int id, int? nature, int? capacity)
        {
            try
            {
                var result = await _productService.ProductDetailAsync(id, nature, capacity);
                if (result == null)
                {
                    return new ResponseResult<ProductDetail>(RetCodeEnum.ApiError, "Chi tiết sản phẩm.", null);
                }
                return new ResponseResult<ProductDetail>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ProductDetail>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [Route("insert")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> InsertProductAsync(InfoProductInsertLogin value, int userId)
        {
            try
            {
                var result = await _infoProductService.InsertInfoProductAsync(value, userId);
                if (result == null)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Thêm sản phẩm thất bại", null);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Thêm sản phẩm thành công", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
