using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using MyVinted.API.AppConfigs;
using MyVinted.Core.Application.Logging;
using MyVinted.Core.Application.Mapper;
using MyVinted.Core.Application.SignalR;
using MyVinted.Core.Application.SignalR.Hubs;
using MyVinted.Core.Common.Helpers;
using Stripe;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace MyVinted.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.ConfigureIdentity();

            services.ConfigureAuthentication(Configuration)
                .ConfigureGoogleAuthentication(Configuration)
                .ConfigureFacebookAuthentication(Configuration);
            services.ConfigureAuthorization();

            services.ConfigureMvc()
                .ConfigureFluentValidation();

            services.ConfigureIpRateLimit(Configuration);

            services.ConfigureDbContext(Configuration);

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            services.AddOptions();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            services.ConfigureCors();

            services.AddMediatR(Assembly.Load("MyVinted.Core.Application"));

            #region services

            services.ConfigureScopedServices();
            services.ConfigureSingletonServices();

            #endregion

            services.ConfigureSettings(Configuration);

            services.AddAutoMapper(typeof(MapperProfile));

            services.AddDataProtection();

            services.AddDirectoryBrowser();

            services.ConfigureServerHostedServices();

            services.ConfigureSwagger();

            services.AddSignalR();

            services.AddSingleton<HubNamesDictionary>(s => HubNamesDictionary.Build());
            services.AddSingleton<LogKeyWordsDictionary>(s => LogKeyWordsDictionary.Build());

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyVinted.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(Constants.CorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.ConfigureLogging();

            StripeConfiguration.ApiKey =
                Configuration.GetSection(AppSettingsKeys.StripeSection)[AppSettingsKeys.SecretKey];

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotifierHub>("/api/hub/notifier");
                endpoints.MapHub<MessengerHub>("/api/hub/messenger");
            });

            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, @"files")),
                RequestPath = new PathString("/files"),
                EnableDirectoryBrowsing = true
            });

            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            StorageLocation.Init(Configuration.GetValue<string>(AppSettingsKeys.ServerAddress));
        }
    }
}