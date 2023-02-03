 
using System.ComponentModel.DataAnnotations;
 

namespace Ornaments.Core.Dtos.Auth
{
	public class ForgotPasswordDto
	{
		[EmailAddress]
		[Required]
		[MaxLength(200)]
		public string Email { get; set; }
	}
}
