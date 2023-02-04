using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyPhamTrueLife.BLL.Implement
{
    public class InfoComentService : IInfoComentService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoComentService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddCommet(InfoCommentRequest value)
        {
            if (value == null)
            {
                return false;
            }
            var comment = new InfoComent();
            comment.ProductId = value.ProductId;
            comment.UserId = value.UserId;
            comment.Content = value.Content;
            var cmt = await _unitOfWork.Repository<InfoComent>().Where(x => x.ProductId.Equals(value.ProductId) && x.UserId.Equals(value.UserId)).AsNoTracking().ToListAsync();
            if (cmt != null && cmt.Count > 0)
            {
                comment.Times = cmt.Count + 1;
            }
            if (cmt == null || cmt.Count <= 0)
            {
                comment.Times = 1;
            }
            comment.CreateAt = DateTime.Now;
            comment.CreateUser = value.UserId;
            comment.DeleteFlag = false;
            await _unitOfWork.Repository<InfoComent>().AddAsync(comment);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
