using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoTypeNatureService
    {
        Task<bool> InsertTypeNatureAsync(InfoTypeNature value, int userId);
        Task<bool> UpdateTypeNatureAsync(InfoTypeNature value, int userId);
        Task<bool> DeleteTypeNatureAsync(int typeNatureId, int userId);
        Task<ResponseList> ListTypeNatureAsync(int page = 1, int limit = 25);
        Task<InfoTypeNature> DetailTypeNatureAsync(int typeNatureId);
    }
}
