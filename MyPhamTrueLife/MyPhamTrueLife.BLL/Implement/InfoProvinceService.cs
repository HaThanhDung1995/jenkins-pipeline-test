using Microsoft.EntityFrameworkCore;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Implement
{
    public class InfoProvinceService : IInfoProvinceService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoProvinceService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<InfoProvince>> ShowProvinceAsync()
        {
            var pro = await _unitOfWork.Repository<InfoProvince>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            return pro;
        }
    }
}
