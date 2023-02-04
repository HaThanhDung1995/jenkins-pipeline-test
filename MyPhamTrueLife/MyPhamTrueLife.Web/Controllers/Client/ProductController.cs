using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models.Utils;
using MyPhamTrueLife.Web.Base;
using MyPhamTrueLife.Web.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPhamTrueLife.Web.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IConfiguration _config;
        public ProductController(IConfiguration config, IProductService productService)
        {
            _config = config;
            _productService = productService;
        }
        //Danh sách danh mục sản phẩm
        [Route("ListProductPortfolioUserAsync")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ResponseResult<List<ListProductPortfolioUser>>> ListProductPortfolioUserAsync()
        {
            try
            {
                var result = await _productService.ListProductPortfolioUserAsync();
                if (result == null)
                {
                    return new ResponseResult<List<ListProductPortfolioUser>>(RetCodeEnum.ApiError, "Danh mục loại sản phẩm.", null);
                }
                return new ResponseResult<List<ListProductPortfolioUser>>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<ListProductPortfolioUser>>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        //6 sản phẩm bán chạy nhất
        [Route("TopSixSellingProducts")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ResponseResult<ResponseList>> TopSixSellingProducts()
        {
            try
            {
                var result = await _productService.TopSixSellingProducts();
                if (result == null)
                {
                    return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, "Sản phẩm bán chạy.", null);
                }
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        //6 sản phẩm mới nhất
        [Route("TopSixNewProducts")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ResponseResult<ResponseList>> TopSixNewProducts()
        {
            try
            {
                var result = await _productService.TopSixNewProducts();
                if (result == null)
                {
                    return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, "Sản phẩm mới.", null);
                }
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        //6 sản phẩm đang được khuyến mãi
        [Route("TopSixProductPromotion")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ResponseResult<ResponseList>> TopSixProductPromotion()
        {
            try
            {
                var result = await _productService.TopSixProductPromotion();
                if (result == null)
                {
                    return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, "Sản phẩm khuyến mãi.", null);
                }
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        //Danh sách tất cả sản phẩm
        [Route("ShowListProductFilter")]
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
        [Route("ProductDetailAsync")]
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
    }
}
