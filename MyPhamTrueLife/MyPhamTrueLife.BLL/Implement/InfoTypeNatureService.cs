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
    public class InfoTypeNatureService : IInfoTypeNatureService
    {
        private readonly dbDevNewContext _unitOfWork;
        public InfoTypeNatureService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteTypeNatureAsync(int typeNatureId, int userId)
        {
            if (typeNatureId <= 0 || userId <= 0)
            {
                return false;
            }
            var typeNature = await _unitOfWork.Repository<InfoTypeNature>().Where(x => x.DeleteFlag != true && x.TypeNatureId.Equals(typeNatureId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeNature == null)
            {
                return false;
            }
            typeNature.DeleteFlag = true;
            typeNature.UpdateAt = DateTime.Now;
            typeNature.UpdateUser = userId;
            _unitOfWork.Repository<InfoTypeNature>().UpdateRange(typeNature);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<InfoTypeNature> DetailTypeNatureAsync(int typeNatureId)
        {
            if (typeNatureId <= 0)
            {
                return null;
            }
            var info = await _unitOfWork.Repository<InfoTypeNature>().Where(x => x.DeleteFlag != true && x.TypeNatureId.Equals(typeNatureId)).AsNoTracking().FirstOrDefaultAsync();
            return info;
        }

        public async Task<bool> InsertTypeNatureAsync(InfoTypeNature value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            value.CreateAt = DateTime.Now;
            value.CreateUser = userId;
            await _unitOfWork.Repository<InfoTypeNature>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<ResponseList> ListTypeNatureAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listTypeNature = await _unitOfWork.Repository<InfoTypeNature>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listTypeNature.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listTypeNature = listTypeNature.Skip(start).Take(limit).ToList();
            listData.ListData = listTypeNature;
            return listData;
        }

        public async Task<bool> UpdateTypeNatureAsync(InfoTypeNature value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            var typeNature = await _unitOfWork.Repository<InfoTypeNature>().Where(x => x.DeleteFlag != true && x.TypeNatureId.Equals(value.TypeNatureId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeNature == null)
            {
                return false;
            }
            typeNature.TypeNatureName = value.TypeNatureName;
            typeNature.DeleteFlag = false;
            typeNature.UpdateAt = DateTime.Now;
            typeNature.UpdateUser = userId;
            _unitOfWork.Repository<InfoTypeNature>().UpdateRange(typeNature);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
