using MyPhamTrueLife.BLL.Implement;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoProductService
    {
        Task<bool> InsertInfoProductAsync(InfoProductInsertLogin value, int userId);
        //Sản phẩm nè
        Task<bool> InsertInfoProductNewAsync(ThongTinThemSanPham value, int userId);

        Task<ProductDetailAdmin> ProductDetailAsync(int id);
    }
}
