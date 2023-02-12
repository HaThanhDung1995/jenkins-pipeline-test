using Microsoft.EntityFrameworkCore;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPhamTrueLife.BLL.Ultil;

namespace MyPhamTrueLife.BLL.Implement
{
    public class InfoOrderService : IInfoOrderService
    {
        public readonly dbDevNewContext _unitOfWork;
        public InfoOrderService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddOrderAsync(InfoOrderInsertRequest value)
        {
            if (value == null || value.listCart == null)
            {
                return false;
            }
            var order = new InfoOrder();
            order.UserId = value.UserId;
            order.IsAccept = false;
            order.Total = value.Total;
            order.Status = "CHUADUYET";
            order.IsPay = value.IsPay;
            if (value.IsPay == true)
            {
                order.Status = "DADUYET";
            }
            order.StatusOrder = "ONLINE";
            order.SeverId = value.ServerId;
            order.DateTimeD = DateTime.Now;
            order.AddressDeliveryId = value.AddressDeliveryId;
            order.CreateAt = DateTime.Now;
            order.CreateUser = value.UserId;
            order.DeleteFlag = false;
            //var newOrderCode = "";
            //var orderMax = await _unitOfWork.Repository<InfoOrder>().AsNoTracking().OrderByDescending(x => x.OrderCode).FirstOrDefaultAsync();
            //if (orderMax != null)
            //{
            //    newOrderCode = FunctionUtils.CheckLetter("HD", orderMax.OrderCode ?? "", 4);
            //}
            //else
            //{
            //    newOrderCode = FunctionUtils.CheckLetter("HD", "", 4);
            //}
            //order.OrderCode = newOrderCode;
            await _unitOfWork.Repository<InfoOrder>().AddAsync(order);
            await _unitOfWork.SaveChangeAsync();
            if (value.listCart[0].PromotionId == null)
            {
                foreach (var item in value.listCart)
                {
                    var cart = new InfoOrderDetail();
                    cart.OrderId = order.OrderId;
                    cart.ProductId = item.ProductId.Value;
                    cart.Amount = item.Quantity;
                    cart.CapacityId = item.CapacityId;
                    if (item.Capacity != null)
                    {
                        var product = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId == item.ProductId && x.CapacityId.Equals(item.Capacity)).OrderByDescending(z => z.StartAt).AsNoTracking().ToListAsync();
                        if (product != null && product.Count > 0)
                        {
                            cart.Prize = product[0].Price;
                        }
                    }
                    else
                    {
                        var product = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId == item.ProductId).OrderByDescending(z => z.StartAt).AsNoTracking().ToListAsync();
                        if (product != null && product.Count > 0)
                        {
                            cart.Prize = product[0].Price;
                        }
                    }    

                    var pro = await _unitOfWork.Repository<InfoProduct>().Where(x => x.ProductId.Equals(item.ProductId) && x.DeleteFlag != true).AsNoTracking().FirstOrDefaultAsync();
                    if (pro.IsExpiry == true)
                    {
                        if (item.Capacity != null)
                        {
                            var expiry = await _unitOfWork.Repository<InfoExpiryProduct>().Where(x => x.DeleteFlag != true && x.ProductId == item.ProductId && x.Amount > item.Quantity && x.CapacityId.Equals(item.CapacityId)).OrderBy(z => z.StartAt).AsNoTracking().ToListAsync();
                            if (expiry != null && expiry.Count > 0)
                            {
                                cart.StartAt = expiry[0].StartAt;
                                cart.EndAd = expiry[0].EndAt;
                            }
                        }
                        if (item.Capacity == null)
                        {
                            var expiry = await _unitOfWork.Repository<InfoExpiryProduct>().Where(x => x.DeleteFlag != true && x.ProductId == item.ProductId && x.Amount > item.Quantity).OrderBy(z => z.StartAt).AsNoTracking().ToListAsync();
                            if (expiry != null && expiry.Count > 0)
                            {
                                cart.StartAt = expiry[0].StartAt;
                                cart.EndAd = expiry[0].EndAt;
                            }
                        }
                    }
                    cart.CreateAt = DateTime.Now;
                    cart.CreateUser = item.UserId;
                    cart.DeleteFlag = false;

                    await _unitOfWork.Repository<InfoOrderDetail>().AddAsync(cart);
                }
                await _unitOfWork.SaveChangeAsync();

