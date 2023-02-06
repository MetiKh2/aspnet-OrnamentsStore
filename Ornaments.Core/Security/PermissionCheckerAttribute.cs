using Ornaments.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Core.Security
{
    //public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    //{
    //    private IUserService _userService;
    //    public async Task OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        _userService= (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
    //        if (context.HttpContext.User.Identity.IsAuthenticated)
    //        {
    //            string userName = context.HttpContext.User.Identity.Name;
    //            var user =await _userService.GetByName(userName);
    //            if (!user.IsAdmin)
    //            {
    //                context.Result = new RedirectResult("/Login");
    //            }

    //        }

    //        else
    //        {
    //            context.Result = new RedirectResult("/Login");
    //        }


    //    }

    //    void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
