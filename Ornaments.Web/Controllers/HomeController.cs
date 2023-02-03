using Microsoft.AspNetCore.Mvc;
using Ornaments.Core.Dtos.Home;
using Ornaments.Core.Dtos.Ornament;
using Ornaments.Core.Services.Interfaces;
using System.Diagnostics;

namespace Ornaments.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOrnamentService _ornamentService;
        public HomeController(ILogger<HomeController> logger, IOrnamentService ornamentService)
        {
            _logger = logger;
            _ornamentService = ornamentService;
        }

        public async Task<IActionResult >Index()
        {
            var dto = new MainPageDto();
            var sliders = await _ornamentService.FilterOrnaments(new FilterOrnamentDto { IsSlider = true,IsDisplay=true,TakeEntity=10});
            var main = await _ornamentService.FilterOrnaments(new FilterOrnamentDto { IsMainPage= true,IsDisplay=true,TakeEntity=7});
            var mostViews = await _ornamentService.FilterOrnaments(new FilterOrnamentDto { MostViews= true,IsDisplay=true,TakeEntity=7});
			dto.SliderOrnaments =sliders.Ornaments;
			dto.MainOrnaments =main.Ornaments;
			dto.MostViewsOrnaments =mostViews.Ornaments;
			return View(dto);
        }


    }
}