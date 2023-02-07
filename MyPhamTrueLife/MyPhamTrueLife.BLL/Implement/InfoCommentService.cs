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
    public class InfoCommentService : IInfoCommentService
    {
        private readonly dbDevNewContext _unitOfWork;
        public InfoCommentService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteCommentAsync(int productId, int userId)
        {
            if (productId <= 0 || userId <= 0)
            {
                return false;
            }
            var typeNature = await _unitOfWork.Repository<InfoComent>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(productId) && x.UserId.Equals(userId)).AsNoTracking().FirstOrDefaultAsync();
            if (typeNature == null)
            {
                return false;
            }
            typeNature.DeleteFlag = true;
            typeNature.UpdateAt = DateTime.Now;
            typeNature.UpdateUser = userId;

            _unitOfWork.Repository<InfoComent>().UpdateRange(typeNature);
            await _unitOfWork.SaveChangeAsync();
            return true;

        }

        public async Task<InfoComent> DetailCommentAsync(int productId, int userId)
        {
            if (productId <= 0 || userId <= 0)
            {
                return null;
            }
            var info = await _unitOfWork.Repository<InfoComent>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(productId) && x.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
            return info;
        }

        public async Task<bool> InsertCommentAsync(InfoComent value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            value.CreateAt = DateTime.Now;
            value.CreateUser = userId;
            await _unitOfWork.Repository<InfoComent>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<ResponseList> ListCommentAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listTypeNature = await _unitOfWork.Repository<InfoComent>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listTypeNature.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listTypeNature = listTypeNature.Skip(start).Take(limit).ToList();
            listData.ListData = listTypeNature;
            return listData;
        }

        public async Task<bool> UpdateCommentAsync(InfoComent value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            var typeNature = await _unitOfWork.Repository<InfoComent>().Where(x => x.DeleteFlag != true && x.ProductId.Equals(value.ProductId) && x.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
            if (typeNature == null)
            {
                return false;
            }
            typeNature.Content = value.Content;
            typeNature.Times = value.Times;
            typeNature.ProductId = value.ProductId;
            typeNature.DeleteFlag = false;
            typeNature.UpdateAt = DateTime.Now;
            typeNature.UpdateUser = userId;
            _unitOfWork.Repository<InfoComent>().UpdateRange(typeNature);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
