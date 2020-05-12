using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FinanceApp.Models.Users
{
	public class UserRegisterCredentials : UserCredentials
	{
		[Required(ErrorMessage = "Необходимо ввести имя.")]
		[StringLength(65,  ErrorMessage = "Имя должно быть от 3 до 65 символов.", MinimumLength = 3)]
		public string Name { get; set; }

		[Required]
		public string RepeatedPassword { get; set; }
	}
}
