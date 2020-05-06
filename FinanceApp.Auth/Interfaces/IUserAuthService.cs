using System;
using System.Threading.Tasks;
using FinanceApp.Models;

namespace FinanceApp.Auth.Interfaces
{
	public interface IAuthService
	{
		bool IsValidUser(string email, string password);
	}
}
