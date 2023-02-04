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
    }
}
