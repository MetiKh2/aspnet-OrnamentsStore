

using System.ComponentModel.DataAnnotations;

namespace Ornaments.Core.Dtos.Auth
{
	public class LoginUserDto
	{
		[Display(Name = "UserName")]
		[MaxLength(200)]
		[Required]
		public string UserName { get; set; }
		[Display(Name = "Password")]
		[Required]
		public string Password { get; set; }
		public string? ReturnUrl { get; set; }
	}
}
