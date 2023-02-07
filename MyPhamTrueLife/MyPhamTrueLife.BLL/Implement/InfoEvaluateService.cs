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
    public class InfoEvaluateService : IInfoEvaluateService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoEvaluateService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddEvaluate(InfoEvaluateRequest value)
        {
            if (value == null)
            {
                return false;
            }
            var evaluateUser = await _unitOfWork.Repository<InfoEvaluate>().Where(x => x.ProductId.Equals(value.ProductId) && value.UserId.Equals(value.UserId)).AsNoTracking().FirstOrDefaultAsync();
            if (evaluateUser != null)
            {
                return false;
            }
            var info = new InfoEvaluate();
            info.ProductId = value.ProductId;
            info.UserId = value.UserId;
            info.NumberStars = value.NumberStars;
            await _unitOfWork.Repository<InfoEvaluate>().AddAsync(info);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
