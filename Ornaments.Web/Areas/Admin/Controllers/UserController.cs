using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ornaments.Core.Dtos.Admin.Users;
using Ornaments.Core.Dtos.Admin.Users.Create;
using Ornaments.Core.Dtos.Admin.Users.Edit;
using Ornaments.Core.Services.Interfaces;

namespace Ornaments.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Admin/users")]
        public async Task<IActionResult> Index(FilterUsersDto filter)
        {
            filter.TakeEntity = 5;
            return View(await _userService.FilterUsersList(filter));
        }
        [HttpGet("Admin/CreateUser")]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }
        [HttpPost("Admin/CreateUser"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserDto create, List<string> selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateUser(create);
                    TempData["InfoMessage"] =result;
                    return RedirectToAction(nameof(Index));
            }
            return View(create);
        }
        [HttpGet("Admin/EditUser/{userName}")]
        public async Task<IActionResult> EditUser(string userName)
        {
            
            var user = await _userService.GetByName(userName);
            if (user == null)
                return NotFound();
           
            return View(new EditUserDto { Email = user.Email, UserName = user.UserName, Id = user.Id });
        }
        [HttpPost("Admin/EditUser/{userName}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserDto edit, List<string> selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.EditUser(edit);
                if (result == null) return NotFound();
               
                TempData["InfoMessage"] = result;
                return RedirectToAction(nameof(Index));
            }
            return View(edit);
        }
        [HttpGet("Admin/DeleteUser/{userName}")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            var result = await _userService.DeleteUser(userName);
            if (result == null) return NotFound();
             TempData["WarningMessage"] = result;
            return RedirectToAction(nameof(Index));
        }
    }
}
