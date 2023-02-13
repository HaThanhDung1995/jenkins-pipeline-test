using MyPhamTrueLife.DAL.Models.Utils;
using MyPhamTrueLife.DAL.Models1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IImportSellService
    {
        Task<bool> CreateImportSellServiceAsync(InfoImportSell value, int staffId);
        Task<bool> DeleteImportSellServiceAsync(int id, int staffId);
        Task<ResponseList> ListImportSellServiceAsync(int page = 1, int limit = 25);
        Task<ResponseList> DetailImportSellAsync(int importSellId, int page = 1, int limit = 25);

        //Thêm danh sách chi tiết sản phẩm
        Task<bool> ThemDanhSachChiTietNhapHang(List<InfoDetailImportSell> value, int staffId, int importSellId);
        Task<bool> XoaChiTietNhapHang(int importSellId, int productId, int? capacityId, int staffId);
        //Cập nhật đơn hàng
        Task<bool> CapNhatTrangThaiDonNhapHang(int importSellId, int staffId, string Status);
        Task<bool> CapNhatTrangThaiThanhToanDonNhapHang(int importSellId, int staffId);
        Task<bool> CapNhatDonNhapHangVaoKho(int importSellId, int staffId);

    }
}
