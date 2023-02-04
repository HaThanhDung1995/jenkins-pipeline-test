using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoPromotionService
    {
        Task<bool> InsertInfoPromotionAsync(InfoPromotion value, int userId);
        Task<bool> UpdateInfoPromotionAsync(InfoPromotion value, int userId);
        Task<bool> DeleteInfoPromotionAsync(int natureId, int userId);
        Task<ResponseList> ListInfoPromotionAsync(int page = 1, int limit = 25);
        Task<InfoPromotion> DetailInfoPromotionAsync(int natureId);
    }
}
