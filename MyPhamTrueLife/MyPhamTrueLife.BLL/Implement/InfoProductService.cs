using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPhamTrueLife.BLL.Implement
{
    public class InfoProductService : IInfoProductService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoProductService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> InsertInfoProductAsync(InfoProductInsertLogin value, int userId)
        {
            if (value == null || userId <= 0)
            {
                return false;
            }
            else
            {
                var infoProuct = new InfoProduct();
                infoProuct.ProductName = value.ProductName;
                infoProuct.Describe = value.Describe;
                infoProuct.Amount = 0;
                infoProuct.IsExpiry = value.IsExpiry;
                infoProuct.StatusProduct = "HETHANG";
                infoProuct.Trademark = value.Trademark;
                infoProuct.NatureId = value.NatureId;
                infoProuct.IsAroma = value.IsAroma;
                infoProuct.Avatar = value.Avatar;
                infoProuct.TypeProductId = value.TypeProductId;
                infoProuct.CreateUser = userId;
                infoProuct.CreateAt = DateTime.Now;
                infoProuct.DeleteFlag = false;
                var result = await _unitOfWork.Repository<InfoProduct>().AddAsync(infoProuct);
                if (value.ListImage != null)
                {
                    foreach (var image in value.ListImage)
                    {
                        var infoImageProduct = new InfoImageProduct();
                        infoImageProduct.ProductId = result.Entity.ProductId;
                        infoImageProduct.Img = image;
                        infoImageProduct.CreateUser = userId;
                        infoImageProduct.CreateAt = DateTime.Now;
                        infoImageProduct.DeleteFlag = false;
                        await _unitOfWork.Repository<InfoImageProduct>().AddAsync(infoImageProduct);
                    }
                }
                if (value.ListCapacity != null)
                {
                    foreach (var capacityId in value.ListCapacity)
                    {
                        var infoCapacityProduct = new InfoCapacityProduct();
                        infoCapacityProduct.ProductId = result.Entity.ProductId;
                        infoCapacityProduct.CapacityId = capacityId;
                        infoCapacityProduct.CreateUser = userId;
                        infoCapacityProduct.CreateAt = DateTime.Now;
                        infoCapacityProduct.DeleteFlag = false;
                        await _unitOfWork.Repository<InfoCapacityProduct>().AddAsync(infoCapacityProduct);
                    }
                }
            }
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
