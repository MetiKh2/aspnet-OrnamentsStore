using Ornaments.Core.Dtos.Order;
using Ornaments.DataAccess.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Core.Services.Interfaces
{
    public interface IOrderService
    {
        #region order
        Task<long> AddOrderForUser(string userId);
        Task<Order> GetUserLatestOrder(string userId);
        Task<int> GetTotalOrderPriceForPayment(string userId);
        Task PayOrderProductPriceToStore(string userId, string refId);
        Task<bool> CloseUserOpenOrderAfterPayment(string userId, string trackingCode);
        #endregion

        #region order detail
        Task AddProductToOpenOrder(AddProductToOrderDto order, string userId);
        Task<bool> RemoveOrderDetail(long detailId, string userId);
        Task<bool> ChangeOrderDetailCount(long detailId, string userId, int count);
        #endregion
    }
}
