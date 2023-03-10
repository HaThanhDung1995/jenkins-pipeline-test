using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IUserAdminService
    {
        Task<bool> InsertStaff(InfoStaff value);
        Task<bool> UpdateStaff(InfoStaff value);
        Task<bool> DeleteStaff(int userId);
        Task<InfoStaff> DetailStaff(int userId);
        Task<ResponseList> ListStaff(int page = 1, int limit = 25);
        Task<LoginResponse> LoginAsync(Login value);
        //Phần điểm danh
        Task<bool> TaoLichLamViec(int userId, int? month, int? year);
        Task<ResponseList> LayLichLamViecChoCaHai(int page = 1, int limit = 25);
        Task<ResponseList> XemChiTietLichLamViecChoCaHai(int userId, int? calendarId, int page = 1, int limit = 25);
        Task<List<LichTaoViecChoAdmin>> LayLichLamDeDangKy(int calendarId);
        Task<int> DangKyLichLamViecCuaNhanVien(InfoDetailCalendar value, int staffId);
        Task<List<LichLamViecCuaNhanVien>> LayLichLamCuaNhanVien(int staffId, int? month, int? year);
        Task<int> DiemDanhVaKetCaCuaNhanVien(int? staffId, int? detailcalendarId, bool? IsAtendan);
    }
}
