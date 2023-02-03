using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ornaments.Core.Dtos.Auth;
using Ornaments.Core.Services.Interfaces;
using Ornaments.DataAccess.Entities.User;

namespace Ornaments.Web.Controllers
{
	public class AuthController : Controller
	{

		#region ctor
		private readonly IMessageSender _messageSender;

		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IMessageSender messageSender)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_messageSender = messageSender;
		}

		#endregion
		#region Login
		[HttpGet("login")]
		public IActionResult Login(string returnUrl = null)
		{
			if (_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Home");
			ViewBag.ReturnUrl = returnUrl;
			return View(new LoginUserDto { ReturnUrl = returnUrl });
		}
		[HttpPost("login"), ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginUserDto login)
		{
			ViewBag.ReturnUrl = login.ReturnUrl;
			if (_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Home");
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, true, true);
				if (result.Succeeded)
				{
					TempData["SuccessMessage"] = "You are logged in";
					if (!string.IsNullOrEmpty(login.ReturnUrl) && Url.IsLocalUrl(login.ReturnUrl))
						return Redirect(login.ReturnUrl);
					return RedirectToAction("Index", "Home");
				}
				if (result.IsLockedOut)
				{
					TempData["ErrorMessage"] = "Your account has been locked due to 5 failed logins - 15 Minutes";
					return View(login);
				}
				ModelState.AddModelError("", "Informaions are wrong");
			}
			return View(login);
		}
		#endregion
		#region Signup
		[HttpGet("Signup")]
		public IActionResult Signup()
		{
			if (_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Home");
			return View();
		}
		[HttpPost("Signup"), ValidateAntiForgeryToken]
		public async Task<IActionResult> Signup(SignupUserDto signup)
		{
			if (_signInManager.IsSignedIn(User)) return RedirectToAction("Index", "Home");
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser()
				{
					Email = signup.Email,
					UserName = signup.UserName,
					EmailConfirmed = true
				};
				var result = await _userManager.CreateAsync(user, signup.Password);
				if (result.Succeeded)
				{
					var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var emailMessage = Url.Action("ConfirmEmail", "Account", new
					{
						userName = user.UserName,
						token = emailConfirmationToken
					}, Request.Scheme);
					//await _messageSender.SendEmailAsync(user.Email, "Email Confirmation", emailMessage);
					TempData["InfoMessage"] = "Activation email sent";
					return RedirectToAction("Login", "Auth");
				}
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
			}
			return View(signup);
		}

		#endregion
		#region ConfirmEmail
		public async Task<IActionResult> ConfirmEmail(string userName, string token)
		{
			if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(token))
				return NotFound();
			var user = await _userManager.FindByNameAsync(userName);
			if (user == null) return NotFound();
			var result = await _userManager.ConfirmEmailAsync(user, token);
			if (result.Succeeded) TempData["SuccessMessage"] = "Account activated ";
			else TempData["ErrorMessage"] = "Account not activated ";
			return RedirectToAction("Index", "Home");
		}
		#endregion
		#region Logout
		[HttpGet("Logout")]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Redirect("/");
		}

		#endregion
		#region Forgot Password
		[HttpGet("Forgot-password")]
		public IActionResult ForgotPassword()
		{
			return View();
		}
		[HttpPost("Forgot-password"), ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgot)
		{
			if (ModelState.IsValid)
			{
				TempData["InfoMessage"] = "If the email is valid, the email will be sent to you";
				var user = await _userManager.FindByEmailAsync(forgot.Email);
				if (user == null)
					return RedirectToAction("Login", "Auth");
				var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
				var resetPasswordEmail = Url.Action("ResetPassword"
					, "Auth", new { email = user.Email, token = resetPasswordToken }, Request.Scheme);
				//await _messageSender.SendEmailAsync(user.Email, "Reset Password", resetPasswordEmail);
				//return RedirectToAction("Login", "Account",new{test=resetPasswordEmail });
				return Redirect(resetPasswordEmail);
			}
			return View(forgot);
		}
		#endregion
		#region ResetPassword
		[HttpGet("reset-password")]
		public IActionResult ResetPassword(string email, string token)
		{
			if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
				return RedirectToAction("Index", "Home");

			var reset = new ResetPasswordDto()
			{
				Email = email,
				Token = token
			};

			return View(reset);
		}

		[HttpPost("reset-password"), ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetPassword(ResetPasswordDto reset)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(reset.Email);
				if (user == null) return RedirectToAction("Login", "Account");
				var result = await _userManager.ResetPasswordAsync(user, reset.Token, reset.NewPassword);
				if (result.Succeeded)
				{
					TempData["SuccessMessage"] = "Your password has changed";
					return RedirectToAction("Login", "Account");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}

			return View(reset);
		}

		#endregion
	}
}
