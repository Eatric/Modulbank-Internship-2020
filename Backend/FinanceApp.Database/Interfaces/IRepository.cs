using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApp.Database.Interfaces
{
	public interface IRepository<T> where T: class
	{
		Task<bool> Create(T item);
		Task<T> Read(string item);
		Task<bool> Update(T item);
		Task<bool> Delete(Guid id);
	}
}
