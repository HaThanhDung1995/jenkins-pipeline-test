using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models;
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
    //[Authorize]
    public class DataController : BaseApiController
    {
        private readonly IInfoComentService _comentService;
        private readonly IInfoEvaluateService _evaluateService;
        private readonly IConfiguration _config;
        private readonly IInfoCartService _cartService;
        private readonly IInfoServerService _serverService;
        private readonly IInfoDistrictService _districtService;
        private readonly IInfoProvinceService _provinceService;
        private readonly IInfoOrderService _orderService;
        public DataController(IConfiguration config, IInfoComentService comentService,
            IInfoEvaluateService evaluateService, IInfoCartService cartService,
            IInfoServerService serverService, IInfoDistrictService districtService,
            IInfoProvinceService provinceService, IInfoOrderService orderService)
        {
            _config = config;
            _comentService = comentService;
            _evaluateService = evaluateService;
            _cartService = cartService;
            _serverService = serverService;
            _districtService = districtService;
            _provinceService = provinceService;
            _orderService = orderService;
        }

        //Đặt hàng
        [HttpPost]
        [Route("AddOrderAsync")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> AddOrderAsync([FromBody] InfoOrderInsertRequest value)
        {
            try
            {
                var result = await _orderService.AddOrderAsync(value);
                if (result == false)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Thêm đơn hàng không thành công.", null);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Thêm đơn hàng thành công.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        //Bình luận sản phẩm
        [HttpPost]
        [Route("UserCommentProduct")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> UserCommentProduct([FromBody] InfoCommentRequest comment)
        {
            try
            {
                var result = await _comentService.AddCommet(comment);
                if (result == false)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Bình luận không thành công.", null);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Bình luận thành công.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        //Đánh giá sao cho sản phẩm
        [HttpPost]
        [Route("UserEvaluateProduct")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> UserEvaluateProduct([FromBody] InfoEvaluateRequest evaluate)
        {
            try
            {
                var result = await _evaluateService.AddEvaluate(evaluate);
                if (result == false)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Đánh giá không thành công.", null);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Đánh giá thành công.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        //Thêm sản phẩm vào giỏ hàng
        [HttpPost]
        [Route("AddProductToCart")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> AddProductToCart([FromBody] InfoCartRequest value)
        {
            try
            {
                var result = await _cartService.AddProductToCart(value);
                if (result == false)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Thêm sản phẩm vào giỏ hàng không thành công.", null);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Thêm sản phẩm vào giỏ hàng thành công.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        //Xóa sản phẩm khỏi giỏ hàng
        [HttpDelete]
        [Route("DeleteProductToCart")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> DeleteProductToCart(int cartId)
        {
            try
            {
                var result = await _cartService.DeleteProductToCart(cartId);
                if (result == false)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Xóa sản phẩm vào giỏ hàng không thành công.", null);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Xóa sản phẩm vào giỏ hàng thành công.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        //Trừ số lượng sản phẩm trong giỏ hàng
        [HttpPost]
        [Route("ExceptProductsToCart")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> ExceptProductsToCart(int cartId)
        {
            try
            {
                var result = await _cartService.ExceptProductsToCart(cartId);
                if (result == false)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Trừ số lượng sản phẩm trong giỏ hàng không thành công.", null);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Trừ số lượng sản phẩm trong giỏ hàng thành công.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        //Cộng số lượng sản phẩm trong giỏ hàng
        [HttpPost]
        [Route("PlusProductToCart")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> PlusProductToCart(int cartId)
        {
            try
            {
                var result = await _cartService.PlusProductToCart(cartId);
                if (result == false)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Cộng số lượng sản phẩm trong giỏ hàng không thành công.", null);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Cộng số lượng sản phẩm trong giỏ hàng thành công.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        //Danh sách cart
        [HttpPost]
        [Route("ListCartByUser")]
        [AllowAnonymous]
        public async Task<ResponseResult<ResponseList>> ListCartByUser(int usreId, int page = 1, int limit = 25)
        {
            try
            {
                var result = await _cartService.ListCartByUser(usreId, page, limit);
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, "Danh sách giỏ hàng.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        //Danh sách dịch vụ mua hàng
        [HttpGet]
        [Route("ShowListSeverAsync")]
        [AllowAnonymous]
        public async Task<ResponseResult<List<InfoSever>>> ShowListSeverAsync()
        {
            try
            {
                var result = await _serverService.ShowListSeverAsync();
                return new ResponseResult<List<InfoSever>>(RetCodeEnum.Ok, "Danh sách dịch vụ mua hàng.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<InfoSever>>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        //Danh sách quận huyện tỉnh xã
        [HttpGet]
        [Route("ShowProvinceAsync")]
        [AllowAnonymous]
        public async Task<ResponseResult<List<InfoProvince>>> ShowProvinceAsync()
        {
            try
            {
                var result = await _provinceService.ShowProvinceAsync();
                return new ResponseResult<List<InfoProvince>>(RetCodeEnum.Ok, "Danh sách dịch vụ mua hàng.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<InfoProvince>>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
        [HttpGet]
        [Route("ShowDistrictByProvinceAsync")]
        [AllowAnonymous]
        public async Task<ResponseResult<List<InfoDistrict>>> ShowDistrictByProvinceAsync(int proviceId)
        {
            try
            {
                var result = await _districtService.ShowDistrictByProvinceAsync(proviceId);
                return new ResponseResult<List<InfoDistrict>>(RetCodeEnum.Ok, "Danh sách dịch vụ mua hàng.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<InfoDistrict>>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

    }
}
