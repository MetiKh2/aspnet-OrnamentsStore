using Microsoft.AspNetCore.Mvc;
using Ornaments.Core.Services.Interfaces;

namespace Ornaments.Web.ViewComponents
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly IOrnamentService _ornamentService;

        public HeaderViewComponent(IOrnamentService ornamentService)
        {
            _ornamentService = ornamentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _ornamentService.GetCategories();
            return View("Header",categories);
        }
    }
}
