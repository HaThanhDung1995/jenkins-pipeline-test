using MyPhamTrueLife.DAL.Models.Utils;
using MyPhamTrueLife.DAL.Models1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoTypeProductService
    {
        Task<bool> InsertTypeProductAsync(InfoTypeProduct value, int userId);
        Task<bool> UpdateTypeProductAsync(InfoTypeProduct value, int userId);
        Task<bool> DeleteTypeProductAsync(int typeProductId, int userId);
        Task<InfoTypeProduct> DetailTypeProductAsync(int typeProductId);
        Task<ResponseList> ListTypeProductAsync(int page = 1, int limit = 25);
    }
}
