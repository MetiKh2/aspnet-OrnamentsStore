using Microsoft.AspNetCore.Http;
using Ornaments.Core.Dtos.Common;
using Ornaments.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZarinpalSandbox;
namespace Ornaments.Core.Services.Implements
{
	public class PaymentService : IPaymentService
	{
		public PaymentStatus CreatePaymentRequest(string merchantId, int amount, string description, string callbackUrl, ref string redirectUrl, string userEmail)
		{
			var payment = new Payment(amount);
			var res = payment.PaymentRequest(description, callbackUrl, userEmail);
			if (res.Result.Status == (int)PaymentStatus.St100)
			{
				redirectUrl = "https://zarinpal.sandbox.com/pg/startpay/" + res.Result.Authority;
				redirectUrl = "https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority;
				return (PaymentStatus)res.Result.Status;

			}
			return (PaymentStatus)res.Status;
		}
		public string GetAuthorityCodeFromCallback(HttpContext context)
		{
			if (context.Request.Query["Status"] == "" || context.Request.Query["Status"].ToString().ToLower() != "ok" || context.Request.Query["Authority"] == "")
				return null;
			string authority = context.Request.Query["Authority"];
			return authority.Length == 36 ? authority : null;
		}

		public PaymentStatus PaymentVerfication(string merchantId, int amount, string authority, ref long refId)
		{
			var payment = new Payment(amount);
			var res = payment.Verification(authority).Result;
			refId = res.RefId;
			return (PaymentStatus)res.Status;

		}
	}
}
