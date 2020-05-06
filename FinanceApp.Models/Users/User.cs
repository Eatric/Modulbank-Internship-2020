using System;
using FinanceApp.Models.Users;

namespace FinanceApp.Models.Users
{
	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Photo { get; set; }
		public string Password { get; set; }
		public string Salt { get; set; }
		public EUserStatus Status { get; set; }

		public User(string name, string email, string password)
		{
			Id = Guid.NewGuid();
			Name = name;
			Email = email;
			var pass = new Password(password);
			Password = pass.PasswordHash;
			Salt = pass.Salt;
			Photo = string.Empty;
			Status = EUserStatus.Moderate;
		}

		public User(Guid Id, string Name, string Email, string Password, string Salt, string Photo, short Status)
		{
			this.Id = Id;
			this.Name = Name;
			this.Email = Email;
			this.Password = Password;
			this.Salt = Salt;
			this.Photo = Photo;
			this.Status = (EUserStatus)Status;
		}
	}
}
