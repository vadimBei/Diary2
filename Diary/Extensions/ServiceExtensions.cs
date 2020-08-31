using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diary.Services;
using Diary.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Diary.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureServices(this IServiceCollection services)
		{
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<IEmailService, EmailService>();
			services.AddScoped<IAesCryptoProviderService, AesCryptoProviderService>();
			services.AddScoped<IRecordService, RecordService>();
			services.AddScoped<IInviteService, InviteService>();
			services.AddScoped<ICaptchaService, CaptchaService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IUploadedFileService, UploadedFileService>();
			services.AddScoped<ISearchService, SearchService>();
		}

	}
}
