using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portfilo.Areas.Admin.ViewModels
{
	public class Register
	{
		[Required(ErrorMessage = "Please enter an email!")]
		[DisplayName("Email")]
		[DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email!")]
        public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter an password!")]
		[DisplayName("Password")]
		[DataType(DataType.Password, ErrorMessage = "Please enter a valid email!")]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter an password confirmation!")]
		[DisplayName("Confirm Password")]
		[DataType(DataType.Password, ErrorMessage = "Please enter a valid email!")]
		[Compare("Password",ErrorMessage = "Password not matched!")]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
