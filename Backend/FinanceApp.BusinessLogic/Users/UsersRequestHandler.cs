using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FinanceApp.Database.Interfaces;
using FinanceApp.Models;
using FinanceApp.Models.Users;

namespace FinanceApp.BusinessLogic.Users
{
	public class UsersRequestHandler
	{
		private readonly IUsersRepository _modelRepository;

		public UsersRequestHandler(IUsersRepository modelRepository)
		{
			_modelRepository = modelRepository;
		}

		public async Task<User> GetUser(Guid id)
		{
			if (id == Guid.Empty)
			{
				throw new ArgumentException("Некорректный идентификатор пользователя", nameof(id));
			}

			return await _modelRepository.Read(id);
		}

		public async Task<bool> Register(UserRegisterCredentials registerCredentials)
		{
			var user = new User(registerCredentials.Name, registerCredentials.Email, registerCredentials.Password);

			return await _modelRepository.Create(user);
		}
	}
}
