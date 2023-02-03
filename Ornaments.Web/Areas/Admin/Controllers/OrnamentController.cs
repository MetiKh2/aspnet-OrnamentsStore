using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ornaments.Core.Dtos;
using Ornaments.Core.Dtos.Ornament;
using Ornaments.Core.Dtos.Ornament.Category;
using Ornaments.Core.Services.Interfaces;
using RadioMeti.Application.DTOs.Admin.Category.Create;
using RadioMeti.Application.DTOs.Admin.Category.Edit;
using RadioMeti.Application.Extensions;
using RadioMeti.Application.Utilities.Utils;

namespace Ornaments.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrnamentController : Controller
    {
        private readonly IOrnamentService _ornamentService;

        public OrnamentController(IOrnamentService ornamentService)
        {
            _ornamentService = ornamentService;
        }
        [HttpGet("admin/Ornament")]
        public async Task<IActionResult> Index(FilterOrnamentDto filter)
        {
            filter.TakeEntity = 5;
            return View(await _ornamentService.FilterOrnaments(filter));
        }
        public async Task<IActionResult >CreateOrnament()
        {
            ViewBag.Categories = await _ornamentService.GetCategories();
            return View();
        }
        [HttpPost("admin/Ornament/CreateOrnament")]
        public async Task<IActionResult> CreateOrnament(CreateOrnamentDto create,IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                #region upload images
                if (image != null)
                {
                    create.Image = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);

                    if (!image.AddImageToServer(create.Image, PathExtension.OrnamentImageOriginSever, 300, 200, PathExtension.OrnamentImageThumbSever))
                    {
                        TempData["ErrorMessage"] = "Something Is Wrong in upload image";
                        return View(create);
                    }
                }
                #endregion
                var result = await _ornamentService.CreateOrnament(create);
                TempData["InfoMessage"] = result;
                return RedirectToAction("Index");
            }
            ViewBag.Categories = await _ornamentService.GetCategories();
            return View(create);
        }
        public async Task<IActionResult> EditOrnament(long id)
        {
            var item = await _ornamentService.GetOrnament(id);
            if (item == null) return NotFound();
            ViewBag.Categories = await _ornamentService.GetCategories();
            return View(new EditOrnamentDto
            {
                Brand = item.Brand,
                Name = item.Name,
                CategoryId = item.CategoryId,
                Description = item.Description,
                Image = item.Image,
                Invertory = item.Invertory,
                IsDisplay = item.IsDisplay,
                IsInMainPage = item.IsInMainPage,
                IsSlider = item.IsSlider,
                Price = item.Price,
            });
        }
        [HttpPost("admin/Ornament/EditOrnament")]
        public async Task<IActionResult> EditOrnament(EditOrnamentDto edit, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                #region upload images
                if (image != null)
                {
                    edit.Image = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);

                    if (!image.AddImageToServer(edit.Image, PathExtension.OrnamentImageOriginSever, 300, 200, PathExtension.OrnamentImageThumbSever))
                    {
                        TempData["ErrorMessage"] = "Something Is Wrong in upload image";
                        return View(edit);
                    }
                }
                #endregion
                var result = await _ornamentService.EditOrnament(edit);
                if (result == null) return NotFound();
                TempData["InfoMessage"] = result;
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await _ornamentService.GetCategories();
            return View(edit);
        }
        [HttpGet("admin/Ornament/DeleteOrnament")]

        public async Task<IActionResult> DeleteOrnament(long id)
        {
            var result = await _ornamentService.DeleteOrnament(id);
            if (result == null) return NotFound();
            TempData["InfoMessage"] = result;
            return RedirectToAction(nameof(Index));
        }
    }
    }
