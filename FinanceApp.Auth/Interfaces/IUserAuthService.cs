using System;
using System.Threading.Tasks;
using FinanceApp.Auth;

namespace FinanceApp.Core.Services.Interfaces
{
	public interface IUserAuthService
	{
		Task<User> GetById(Guid id);
		Task<User> GetByEmail(string email);
		bool IsValidUser(string email, string password);
	}
}
