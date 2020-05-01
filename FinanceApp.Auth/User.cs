using System;

namespace FinanceApp.Auth
{
	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Photo { get; set; }
		public bool IsModerated { get; set; }
		public string Password { get; set; }
		public string Salt { get; set; }
		public DateTime CreateDate { get; set; }

		public User(string name, string email, string password)
		{
			Id = Guid.NewGuid();
			Name = name;
			Email = email;
			var pass = new Password(password);
			Password = pass.PasswordHash;
			Salt = pass.Salt;
			Photo = string.Empty;
			IsModerated = false;
		}

		public User(Guid Id, string Name, string Email, string Password, string Salt, string Photo, bool IsModerated,
			DateTime CreateDate)
		{
			this.Id = Id;
			this.Name = Name;
			this.Email = Email;
			this.Password = Password;
			this.Salt = Salt;
			this.Photo = Photo;
			this.IsModerated = IsModerated;
			this.CreateDate = CreateDate;
		}
	}
}
