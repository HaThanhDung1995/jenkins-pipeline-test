using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using MyPhamTrueLife.DAL.Models1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoNatureService
    {
        Task<bool> InsertNatureAsync(InfoNature value, int userId);
        Task<bool> UpdateNatureAsync(InfoNature value, int userId);
        Task<bool> DeleteNatureAsync(int natureId, int userId);
        Task<ResponseList> ListNatureAsync(int page = 1, int limit = 25);
        Task<InfoNature> DetailNatureAsync(int natureId);
    }
}
