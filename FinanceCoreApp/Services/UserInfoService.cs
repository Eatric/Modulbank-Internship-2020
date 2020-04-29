using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FinanceCoreApp.Models;
using FinanceCoreApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace FinanceCoreApp.Services
{
	public class UserInfoService : IUserInfoService
	{
		private string connectionString;

		public UserInfoService(IConfiguration configuration)
		{
			connectionString = configuration.GetValue<string>("ConnectionStrings:PostgreSQL");
		}

		public async Task<User> GetById(Guid id)
		{
			await using var connection = new NpgsqlConnection(connectionString);
			return await connection.QuerySingleAsync<User>("SELECT * FROM `Users` WHERE `Id` = @id", new {id});
		}

		public async Task<User> GetByEmail(string email)
		{
			await using var connection = new NpgsqlConnection(connectionString);
			return await connection.QuerySingleAsync<User>("SELECT * FROM `Users` WHERE `Email` = @email", new { email });
		}

		public bool IsValidUser(string email, string password)
		{
			User user = GetByEmail(email).Result;
			return Password.CheckPassword(password, user.Salt, user.Password);
		}
	}
}
