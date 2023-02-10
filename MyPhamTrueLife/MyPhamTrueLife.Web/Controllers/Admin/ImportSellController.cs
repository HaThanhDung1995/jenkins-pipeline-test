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
    public class ImportSellController : BaseApiController
    {
        private readonly IImportSellService _importSellService;
        public ImportSellController(IImportSellService importSellService)
        {
            _importSellService = importSellService;
        }

        [Route("them-nhap-hang")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Post([FromBody] InfoImportSell value, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _importSellService.CreateImportSellServiceAsync(value, userId);
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [Route("xoa-nhap-hang")]
        [HttpDelete]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Delete(int importId, int userId)
        {
            try
            {
                //int currentUserId = GetCurrentUserId();
                var result = await _importSellService.DeleteImportSellServiceAsync(importId, userId);
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("lay-danh-sach-nhap-hang")]
        [AllowAnonymous]
        public async Task<ResponseResult<ResponseList>> list(int page = 1, int limit = 25)
        {
            try
            {
                var result = await _importSellService.ListImportSellServiceAsync(page, limit);
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, "Danh sách nhập hàng", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [Route("them-danh-sach-chi-tiet-nhap-hang")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Post1([FromBody] List<InfoDetailImportSell> value, int userId, int importSellId)
        {
            try
            {
                var result = await _importSellService.ThemDanhSachChiTietNhapHang(value, userId, importSellId);
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }


        [Route("xem-chit-tiet-don-nhap-hang")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> Post2(int importSellId, int page = 1, int limit = 25)
        {
            try
            {
                var result = await _importSellService.DetailImportSellAsync(importSellId, page, limit);
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