                //Xóa sản phẩm khỏi giỏ hàng
                foreach (var item1 in value.listCart)
                {
                    var cart = await _unitOfWork.Repository<InfoCart>().Where(x => x.CartId == item1.CartId).AsNoTracking().FirstOrDefaultAsync();
                    if (cart != null)
                    {
                        cart.DeleteFlag = true;
                        _unitOfWork.Repository<InfoCart>().Update(cart);
                        await _unitOfWork.SaveChangeAsync();
                    }
                }
            }
            return true;
        }

        public async Task<ResponseList> GetListOrderAdminAsync(int page = 1, int limit = 25)
        {
            var result = new ResponseList();
            var listOrder = _unitOfWork.Repository<InfoOrder>().Where(x => x.DeleteFlag != true);
            
            var listData = await( from a in listOrder
                           select new InfoOrderList()
                           {
                               OrderId = a.OrderId,
                               UserId = a.UserId,
                               IsAccept = a.IsAccept,
                               StaffId = a.StaffId,
                               Total = a.Total,
                               Status = a.Status,
                               IsPay = a.IsPay,
                               StatusOrder = a.StatusOrder,
                               SeverId = a.SeverId,
                               DateTimeD = a.DateTimeD,
                               AddressDeliveryId = a.AddressDeliveryId,
                               //FullName = b.FullName,
                               //FullNameStaff = c.FullName,
                               CreateAt = a.CreateAt
                           }).AsNoTracking().ToListAsync();
            foreach (var item in listData)
            {
                item.FullName = item.UserId == null ? "" : await _unitOfWork.Repository<InfoUser>().Where(x => x.DeleteFlag != true && x.UserId == item.UserId).AsNoTracking().Select(z=>z.FullName).FirstOrDefaultAsync();
                item.FullNameStaff = item.StaffId == null ? "" : await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId == item.StaffId).AsNoTracking().Select(z => z.FullName).FirstOrDefaultAsync();
                item.infoUser = item.UserId == null ? null : await _unitOfWork.Repository<InfoUser>().Where(x => x.DeleteFlag != true && x.UserId == item.UserId).AsNoTracking().FirstOrDefaultAsync();
                item.infoStaff = item.StaffId == null ? null : await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId == item.StaffId).AsNoTracking().FirstOrDefaultAsync();
                //item.infoSever = item.SeverId == null ? null : await _unitOfWork.Repository<InfoSever>().Where(x => x.DeleteFlag != true && x.SeverId == order.SeverId).AsNoTracking().FirstOrDefaultAsync();
                var infoAdd = item.AddressDeliveryId == null ? null : await _unitOfWork.Repository<InfoAddressDeliveryUser>().Where(x => x.DeleteFlag != true && x.AddressDeliveryId == item.AddressDeliveryId.Value).AsNoTracking().FirstOrDefaultAsync();

                var addRess = new IndoAddressDeliveryReq();
                PropertyCopier<InfoAddressDeliveryUser, IndoAddressDeliveryReq>.Copy(infoAdd, addRess);
                item.infoAddressDeliveryUser = new IndoAddressDeliveryReq();
                item.infoAddressDeliveryUser = addRess;

            }
            var totalRows = listData.Count();
            result.Paging = new Paging(totalRows, page, limit);
            int start = result.Paging.start;
            listData = listData.OrderByDescending(z=>z.CreateAt).Skip(start).Take(limit).ToList();
            result.ListData = listData;
            return result;
        }

        public async Task<bool> UpdateOrderAsync(InfoOrderUpdateStatus value)
        {
            var order = await _unitOfWork.Repository<InfoOrder>().Where(x => x.DeleteFlag != true && x.OrderId == value.OrderId).AsNoTracking().FirstOrDefaultAsync();
            if (order == null)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(value.Status))
            {
                order.Status = value.Status;
                order.StaffId = value.StaffId;
            }
            if (value.IsPay != null)
            {
                order.IsPay = value.IsPay;
            }
            order.UpdateAt = DateTime.Now;
            order.UpdateUser = value.StaffId;
            order.DeleteFlag = false;
            _unitOfWork.Repository<InfoOrder>().UpdateRange(order);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<XemChiTietDonHangRes> XemChiTietDonHang(int orderId)
        {
            var order = await _unitOfWork.Repository<InfoOrder>().Where(x => x.DeleteFlag != true && x.OrderId == orderId).AsNoTracking().FirstOrDefaultAsync();
            if (order != null)
            {
                var info = new XemChiTietDonHangRes();
                info.OrderId = order.OrderId;
                info.UserId = order.UserId;
                info.IsAccept = order.IsAccept;
                info.StaffId = order.StaffId;
                info.Total = order.Total;
                info.Status = order.Status;
                info.IsPay = order.IsPay;
                info.StatusOrder = order.StatusOrder;
                info.SeverId = order.SeverId;
                info.DateTimeD = order.DateTimeD;
                info.AddressDeliveryId = order.AddressDeliveryId;

                info.infoUser = order.UserId == null ? null : await _unitOfWork.Repository<InfoUser>().Where(x => x.DeleteFlag != true && x.UserId == order.UserId).AsNoTracking().FirstOrDefaultAsync();
                info.infoStaff = order.StaffId == null ? null : await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId == order.StaffId).AsNoTracking().FirstOrDefaultAsync();
                info.infoSever = order.SeverId == null ? null : await _unitOfWork.Repository<InfoSever>().Where(x => x.DeleteFlag != true && x.SeverId == order.SeverId).AsNoTracking().FirstOrDefaultAsync();
                info.infoAddressDeliveryUser = order.AddressDeliveryId == null ? null : await _unitOfWork.Repository<InfoAddressDeliveryUser>().Where(x => x.DeleteFlag != true && x.AddressDeliveryId == order.AddressDeliveryId.Value).AsNoTracking().FirstOrDefaultAsync();

                info.thongTinChiTietDonHangs = new List<ThongTinChiTietDonHang>();
                var detailOrder = await _unitOfWork.Repository<InfoOrderDetail>().Where(x => x.DeleteFlag != true && x.OrderId == order.OrderId).AsNoTracking().ToListAsync();
                foreach (var item in detailOrder)
                {
                    var info1 = new ThongTinChiTietDonHang();
                    info1.OrderId = item.OrderId;
                    info1.ProductId = item.ProductId;
                    info1.Amount = item.Amount;
                    info1.Prize = item.Prize;
                    info1.CapacityId = item.CapacityId;
                    info1.CapacityName = item.CapacityId == null ? null : await _unitOfWork.Repository<InfoCapacity>().Where(x => x.DeleteFlag != true && x.CapacityId == item.CapacityId).AsNoTracking().Select(z=>z.CapacityName).FirstOrDefaultAsync();
                    info1.StartAt = item.StartAt;
                    info1.EndAt = item.EndAd;
                    var product = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true && x.ProductId == item.ProductId).AsNoTracking().FirstOrDefaultAsync();
                    if (product != null)
                    {
                        info1.Avatar = product.Avatar;
                        info1.ProductName = product.ProductName;
                        if (product.NatureId != null)
                        {
                            var nature = await _unitOfWork.Repository<InfoNature>().Where(x => x.DeleteFlag != true && x.NatureId == product.NatureId).AsNoTracking().FirstOrDefaultAsync();
                            if (nature != null)
                            {
                                info1.ProductName += " - " + nature.NatureName;
                            }
                        }
                    }
                    info.thongTinChiTietDonHangs.Add(info1);
                }

                return info;
            }
            return null;
        }
    }
}
