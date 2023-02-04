using MyPhamTrueLife.BLL.Implement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoProductService
    {
        Task<bool> InsertInfoProductAsync(InfoProductInsertLogin value, int userId);
    }
}
