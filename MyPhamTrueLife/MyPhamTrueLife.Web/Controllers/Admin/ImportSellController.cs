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
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Thêm nhập hàng không thành công", result.ToString());
                }
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
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Xóa nhập hàng không thành công", result.ToString());
                }
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
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Thêm chi tiết nhập hàng không thành công", result.ToString());
                }
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
        public async Task<ResponseResult<ResponseList>> Post2(int importSellId, int page = 1, int limit = 25)
        {
            try
            {
                var result = await _importSellService.DetailImportSellAsync(importSellId, page, limit);
                return new ResponseResult<ResponseList>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<ResponseList>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [Route("cap-nhat-trang-thai-nhap-hang")]
        [HttpPut]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> UpdateStatusImport(int importSellId, int staffId, string Status)
        {
            try
            {
                var result = await _importSellService.CapNhatTrangThaiDonNhapHang(importSellId, staffId, Status);
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Cập nhật trạng thái nhập hàng thất bại", result.ToString());
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [Route("cap-nhat-trang-thai-thanh-toan")]
        [HttpPut]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> UpdateStatusPayImport(int importSellId, int staffId)
        {
            try
            {
                var result = await _importSellService.CapNhatTrangThaiThanhToanDonNhapHang(importSellId, staffId);
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Cập nhật nhập hàng trạng thái thanh toán không thành công", result.ToString());
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [Route("cap-nhat-nhap-hang-vao-kho")]
        [HttpPut]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> UpdateTockImport(int importSellId, int staffId)
        {
            try
            {
                var result = await _importSellService.CapNhatDonNhapHangVaoKho(importSellId, staffId);
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Cập nhập hàng vào kho không thành công", result.ToString());
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [Route("xoa-chi-tiet-nhap-hang")]
        [HttpDelete]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> DeletehapHang(int importSellId, int productId, int? capacityId, int staffId)
        {
            try
            {
                var result = await _importSellService.XoaChiTietNhapHang(importSellId, productId, capacityId, staffId);
                if (result != true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Xóa chi tiết nhập hàng không thành công", result.ToString());
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, RetCodeEnum.Ok.ToString(), result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }
    }
}
