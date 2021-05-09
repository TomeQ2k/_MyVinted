using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyVinted.API.BackgroundServices
{
    internal abstract class ServerHostedService : IHostedService, IDisposable
    {
        protected readonly IServiceProvider services;

        public int TimeInterval { get; protected set; }

        private Timer timer;

        public ServerHostedService(IServiceProvider services)
        {
            this.services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Log.Information($"{this.GetType().Name}: Background server hosted service started...");

            timer = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromMinutes(TimeInterval));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Log.Information($"{this.GetType().Name}: Background server hosted service stopped...");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public virtual void Callback(object state)
        {
            Log.Information($"{this.GetType().Name}: Background server hosted service invoked");
        }
    }
}