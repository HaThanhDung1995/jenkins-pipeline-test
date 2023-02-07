using Microsoft.EntityFrameworkCore;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Implement
{
    public class InfoDistrictService : IInfoDistrictService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoDistrictService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<InfoDistrict>> ShowDistrictByProvinceAsync(int provinceId)
        {
            var dis = await _unitOfWork.Repository<InfoDistrict>().Where(x => x.DeleteFlag != true && x.ProvinceId.Equals(provinceId)).AsNoTracking().ToListAsync();
            return dis;
        }
    }
}
