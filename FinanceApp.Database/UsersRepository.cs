using System;
using System.Threading.Tasks;
using Dapper;
using FinanceApp.Database.Interfaces;
using FinanceApp.Models.Users;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FinanceApp.Database
{
	public class UsersRepository : IUsersRepository
	{
		private readonly DatabaseOptions _databaseOptions;

		public UsersRepository(IOptions<DatabaseOptions> databaseOptions)
		{
			_databaseOptions = databaseOptions.Value;
		}

		public async Task<User> Read(Guid id)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM \"Users\" WHERE \"Id\" = @id", new {id});	
		}

		public async Task<User> Read(string email)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM \"Users\" WHERE \"Email\" = @email", new { email });
		}

		public async Task<bool> Create(User item)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			return await connection.ExecuteAsync("INSERT INTO \"Users\" VALUES (@Id, @Name, @Email, @Password, @Salt, @Photo, @ItemStatus)", new
			{
				item.Id,
				item.Name,
				item.Email,
				item.Password,
				item.Salt,
				item.Photo,
				ItemStatus = (short)item.Status
			}) > 0;
		}

		public async Task<bool> Update(User item)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			return await connection.ExecuteAsync("UPDATE \"Users\" SET \"Name\" = @Name, \"Email\" = @Email, \"Photo\" = @Photo, \"Status\" = @Status, \"Password\" = @Password, \"Salt\" = @Salt WHERE \"Id\" = @Id", item) > 0;
		}

		public async Task<bool> Delete(Guid id)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			return await connection.ExecuteAsync("DELETE FROM \"Users\" WHERE \"Id\"= @id",  new { id }) > 0;
		}
	}
}
