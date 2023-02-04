using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoCapicityProductService
    {
        Task<bool> InsertCapacityProductAsync(InfoCapacityProduct value, int userId);
        Task<bool> UpdateCapacityProductAsync(InfoCapacityProduct value, int userId);
        Task<bool> DeleteCapacityProductAsync(int capicityProductId, int userId);
        Task<ResponseList> ListCapacityProductAsync(int page = 1, int limit = 25);
        Task<InfoCapacityProduct> DetailCapacityProductAsync(int capicityProductId);
    }
}
