using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Models.Users
{
	public class UserCredentials
	{
		[Required]
		[EmailAddress(ErrorMessage = "Не валидный Email.")]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
