using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceCoreApp.Services
{
	public static class ServiceProviderExtensions
	{
		public static void AddUserInfoService(this IServiceCollection services)
		{
			services.AddSingleton<UserInfoService>();
		}
	}
}
