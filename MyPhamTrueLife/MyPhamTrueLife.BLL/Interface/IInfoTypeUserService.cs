using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoTypeUserService
    {
        Task<bool> InsertTypeUserAsync(InfoTypeUserReq value, int userId);
        Task<bool> UpdateTypeUserAsync(InfoTypeUserReq value, int userId);
        Task<bool> DeleteTypeUserAsync(int typeUserId, int userId);
        Task<InfoTypeUser> DetailTypeUserAsync(int typeUserId);
        Task<ResponseList> ListTypeUserAsync(int page = 1, int limit = 25);
    }
}
