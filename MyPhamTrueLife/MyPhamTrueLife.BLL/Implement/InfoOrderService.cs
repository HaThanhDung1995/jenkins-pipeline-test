﻿using Microsoft.EntityFrameworkCore;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            order.DeleteFlag = true;
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
                        var product = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.CapacityId.Equals(item.Capacity)).OrderByDescending(z => z.StartAt).AsNoTracking().ToListAsync();
                        if (product != null && product.Count > 0)
                        {
                            cart.Prize = product[0].Price;
                        }
                    }
                    if (item.Capacity == null)
                    {
                        var product = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true).OrderByDescending(z => z.StartAt).AsNoTracking().ToListAsync();
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
                            var expiry = await _unitOfWork.Repository<InfoExpiryProduct>().Where(x => x.DeleteFlag != true && x.Amount > item.Quantity && x.CapacityId.Equals(item.CapacityId)).OrderBy(z => z.StartAt).AsNoTracking().ToListAsync();
                            if (expiry != null && expiry.Count > 0)
                            {
                                cart.StartAt = expiry[0].StartAt;
                                cart.EndAd = expiry[0].EndAt;
                            }
                        }
                        if (item.Capacity == null)
                        {
                            var expiry = await _unitOfWork.Repository<InfoExpiryProduct>().Where(x => x.DeleteFlag != true && x.Amount > item.Quantity).OrderBy(z => z.StartAt).AsNoTracking().ToListAsync();
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
            }
            return true;
        }
    }
}