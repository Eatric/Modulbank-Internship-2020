using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceCoreApp.Models
{
	public class User
	{
		public Guid Id { get; }
		public string Name { get; }
		public string Email { get; }
		
		private Password _password;

		public string Password => _password.PasswordHash;

		public string Salt => _password.Salt;

		public User(string name, string email, string password)
		{
			Id = Guid.NewGuid();
			Name = name;
			Email = email;
			_password = new Password(password);
		}
	}
}
