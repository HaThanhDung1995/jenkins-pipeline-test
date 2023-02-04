using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoTypeStaffService
    {
        Task<bool> InsertTypeStaffAsync(InfoTypeStaff value, int userId);
        Task<bool> UpdateTypeStaffAsync(InfoTypeStaff value, int userId);
        Task<bool> DeleteTypeStaffAsync(int typeStaffId, int userId);
        Task<ResponseList> ListTypeStaffAsync(int page = 1, int limit = 25);
        Task<InfoTypeStaff> DetailTypeStaffAsync(int typeStaffId);
    }
}
