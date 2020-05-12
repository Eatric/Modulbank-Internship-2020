using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using FinanceApp.Database.Interfaces;
using FinanceApp.Models.Accounts;
using FinanceApp.Models.Users;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FinanceApp.Database
{
	public class AccountsRepository : IAccountsRepository
	{
		private readonly DatabaseOptions _databaseOptions;

		public AccountsRepository(IOptions<DatabaseOptions> databaseOptions)
		{
			_databaseOptions = databaseOptions.Value;
		}

		public async Task<IEnumerable<Account>> Read(Guid id)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			var queryResult = await connection.QueryMultipleAsync("SELECT * FROM \"Accounts\" WHERE \"Owner\" = @id", new {id});
			var result = queryResult.Read<Account>();
			return result;	
		}

		public async Task<Account> Read(string number)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			return await connection.QueryFirstOrDefaultAsync<Account>("SELECT * FROM \"Accounts\" WHERE \"Number\" = @number", new { number });
		}

		public async Task<bool> Create(Account item)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			return await connection.ExecuteAsync("INSERT INTO \"Accounts\" VALUES (@Number, @Balance, @Status, @Owner)", item) > 0;
		}

		public async Task<bool> Update(Account item)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			return await connection.ExecuteAsync("UPDATE \"Accounts\" SET \"Balance\" = @Balance, \"Status\" = @Status, \"Owner\" = @Owner WHERE \"Number\" = @Number", item) > 0;
		}

		public async Task<bool> Delete(Guid id)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			return await connection.ExecuteAsync("DELETE FROM \"Accounts\" WHERE \"Number\" = @Number)", new { id }) > 0;
		}
	}
}
