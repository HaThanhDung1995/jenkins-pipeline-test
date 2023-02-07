using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoPositionStaffService
    {
        Task<bool> InsertPositionStaffAsync(InfoPositionStaff value, int userId);
        Task<bool> UpdatePositionStaffAsync(InfoPositionStaff value, int userId);
        Task<bool> DeletePositionStaffAsync(int natureId, int userId);
        Task<ResponseList> ListPositionStaffAsync(int page = 1, int limit = 25);
        Task<InfoPositionStaff> DetailPositionStaffAsync(int natureId);
    }
}
