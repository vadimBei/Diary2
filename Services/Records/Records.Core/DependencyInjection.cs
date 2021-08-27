using Common.System.Interfaces;
using Common.System.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Records.Core.Application.Common.Interfaces;
using Records.Core.Application.Common.Services;
using Records.Core.Infrastructure;
using System.Reflection;

namespace Records.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlServer(configuration["ConnectionString:RecordsDb"]));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddTransient<IAesCryptoProviderService, AesCryptoProviderService>();
            services.AddTransient<IRecordService, RecordService>();

            return services;
        }
    }
}
