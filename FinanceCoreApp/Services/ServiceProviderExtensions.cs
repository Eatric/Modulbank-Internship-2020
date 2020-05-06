using System;
using System.Text;
using FinanceApp.Auth;
using FinanceApp.Auth.Interfaces;
using FinanceApp.BusinessLogic.Accounts;
using FinanceApp.BusinessLogic.Users;
using FinanceApp.Database;
using FinanceApp.Database.Interfaces;
using FinanceApp.Models;
using FinanceApp.Models.Accounts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FinanceApp.Core.Services
{
	public static class ServiceProviderExtensions
	{
		#region Database
		public static void ConfigureDatabaseOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<DatabaseOptions>(configuration.GetSection("ConnectionStrings"));
		}
		#endregion

		#region Users
		public static void AddUsersRepository(this IServiceCollection services)
		{
			services.AddScoped<IUsersRepository, UsersRepository>();
		}

		public static void AddUsersRequestHandler(this IServiceCollection services)
		{
			services.AddScoped<UsersRequestHandler>();
		}
		#endregion

		#region Accounts
		public static void AddAccountsRequestHandler(this IServiceCollection services)
		{
			services.AddScoped<AccountsRequestHandler>();
		}

		public static void AddAccountsRepository(this IServiceCollection services)
		{
			services.AddScoped<IAccountsRepository, AccountsRepository>();
		}
		#endregion

		#region Auth
		public static void AddAuthService(this IServiceCollection services)
		{
			services.AddScoped<IAuthService, AuthService>();
		}

		public static void ConfigureAuthOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<AuthOptions>(configuration.GetSection("AuthOptions"));
		}

		public static void AddJwtBearerTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					RequireExpirationTime = true,
					ValidIssuer = authOptions.Issuer,
					ValidAudience = authOptions.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SecureKey))
				};
			});
		}
		#endregion
	}
}
