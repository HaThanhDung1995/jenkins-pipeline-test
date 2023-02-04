using MyPhamTrueLife.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Interface
{
    public interface IInfoAddressDeliveryUserService
    {
        Task<bool> CreateInfoAddressDeliveryUserAsync(InfoAddressDeliveryUser value);
        Task<bool> UpdateInfoAddressDeliveryUserAsync(InfoAddressDeliveryUser value);
        Task<bool> DeleteInfoAddressDeliveryUserAsync(int id);
        Task<List<InfoAddressDeliveryUser>> ListInfoAddressDeliveryUserAsync(int usserId);
    }
}
