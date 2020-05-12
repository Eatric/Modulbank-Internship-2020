using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FinanceApp.Auth.Interfaces;
using FinanceApp.Models;
using FinanceApp.Database;
using FinanceApp.Database.Interfaces;
using FinanceApp.Models.Users;
using Npgsql;

namespace FinanceApp.Auth
{
	public class AuthService : IAuthService
	{
		private readonly IUsersRepository _modelRepository;

		public AuthService(IUsersRepository modelRepository)
		{
			_modelRepository = modelRepository;
		}

		public bool IsValidUser(string email, string password)
		{
			var user = _modelRepository.Read(email).Result;

			return Password.CheckPassword(password, user.Salt, user.Password);
		}

		public Guid GetGuidOfUser(string email)
		{
			var user = _modelRepository.Read(email).Result;

			return user.Id;
		}
	}
}
