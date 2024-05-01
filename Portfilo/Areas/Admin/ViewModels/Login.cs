using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portfilo.Areas.Admin.ViewModels
{
	public class Login
	{
		[Required(ErrorMessage = "Please enter an email!")]
		[DisplayName("Email")]
		[DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email!")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please enter password!")]
		[DisplayName("Password")]
		[DataType(DataType.Password, ErrorMessage = "Please enter a valid email!")]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
