using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Common.Helpers;
using MyVinted.Infrastructure.Persistence.Database;

namespace MyVinted.API.AppConfigs
{
    public static class DbContextAppConfig
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(AppSettingsKeys.ConnectionString), b => b.MigrationsAssembly("MyVinted.API"));
                options.UseLazyLoadingProxies();
            });
    }
}