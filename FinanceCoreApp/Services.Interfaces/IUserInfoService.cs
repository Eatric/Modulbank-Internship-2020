using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceCoreApp.Models;

namespace FinanceCoreApp.Services.Interfaces
{
	public interface IUserInfoService
	{
		Task<User> GetById(Guid id);
		Task<User> GetByEmail(string email);
		bool IsValidUser(string email, string password);
	}
}
