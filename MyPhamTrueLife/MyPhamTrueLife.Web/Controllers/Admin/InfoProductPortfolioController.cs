using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class InfoProductPortfolioController : BaseApiController
    {
        private readonly IInfoProductPortfolioService _infoProductPortfolioService;
        public InfoProductPortfolioController(IInfoProductPortfolioService infoProductPortfolioService)
        {
            _infoProductPortfolioService = infoProductPortfolioService;
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Post([FromBody] InfoProductPortfolio value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoProductPortfolioService.InsertProductPortfolioAsync(value, userId);
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
        public async Task<ResponseResult<string>> Update([FromBody] InfoProductPortfolio value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoProductPortfolioService.UpdateProductPortfolioAsync(value, userId);
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
        public async Task<ResponseResult<string>> Delete(int productPortfolioId, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoProductPortfolioService.DeleteProductPortfolioAsync(productPortfolioId, userId);
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
                var result = await _infoProductPortfolioService.ListProductPortfolioAsync(page, limit);
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
        public async Task<ResponseResult<InfoProductPortfolio>> Detail(int productPortfolioId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _infoProductPortfolioService.DetailProductPortfolioAsync(productPortfolioId);
                return new ResponseResult<InfoProductPortfolio>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InfoProductPortfolio>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
