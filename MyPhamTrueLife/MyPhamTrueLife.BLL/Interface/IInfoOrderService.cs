using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoOrderService
    {
        Task<bool> AddOrderAsync(InfoOrderInsertRequest value);
        //getlisst admin
        Task<ResponseList> GetListOrderAdminAsync(int page = 1, int limit = 25);
        Task<bool> UpdateOrderAsync(InfoOrderUpdateStatus value);
    }
}
