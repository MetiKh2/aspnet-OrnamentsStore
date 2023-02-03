 
using System.ComponentModel.DataAnnotations;
 

namespace Ornaments.Core.Dtos.Auth
{
	public class SignupUserDto
	{
		[Display(Name = "UserName")]
		[MaxLength(200)]
		[Required]
		public string UserName { get; set; }
		[Display(Name = "Email")]
		[MaxLength(200)]
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Display(Name = "Password")]
		[Required]
		public string Password { get; set; }
		[Display(Name = "RePassword")]
		[Required]
		[Compare("Password")]
		public string RePassword { get; set; }
	}
}
