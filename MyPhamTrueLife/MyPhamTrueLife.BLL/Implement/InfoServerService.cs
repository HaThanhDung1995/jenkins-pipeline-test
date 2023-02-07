using Microsoft.EntityFrameworkCore;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Implement
{
    public class InfoServerService : IInfoServerService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoServerService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<InfoSever>> ShowListSeverAsync()
        {
            var server = await _unitOfWork.Repository<InfoSever>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            return server;
        }
    }
}
