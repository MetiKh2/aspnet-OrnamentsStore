using Ornaments.DataAccess.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Core.Extensions
{
    public static class OrderExtensions
    {
        public static string OrderDetailWithDiscountPrice(this OrderDetail orderDetail)
        {
            return ((orderDetail.Ornament.Price )).ToString("n0");
        }
        public static string OrderDetailWithDiscountPriceAndCount(this OrderDetail orderDetail)
        {
            return ((orderDetail.Ornament.Price) * orderDetail.Count).ToString("n0");
        }
        public static int IntOrderDetailWithDiscountPriceAndCount(this OrderDetail orderDetail)
        {
            return (int)((orderDetail.Ornament.Price) * orderDetail.Count );
        }
        public static string StringSumOrderDetailsWithDiscount(this List<OrderDetail> orderDetails)
        {
            int total = 0;
            foreach (var item in orderDetails)
            {
                total += (int)((item.Ornament.Price ) * item.Count );
            }
            return total.ToString("n0");
        }
        public static int IntSumOrderDetailsWithDiscount(this List<OrderDetail> orderDetails)
        {
            int total = 0;
            foreach (var item in orderDetails)
            {
                total += (int)((item.Ornament.Price) * item.Count);
            }
            return total;
        }
    }
}
