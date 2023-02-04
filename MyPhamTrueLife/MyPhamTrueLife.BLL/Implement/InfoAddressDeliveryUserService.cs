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
    public class InfoAddressDeliveryUserService : IInfoAddressDeliveryUserService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoAddressDeliveryUserService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateInfoAddressDeliveryUserAsync(InfoAddressDeliveryUser value)
        {
            if (value == null)
            {
                return false;
            }
            value.CreateAt = DateTime.Now;
            value.CreateUser = value.UserId;
            value.DeleteFlag = false;
            await _unitOfWork.Repository<InfoAddressDeliveryUser>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> UpdateInfoAddressDeliveryUserAsync(InfoAddressDeliveryUser value)
        {
            if (value == null)
            {
                return false;
            }
            value.UpdateAt = DateTime.Now;
            value.UpdateUser = value.UserId;
            value.DeleteFlag = false;
            _unitOfWork.Repository<InfoAddressDeliveryUser>().Update(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> DeleteInfoAddressDeliveryUserAsync(int id)
        {
            if (id == null || id <= 0)
            {
                return false;
            }
            var value = await _unitOfWork.Repository<InfoAddressDeliveryUser>().Where(x => x.DeleteFlag != true && x.AddressDeliveryId.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
            if (value == null)
            {
                return false;
            }
            value.UpdateAt = DateTime.Now;
            value.UpdateUser = value.UserId;
            value.DeleteFlag = true;
            _unitOfWork.Repository<InfoAddressDeliveryUser>().Update(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<List<InfoAddressDeliveryUser>> ListInfoAddressDeliveryUserAsync(int usserId)
        {
            if (usserId == null || usserId <= 0)
            {
                return null;
            }
            var listUserAddres = await _unitOfWork.Repository<InfoAddressDeliveryUser>().Where(x => x.DeleteFlag != true && x.UserId.Equals(usserId)).AsNoTracking().ToListAsync();
            return listUserAddres;
        }
    }
}
