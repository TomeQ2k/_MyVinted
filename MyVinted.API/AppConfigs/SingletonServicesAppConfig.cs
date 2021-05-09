using Microsoft.Extensions.DependencyInjection;
using MyVinted.Core.Application.Logging;
using MyVinted.Core.Application.Services;
using MyVinted.Core.Application.Services.ReadOnly;
using MyVinted.Infrastructure.Persistence.Logging;
using MyVinted.Infrastructure.Shared.Services;

namespace MyVinted.API.AppConfigs
{
    public static class SingletonServicesAppConfig
    {
        public static IServiceCollection ConfigureSingletonServices(this IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IFilesManager, FilesManager>();
            services.AddSingleton<ICryptoService, CryptoService>();
            services.AddSingleton<ILogReader, LogReader>();
            services.AddSingleton<IHttpContextService, HttpContextService>();
            services.AddSingleton<IHttpContextWriter, HttpContextService>();
            services.AddSingleton<IHttpContextReader, HttpContextService>();

            services.AddSingleton<IReadOnlyFilesManager, FilesManager>();

            return services;
        }
    }
}