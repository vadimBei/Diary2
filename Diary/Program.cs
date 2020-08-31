using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Entities.Models;
using Diary.Repository;
using Diary.Services;
using Diary.Services.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Diary
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = BuildWebHost(args);

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var rolesManager = services.GetRequiredService<RoleManager<AppRole>>();
					var userManager = services.GetRequiredService<UserManager<User>>();
					var aesCrypto = services.GetRequiredService<IAesCryptoProviderService>();
					byte[] cryptokey = aesCrypto.GenerateKey();

					DbInitializer dbInitializer = new DbInitializer();
					await dbInitializer.InitializeAsync(rolesManager, userManager, cryptokey);
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred while seeding the database.");
				}
			}

			host.Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
			.UseStartup<Startup>()
			.ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Trace))
			.Build();
	}
}
