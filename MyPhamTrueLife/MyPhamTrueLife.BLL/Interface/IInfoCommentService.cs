using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoCommentService
    {
        Task<bool> InsertCommentAsync(InfoComent value, int userId);
        Task<bool> UpdateCommentAsync(InfoComent value, int userId);
        Task<bool> DeleteCommentAsync(int productId, int userId);
        Task<ResponseList> ListCommentAsync(int page = 1, int limit = 25);
        Task<InfoComent> DetailCommentAsync(int productId, int userId);
    }
}
