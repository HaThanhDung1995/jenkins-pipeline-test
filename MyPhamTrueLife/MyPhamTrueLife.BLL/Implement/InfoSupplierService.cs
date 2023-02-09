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
    public class InfoSupplierService: IInfoSupplierService
    {
        private readonly dbDevNewContext _unitOfWork;
        public InfoSupplierService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> InsertSupplierAsync(InfoSupplier value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            value.CreateAt = DateTime.Now;
            value.CreateUser = userId;
            await _unitOfWork.Repository<InfoSupplier>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> UpdateSupplierAsync(InfoSupplier value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            var supplier = await _unitOfWork.Repository<InfoSupplier>().Where(x => x.DeleteFlag != true && x.SupplierId.Equals(value.SupplierId)).AsNoTracking().FirstOrDefaultAsync();
            if (supplier == null)
            {
                return false;
            }
            supplier.SupplierName = value.SupplierName;
            supplier.Adrress = value.Adrress;
            supplier.ProvinceId = value.ProvinceId;
            supplier.DistrictId = value.DistrictId;
            supplier.Email = value.Email;
            supplier.Phone = value.Phone;
            supplier.TypeProductId = value.TypeProductId;
            supplier.DeleteFlag = false;
            supplier.UpdateAt = DateTime.Now;
            supplier.UpdateUser = userId;
            _unitOfWork.Repository<InfoSupplier>().UpdateRange(supplier);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> DeleteSupplierAsync(int supplierId, int userId)
        {
            if (supplierId <= 0 || userId <= 0)
            {
                return false;
            }
            var supplier = await _unitOfWork.Repository<InfoSupplier>().Where(x => x.DeleteFlag != true && x.SupplierId.Equals(supplierId)).AsNoTracking().FirstOrDefaultAsync();
            if (supplier == null)
            {
                return false;
            }
            supplier.DeleteFlag = true;
            supplier.UpdateAt = DateTime.Now;
            supplier.UpdateUser = userId;
            _unitOfWork.Repository<InfoSupplier>().UpdateRange(supplier);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<ResponseList> ListSupplierAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listSupplier = await _unitOfWork.Repository<InfoSupplier>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listSupplier.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listSupplier = listSupplier.Skip(start).Take(limit).ToList();
            listData.ListData = listSupplier;
            return listData;
        }
        public async Task<InfoSupplier> DetailSupplierAsync(int supplierId)
        {
            if (supplierId <= 0)
            {
                return null;
            }
            var info = await _unitOfWork.Repository<InfoSupplier>().Where(x => x.DeleteFlag != true && x.SupplierId.Equals(supplierId)).AsNoTracking().FirstOrDefaultAsync();
            return info;
        }
    }
}
