using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ornaments.Core.Dtos.Common;
using Ornaments.Core.Dtos.Order;
using Ornaments.Core.Services.Interfaces;
using Ornaments.Web.Extensions;
using Ornaments.Web.Http;
using RadioMeti.Application.Utilities.Utils;

namespace Ornaments.Web.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IOrderService orderService;
		private readonly IUserService userService;
		private readonly IPaymentService _paymentService;
		public OrderController(IOrderService orderService, IUserService userService, IPaymentService paymentService)
		{
			this.orderService = orderService;
			this.userService = userService;
			_paymentService = paymentService;
		}

		#region add product to open order
		[HttpPost("add-product-to-order")]
		[AllowAnonymous]
		public async Task<IActionResult> AddProductToOrder(AddProductToOrderDto order)
		{
			if (ModelState.IsValid)
			{
				if (User.Identity.IsAuthenticated)
				{
					await orderService.AddProductToOpenOrder(order, User.UserId());
					return RedirectToAction(nameof(UserOpenOrder));
					//return JsonResponeResult.SendStatus(JsonReqponseStatusType.Success, "محصول با موفقیت به سبد خرید اضافه شد ", null, false);
				}
				else return JsonResponeResult.SendStatus(JsonReqponseStatusType.Error, "برای خرید باید وارد سایت شوید ", null, false);
			}
			return JsonResponeResult.SendStatus(JsonReqponseStatusType.Error, "در ثبت اطلعات خطایی رخ داد", null, false);
		}
		#endregion

		#region open cart
		[HttpGet("open-order")]
		public async Task<IActionResult> UserOpenOrder(string error = "")
		{
			ViewBag.error = error;
			return View(await orderService.GetUserLatestOrder(User.UserId()));
		}
		#endregion

		#region remove product from order
		[HttpGet("remove-order-item/{detailId}")]
		public async Task<IActionResult> RemoveProductFromOrder(long detailId)
		{
			if (await orderService.RemoveOrderDetail(detailId, User.UserId()))
			{
				return JsonResponeResult.SendStatus(JsonReqponseStatusType.Success, "محصول با موفقیت پاک شد", null, true);

			}
			return JsonResponeResult.SendStatus(JsonReqponseStatusType.Error, "محصول یافت نشد", PartialView(await orderService.GetUserLatestOrder(User.UserId())), true);
		}
		#endregion

		#region open order partial
		[HttpGet("change-detail-count/{detailId}/{count}")]
		public async Task<IActionResult> ChangeDetailCount(long detailId, int count)
		{
			await orderService.ChangeOrderDetailCount(detailId, User.UserId(), count);
			return PartialView(await orderService.GetUserLatestOrder(User.UserId()));

		}
		#endregion
		#region pay order
		[HttpGet("pay-order")]
		public async Task<IActionResult> PayUserOrder(string address, string phone)
		{
			if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
				return RedirectToAction(nameof(UserOpenOrder), new { error = "آدرس و تلفن را وارد کنید" });
			else
				if (!await orderService.AddAddressAndPhone(User.UserId(), address, phone))
				return RedirectToAction(nameof(UserOpenOrder), new { error = "خطایی به وجود آمده" });

			var openOrderAmount = await orderService.GetTotalOrderPriceForPayment(User.UserId());
			string callBackUrl = PathExtension.DomainAddress + Url.RouteUrl("ZarinpalPaymentResult");
			string redirectUrl = "";
			var status = _paymentService.CreatePaymentRequest(null, openOrderAmount, "خرید از سایت ما", callBackUrl, ref redirectUrl, null);
			if (status == PaymentStatus.St100) return Redirect(redirectUrl);
			return RedirectToAction("UserOpenOrder");
		}
		#endregion

		#region callback zarin pal
		[HttpGet("payment-result", Name = "ZarinpalPaymentResult")]
		public async Task<IActionResult> CallbackZarinPal()
		{
			string authority = _paymentService.GetAuthorityCodeFromCallback(HttpContext);

			if (authority == "")
			{
				TempData["ErrorMessage"] = "عملیات پرداخت با شکست مواجه شد";
			}
			var openOrderAmount = await orderService.GetTotalOrderPriceForPayment(User.UserId());
			long refId = 0;
			var res = _paymentService.PaymentVerfication(null, openOrderAmount, authority, ref refId);
			if (res == PaymentStatus.St100)
			{
				TempData["SuccessMessage"] = "پرداخت موفقیت آمیز بود";
				TempData["InfoMessage"] = "کد پیگیری شما" + refId;
				await orderService.PayOrderProductPriceToStore(User.UserId(), refId.ToString());
				return View();
			}
			else
			{
				TempData["ErrorMessage"] = "پرداخت موفقیت آمیز نبود";
				TempData["InfoMessage"] = "کد پیگیری شما" + refId;
				return View();
			}
		}
		#endregion
		[HttpGet("orders")]
		public async Task<IActionResult> UserPaidOrders()
		{
			var model = await orderService.FilterOrders(new FilterOrderDto
			{
				UserId = User.UserId(),
				TakeEntity = 100,
				IsPay = true
			});

            return View(model.Orders);
		}
	}
}
