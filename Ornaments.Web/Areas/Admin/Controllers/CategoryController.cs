using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ornaments.Core.Dtos.Ornament.Category;
using Ornaments.Core.Services.Interfaces;
using RadioMeti.Application.DTOs.Admin.Category.Create;
using RadioMeti.Application.DTOs.Admin.Category.Edit;

namespace Ornaments.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IOrnamentService _ornamentService;

        public CategoryController(IOrnamentService ornamentService)
        {
            _ornamentService = ornamentService;
        }
        [HttpGet("admin/Category")]
        public async Task<IActionResult> Index(FilterCategoryDto filter)
        {
            filter.TakeEntity = 5;
            return View(await _ornamentService.FilterCategories(filter));
        }
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost("admin/Category/CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto create)
        {
            if (ModelState.IsValid)
            {
                var result = await _ornamentService.CreateCategory(create);
                TempData["InfoMessage"] = result;
                return RedirectToAction("Index");
            }
            return View(create);
        }
        public async Task<IActionResult> EditCategory(long id)
        {
            var item = await _ornamentService.GetCategory(id);
            if (item== null) return NotFound();
            return View(new EditCategoryDto { CategoryName = item.CategoryName, Id = item.Id });
        }
        [HttpPost("admin/Category/EditCategory")]
        public async Task<IActionResult> EditCategory(EditCategoryDto edit, List<string> selectedClaims)
        {
            if (ModelState.IsValid)
            {
                var result = await _ornamentService.EditCategory(edit);
                if (result == null) return NotFound();
                    TempData["InfoMessage"] = result;
                    return RedirectToAction(nameof(Index));
            }
            return View(edit);
        }
        [HttpGet("admin/Category/DeleteCategory")]

        public async Task<IActionResult> DeleteCategory(long id)
        {
            var result = await _ornamentService.DeleteCategory(id);
            if (result == null) return NotFound();
            TempData["InfoMessage"] = result;
            return RedirectToAction(nameof(Index));
        }
    }
}
