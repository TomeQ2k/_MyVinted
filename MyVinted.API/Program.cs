using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyVinted.API.BackgroundServices.Interfaces;
using MyVinted.Core.Common.Helpers;
using MyVinted.Infrastructure.Persistence.Database;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Threading.Tasks;

namespace MyVinted.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(new CompactJsonFormatter())
                .WriteTo.File(new CompactJsonFormatter(), Constants.LogFilesPath, rollingInterval: RollingInterval.Day)
                .WriteTo.Seq("http://localhost:5000")
                .CreateLogger();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    Log.Information("Application started...");

                    var context = services.GetRequiredService<DataContext>();
                    var databaseManager = services.GetRequiredService<IDatabaseManager>();

                    context.Database.Migrate();

                    await databaseManager.Seed();
                    Log.Information("Database seed completed");

                    host.Run();
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "Application terminated unexpectedly...");
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
