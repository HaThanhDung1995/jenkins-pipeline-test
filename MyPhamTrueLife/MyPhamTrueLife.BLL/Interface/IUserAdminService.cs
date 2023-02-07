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
        Task<bool> TaoLichLamViec(int userId);
    }
}
