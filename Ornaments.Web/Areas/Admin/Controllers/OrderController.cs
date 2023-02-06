using MetiJob.Api.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ornaments.Core.Dtos.Order;
using Ornaments.Core.Services.Interfaces;

namespace Ornaments.Web.Areas.Admin.Controllers
{
	[Authorize]
    [PermissionChecker]
	[Area("Admin")]
    public class OrderController : Controller
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}
        [HttpGet("admin/orders")]
        public async Task<IActionResult> Index(FilterOrderDto filter)
        {
            filter.IsPay = true;
            return View(await _orderService.FilterOrders(filter));
        }
        [HttpGet("admin/orderdetails/{orderId}")]
        public async Task<IActionResult> OrderDetails(long orderId)
        {
            return View(await _orderService.GetOrderDetails(orderId));
        }
        [HttpPost("admin/order/addstatus")]
        public async Task<IActionResult> AddStatus(string status,long orderId)
        {
            await _orderService.AddStatus(status, orderId);
            return RedirectToAction(nameof(Index));
        }


    }
}
