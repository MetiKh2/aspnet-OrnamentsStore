using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ornaments.Core.Services.Interfaces;

namespace MetiJob.Api.Filters;

public class PermissionChecker : AuthorizeAttribute, IAsyncAuthorizationFilter
{

    private IUserService _userService;
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            string userName = context.HttpContext.User.Identity.Name;
            var user = await _userService.GetByName(userName);
            if (!user.IsAdmin)
            {
                context.Result = new RedirectResult("/Login");
            }
        }

        else
        {
            context.Result = new RedirectResult("/Login");
        }


    }
   
     
}