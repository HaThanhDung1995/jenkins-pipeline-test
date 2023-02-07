using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoStaffService
    {
        Task<bool> InsertStaffAsync(InfoStaff value, int userId);
        Task<bool> UpdateStaffAsync(InfoStaff value, int userId);
        Task<bool> DeleteStaffAsync(int staffId, int userId);
        Task<ResponseList> ListStaffAsync(int page = 1, int limit = 25);
        Task<InfoStaffReq> DetailStaffAsync(int staffId);
    }
}
