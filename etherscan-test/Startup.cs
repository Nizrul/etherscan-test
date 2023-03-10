using System;
using etherscan_test.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace etherscan_test
{
	public class Startup
	{
		public IConfigurationRoot Configuration { get; }
		public Startup()
        {
			var builder = new ConfigurationBuilder().AddUserSecrets("b2db93b0-dfcd-4e49-8158-1f80e8064ba7");

            Configuration = builder.Build();
        }

		public void ConfigurationServices(IServiceCollection services)
		{
			Console.WriteLine("This is from startup");

            services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(Configuration.GetConnectionString("Default")));
            services.AddSingleton<IConfigurationRoot>(Configuration);
            services.AddSingleton<IndexService>();
		}
	}
}

