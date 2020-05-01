using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceApp.Auth
{
	public class UserRegisterCredentials : UserCredentials
	{
		public string RepeatedPassword { get; set; }
	}
}
