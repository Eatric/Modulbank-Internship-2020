using System;
using System.Threading.Tasks;
using FinanceApp.Models.Users;

namespace FinanceApp.Database.Interfaces
{
	public interface IUsersRepository : IRepository<User>
	{
		Task<User> Read(Guid id);
	}
}
