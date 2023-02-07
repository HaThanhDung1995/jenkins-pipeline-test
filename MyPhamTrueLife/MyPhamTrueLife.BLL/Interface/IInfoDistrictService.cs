using MyPhamTrueLife.DAL.Models1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoDistrictService
    {
        Task<List<InfoDistrict>> ShowDistrictByProvinceAsync(int provinceId);
    }
}
