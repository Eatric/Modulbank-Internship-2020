using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
using Microsoft.OpenApi.Models;

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

		public static void AddPaymentsRepository(this IServiceCollection services)
		{
			services.AddScoped<IPaymentsRepository, PaymentsRepository>();
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

		#region Swagger

		public static void AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Bank Api Gateway",
					Description = "Maded for ModulBank Internship",
					Contact = new OpenApiContact
					{
						Name = "Iksanov Kamil",
						Email = string.Empty,
						Url = new Uri("https://t.me/Eatric")
					},
				});

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization",
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] { }
					}
				});
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				options.IncludeXmlComments(xmlPath);
			});
		}

		#endregion
	}
}
