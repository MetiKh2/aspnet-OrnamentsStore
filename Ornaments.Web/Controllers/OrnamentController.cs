using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ornaments.Core.Dtos.Ornament;
using Ornaments.Core.Dtos.Ornament.Comment;
using Ornaments.Core.Services.Interfaces;
using Ornaments.Web.Extensions;

namespace Ornaments.Web.Controllers
{
    public class OrnamentController : Controller
    {
        private readonly IOrnamentService _ornamentService;

        public OrnamentController(IOrnamentService ornamentService)
        {
            _ornamentService = ornamentService;
        }

        [HttpGet("Ornament/{id}")]
        public async Task<IActionResult >Index(long id)
        {
            var model = await _ornamentService.GetOrnament(id);
            if (model == null) return NotFound();
			return View(model);
        }
        [HttpGet("Ornament/OrnamentsList")]
        public async Task<IActionResult>OrnamentsList(FilterOrnamentDto filter)
        {
            if (filter.TakeEntity == null || filter.TakeEntity < 1)
                filter.TakeEntity = 30;
            filter.IsDisplay = true;
            var model = await _ornamentService.FilterOrnaments(filter);
            return View(model.Ornaments);
        }
		[HttpPost("Ornament/add-comment")]
        [Authorize]
		public async Task<IActionResult> AddComment(AddCommentDto add)
		{
            add.UserId = User.UserId();
            await _ornamentService.AddComment(add);
			return RedirectToAction("Index",new { id=add.OrnamentId});
		}
	}
}
