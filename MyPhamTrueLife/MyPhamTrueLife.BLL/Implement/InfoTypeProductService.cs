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
    public class InfoTypeProductService : IInfoTypeProductService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoTypeProductService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> InsertTypeProductAsync(InfoTypeProduct value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            value.CreateAt = DateTime.Now;
            value.CreateUser = userId;
            await _unitOfWork.Repository<InfoTypeProduct>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> UpdateTypeProductAsync(InfoTypeProduct value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            var typePruct = await _unitOfWork.Repository<InfoTypeProduct>().Where(x => x.DeleteFlag != true && x.TypeProductId.Equals(value.TypeProductId)).AsNoTracking().FirstOrDefaultAsync();
            if (typePruct == null)
            {
                return false;
            }
            typePruct.TypePeoductName = value.TypePeoductName;
            typePruct.DeleteFlag = false;
            typePruct.UpdateAt = DateTime.Now;
            typePruct.UpdateUser = userId;
            _unitOfWork.Repository<InfoTypeProduct>().UpdateRange(typePruct);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> DeleteTypeProductAsync(int typeProductId, int userId)
        {
            if (typeProductId <= 0 || userId <= 0)
            {
                return false;
            }
            var typeProduct = await _unitOfWork.Repository<InfoTypeProduct>().Where(x => x.DeleteFlag != true && x.TypeProductId.Equals(typeProductId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeProduct == null)
            {
                return false;
            }
            typeProduct.DeleteFlag = true;
            typeProduct.UpdateAt = DateTime.Now;
            typeProduct.UpdateUser = userId;
            _unitOfWork.Repository<InfoTypeProduct>().UpdateRange(typeProduct);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<InfoTypeProduct> DetailTypeProductAsync(int typeProductId)
        {
            if (typeProductId <= 0)
            {
                return null;
            }
            var info = await _unitOfWork.Repository<InfoTypeProduct>().Where(x => x.DeleteFlag != true && x.TypeProductId.Equals(typeProductId)).AsNoTracking().FirstOrDefaultAsync();
            return info;
        }
        public async Task<ResponseList> ListTypeProductAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listTypeProdcut = await _unitOfWork.Repository<InfoTypeProduct>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listTypeProdcut.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listTypeProdcut = listTypeProdcut.Skip(start).Take(limit).ToList();
            listData.ListData = listTypeProdcut;
            return listData;
        }
    }
}
