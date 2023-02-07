using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoUserService
    {
        Task<bool> InsertInfoUserAssync(InfoUserReq value, int userId);
        Task<bool> UpdateInfoUserAsync(InfoUser value);
        Task<bool> DeleteInfoUserAsync(int userId, int userIDelete);
        Task<InfoUser> DetailInfoUserAsync(int userId);
        Task<ResponseList> ListInfoUserAsync(int page = 1, int limit = 25);
    }
}
