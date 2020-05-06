using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Models;
using FinanceApp.Models.Accounts;

namespace FinanceApp.Database.Interfaces
{
	public interface IAccountsRepository : IRepository<Account>
	{
		Task<IEnumerable<Account>> Read(Guid id);
	}
}
