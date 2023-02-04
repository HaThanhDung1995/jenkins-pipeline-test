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
    public class InfoPromotionService : IInfoPromotionService
    {
        private readonly dbDevNewContext _unitOfWork;
        public InfoPromotionService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteInfoPromotionAsync(int natureId, int userId)
        {
            if (natureId <= 0 || userId <= 0)
            {
                return false;
            }
            var typeNature = await _unitOfWork.Repository<InfoPromotion>().Where(x => x.DeleteFlag != true && x.PromotionId.Equals(natureId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeNature == null)
            {
                return false;
            }
            typeNature.DeleteFlag = true;
            typeNature.UpdateAt = DateTime.Now;
            typeNature.UpdateUser = userId;
            _unitOfWork.Repository<InfoPromotion>().UpdateRange(typeNature);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<InfoPromotion> DetailInfoPromotionAsync(int natureId)
        {
            if (natureId <= 0)
            {
                return null;
            }
            var info = await _unitOfWork.Repository<InfoPromotion>().Where(x => x.DeleteFlag != true && x.PromotionId.Equals(natureId)).AsNoTracking().FirstOrDefaultAsync();
            return info;
        }

        public async Task<bool> InsertInfoPromotionAsync(InfoPromotion value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            value.CreateAt = DateTime.Now;
            value.CreateUser = userId;
            await _unitOfWork.Repository<InfoPromotion>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<ResponseList> ListInfoPromotionAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listTypeNature = await _unitOfWork.Repository<InfoPromotion>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listTypeNature.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listTypeNature = listTypeNature.Skip(start).Take(limit).ToList();
            listData.ListData = listTypeNature;
            return listData;
        }

        public async Task<bool> UpdateInfoPromotionAsync(InfoPromotion value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            var typeNature = await _unitOfWork.Repository<InfoPromotion>().Where(x => x.DeleteFlag != true && x.PromotionId.Equals(value.PromotionId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeNature == null)
            {
                return false;
            }
            typeNature.StartAt = value.StartAt;
            typeNature.EndAt = value.EndAt;
            typeNature.Describe = value.Describe;
            typeNature.Name = value.Name;
            typeNature.PromotionType = value.PromotionType; 
            typeNature.DeleteFlag = false;
            typeNature.UpdateAt = DateTime.Now;
            typeNature.UpdateUser = userId;
            _unitOfWork.Repository<InfoPromotion>().UpdateRange(typeNature);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
