﻿using Microsoft.EntityFrameworkCore;
using Ornaments.Core.Dtos.Order;
using Ornaments.Core.Extensions;
using Ornaments.Core.Services.Interfaces;
using Ornaments.DataAccess.Entities.Order;
using Ornaments.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Core.Services.Implements
{
    public class OrderService:IOrderService
    {
        #region cons
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }


        #endregion

        #region order
        public async Task<long> AddOrderForUser(string userId)
        {
            var order = new Order
            {
                IsPay = false,
                UserId = userId,
                AdminDescription = "",
                TrackingCode = ""
            };
            await _orderRepository.AddEntity(order);
            await _orderRepository.SaveChangesAsync();
            return order.Id;
        }

        public async Task<Order> GetUserLatestOrder(string userId)
        {
            if (!await _orderRepository.GetQuery().AnyAsync(p => p.UserId == userId && !p.IsPay))
                await AddOrderForUser(userId);
            return await _orderRepository.GetQuery().Include(p => p.OrderDetails).ThenInclude(p => p.Ornament).Include(p => p.OrderDetails).ThenInclude(p => p.Ornament).FirstOrDefaultAsync(p => p.UserId == userId && !p.IsPay);
        }

        public async Task AddProductToOpenOrder(AddProductToOrderDto order, string userId)
        {
            var openOrder = await GetUserLatestOrder(userId);
            var openOrderDetail =
                openOrder.OrderDetails.FirstOrDefault(p => p.OrnamentId == order.OrnamentId);
            if (openOrderDetail == null)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = openOrder.Id,
                    OrnamentId = order.OrnamentId,
                    Count = order.Count,
                };
                await _orderDetailRepository.AddEntity(orderDetail);
            }
            else
            {
                openOrderDetail.Count += order.Count;
                _orderDetailRepository.EditEntity(openOrderDetail);
            }
            await _orderDetailRepository.SaveChangesAsync();
        }

        public async Task<int> GetTotalOrderPriceForPayment(string userId)
        {
            var openOrder = await GetUserLatestOrder(userId);
            if (openOrder == null) return 0;
            //return openOrder.OrderDetails.Sum(p => (p.Product.Price+p.ProductColor.Price)*p.Count);    
            return openOrder.OrderDetails.ToList().IntSumOrderDetailsWithDiscount();
        }

        public async Task PayOrderProductPriceToStore(string userId, string refId)
        {
            var openOrder = await GetUserLatestOrder(userId);
            foreach (var item in openOrder.OrderDetails)
            {
                // var discount = 0;
                // var totalPrice = (item.Product.Price + item.ProductColor.Price)*item.Count;
                var totalPrice = item.IntOrderDetailWithDiscountPriceAndCount();
                //await _storeWalletService.AddWallet(new dataLayer.Entities.Wallet.StoreWallet
                //{
                //    Amount = (int)Math.Ceiling(totalPrice * item.Product.SiteProfit / (double)100),
                //    Description = $"حق فروش {item.Product.Title} به تعداد {item.Count} با سهم تعیین شده {item.Product.SiteProfit} درصد",
                //    StoreId = item.Product.SellerId,
                //    TransactionType = dataLayer.Entities.Wallet.TransactionType.Deposit,
                //});
                item.Price = totalPrice;
                _orderDetailRepository.EditEntity(item);
            }
            openOrder.IsPay = true;
            openOrder.PermentDate = DateTime.Now;
            openOrder.TrackingCode = refId;
            _orderRepository.EditEntity(openOrder);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task<bool> RemoveOrderDetail(long detailId, string userId)
        {
            var openOrder = await GetUserLatestOrder(userId);
            var orderDetail = openOrder.OrderDetails.SingleOrDefault(p => p.Id == detailId);
            if (orderDetail == null) return false;
            else
            {
                _orderDetailRepository.DeleteEntity(orderDetail);
                await _orderDetailRepository.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> ChangeOrderDetailCount(long detailId, string userId, int count)
        {
            if (count < 1) return false;
            var openOrder = await GetUserLatestOrder(userId);
            var orderDetail = openOrder.OrderDetails.SingleOrDefault(p => p.Id == detailId);
            if (orderDetail == null) return false;
            else
            {
                orderDetail.Count = count;
                _orderDetailRepository.EditEntity(orderDetail);
                await _orderDetailRepository.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> CloseUserOpenOrderAfterPayment(string userId, string trackingCode)
        {
            var openOrder = await GetUserLatestOrder(userId);
            openOrder.IsPay = true;
            openOrder.PermentDate = DateTime.Now;
            openOrder.TrackingCode = trackingCode;
            _orderRepository.EditEntity(openOrder);
            await _orderRepository.SaveChangesAsync();
            return true;
        }

        #endregion
    }
}