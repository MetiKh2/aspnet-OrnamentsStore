using Microsoft.AspNetCore.Http;
using Ornaments.Core.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Core.Services.Interfaces
{
	public interface IPaymentService
	{
		PaymentStatus CreatePaymentRequest(string merchantId, int amount, string description, string callbackUrl, ref string redirectUrl, string userEmail);
		PaymentStatus PaymentVerfication(string merchantId, int amount, string authority, ref long refId);
		string GetAuthorityCodeFromCallback(HttpContext context);
	}
}
