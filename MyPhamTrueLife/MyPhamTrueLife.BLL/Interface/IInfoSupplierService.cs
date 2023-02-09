using MyPhamTrueLife.DAL.Models.Utils;
using MyPhamTrueLife.DAL.Models1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoSupplierService
    {
        Task<bool> InsertSupplierAsync(InfoSupplier value, int userId);
        Task<bool> UpdateSupplierAsync(InfoSupplier value, int userId);
        Task<bool> DeleteSupplierAsync(int supplierId, int userId);
        Task<ResponseList> ListSupplierAsync(int page = 1, int limit = 25);
        Task<InfoSupplier> DetailSupplierAsync(int supplierId);
    }
}
