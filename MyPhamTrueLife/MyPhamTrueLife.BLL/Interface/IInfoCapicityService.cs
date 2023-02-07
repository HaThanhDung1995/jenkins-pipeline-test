using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoCapicityService
    {
        Task<bool> InsertCapacityAsync(InfoCapacity value, int userId);
        Task<bool> UpdateCapacityAsync(InfoCapacity value, int userId);
        Task<bool> DeleteCapacityAsync(int capicityId, int userId);
        Task<ResponseList> ListCapacityAsync(int page = 1, int limit = 25);
        Task<InfoCapacity> DetailCapacityAsync(int capicityId);
    }
}
