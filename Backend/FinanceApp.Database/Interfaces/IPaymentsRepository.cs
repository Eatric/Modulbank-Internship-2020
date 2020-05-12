using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApp.Models.Accounts;

namespace FinanceApp.Database.Interfaces
{
	public interface IPaymentsRepository : IRepository<Payment>
	{
		Task<IEnumerable<Payment>> GetPaymentsTo(string to);
		Task<IEnumerable<Payment>> GetPaymentsFrom(string from);
	}
}
