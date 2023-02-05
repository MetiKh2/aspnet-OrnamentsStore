using System.Security.Claims;
using System.Security.Principal;

namespace Ornaments.Web.Extensions
{
	public static class IdentityExtensions
	{
		public static string UserId(this ClaimsPrincipal claimsPrincipal)
		{
			if (claimsPrincipal != null)
			{
				var data = claimsPrincipal.Claims.SingleOrDefault(p => p.Type == ClaimTypes.NameIdentifier);
				if (data != null)
					return Convert.ToString(data.Value);
			}
			return default(string);
		}
		public static string UserId(this IPrincipal principal)
		{
			var user = (ClaimsPrincipal)principal;
			return user.UserId();
		}
	}
}
