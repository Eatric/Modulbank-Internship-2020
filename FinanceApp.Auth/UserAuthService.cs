using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FinanceApp.Auth;
using FinanceApp.Core.Configurations;
using FinanceApp.Core.Services.Interfaces;
using Npgsql;

namespace FinanceApp.Core.Services
{
	public class UserAuthService : IUserAuthService
	{
		private DatabaseOptions _databaseOptions;

		public UserAuthService(IOptions<DatabaseOptions> databaseOptions)
		{
			_databaseOptions = databaseOptions.Value;
		}

		public async Task<User> GetById(Guid id)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			return await connection.QuerySingleAsync<User>("SELECT * FROM \"Users\" WHERE \"Id\" = @id", new {id});	
		}

		public async Task<User> GetByEmail(string email)
		{
			await using var connection = new NpgsqlConnection(_databaseOptions.ToString());
			return await connection.QuerySingleAsync<User>("SELECT * FROM \"Users\" WHERE \"Email\" = @email", new { email });
		}

		public bool IsValidUser(string email, string password)
		{
			var user = GetByEmail(email).Result;
			return Password.CheckPassword(password, user.Salt, user.Password);
		}
	}
}
