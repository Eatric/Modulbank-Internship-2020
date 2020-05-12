using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FinanceApp.Database.Interfaces;
using FinanceApp.Models.Accounts;
using FinanceApp.Models.Users;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FinanceApp.Database
{
	public class PaymentsRepository : IPaymentsRepository
	{
		private readonly DatabaseOptions _databaseOptions;

		public PaymentsRepository(IOptions<DatabaseOptions> databaseOptions)
		{
			_databaseOptions = databaseOptions.Value;
		}

		public async Task<bool> Create(Payment item)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			return await connection.ExecuteAsync("INSERT INTO \"Transactions\" VALUES (nextval('\"TransactionsAutoIncrement\"'::regclass), @FromAccount, @ToAccount, @Amount, @When, @Type)", new
			{
				item.FromAccount,
				item.ToAccount,
				item.Amount,
				item.When,
				Type = (short) item.Type
			}) > 0;
		}

		public async Task<Payment> Read(string item)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Update(Payment item)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Payment>> GetPaymentsTo(string to)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Payment>> GetPaymentsFrom(string @from)
		{
			throw new NotImplementedException();
		}
	}
}
