using System.ComponentModel.DataAnnotations;
namespace Ornaments.Core.Dtos.Auth
{
	public class ResetPasswordDto
	{
		[Required]
		[Display(Name = "New password")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required]
		[Display(Name = "Confrim new password")]
		[Compare(nameof(NewPassword))]
		[DataType(DataType.Password)]
		public string ConfirmNewPassword { get; set; }

		[Required]
		public string Token { get; set; }

		[Required]
		public string Email { get; set; }
	}
}
