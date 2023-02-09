using MyPhamTrueLife.DAL.Models.Utils;
using MyPhamTrueLife.DAL.Models1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IImportSellService
    {
        Task<bool> CreateImportSellServiceAsync(InfoImportSell value, int staffId);
        Task<bool> DeleteImportSellServiceAsync(int id, int staffId);
        Task<ResponseList> ListImportSellServiceAsync(int page = 1, int limit = 25);
    }
}
