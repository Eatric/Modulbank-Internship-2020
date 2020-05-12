using FinanceApp.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinanceApp.Core
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSwagger();

			services.AddLogging();

			services.ConfigureAuthOptions(Configuration);
			services.ConfigureDatabaseOptions(Configuration);

			services.AddUsersRepository();
			services.AddUsersRequestHandler();

			services.AddAccountsRepository();
			services.AddPaymentsRepository();
			services.AddAccountsRequestHandler();

			services.AddAuthService();
			services.AddJwtBearerTokenAuthentication(Configuration);

			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank Api Gateway v1");
				options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
			});

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
