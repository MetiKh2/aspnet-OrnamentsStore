using Ornaments.Core.Dtos.Ornament;
using Ornaments.Core.Dtos.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Core.Dtos.Order
{
    public class FilterOrderDto:BasePaging
    {
        public List<DataAccess.Entities.Order.Order> Orders { get; set; }
        public bool IsPay { get; set; }
        public string Phone{ get; set; }
        public string UserId { get; set; }


        #region methods
        public FilterOrderDto SetOrders(List<DataAccess.Entities.Order.Order> orders)
		{
			this.Orders = orders;
			return this;
		}
		public FilterOrderDto SetPaging(BasePaging paging)
		{
			this.PageId = paging.PageId;
			this.AllEntitiesCount = paging.AllEntitiesCount;
			this.EndPage = paging.EndPage;
			this.StartPage = paging.StartPage;
			this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
			this.SkipEntity = paging.SkipEntity;
			this.TakeEntity = paging.TakeEntity;
			this.PageCount = paging.PageCount;
			return this;
		}
		#endregion
	}
}
