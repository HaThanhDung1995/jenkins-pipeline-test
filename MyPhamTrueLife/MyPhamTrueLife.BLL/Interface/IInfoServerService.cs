using MyPhamTrueLife.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoServerService
    {
        Task<List<InfoSever>> ShowListSeverAsync();
    }
}
