using Microsoft.AspNetCore.Mvc;

namespace Ornaments.Web.Areas.Admin.ViewComponents
{
    #region Header

    public class AdminHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("AdminHeader");
        }
    }

    #endregion

  
}
