using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Diary.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Diary.Repository
{
	public class DbInitializer
	{
		public const string AdminEmail = "travelblog1.no.reply@gmail.com";

		public const string AdminPassword = "TravelBlog@111";

		public const string AdminName = "Admin";

		public async Task InitializeAsync(RoleManager<AppRole> roleManager, UserManager<User> userManager, byte[] cryptokey)
		{
			if (await roleManager.FindByNameAsync("Admin") == null)
			{
				await roleManager.CreateAsync(new AppRole() { Name = "Admin" });
			}
			if (await roleManager.FindByNameAsync("User") == null)
			{
				await roleManager.CreateAsync(new AppRole() { Name = "User" });
			}

			var admin = new User()
			{
				Email = AdminEmail,
				UserName = AdminName,
				CryptoKey = cryptokey
			};

			IdentityResult result = await userManager.CreateAsync(admin, AdminPassword);

			if (result.Succeeded)
				await userManager.AddToRoleAsync(admin, "Admin");
		}
	}
}
