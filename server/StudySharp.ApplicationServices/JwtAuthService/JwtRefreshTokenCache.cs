using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace StudySharp.ApplicationServices.JwtAuthService
{
    public class JwtRefreshTokenCache : IHostedService, IDisposable
    {
        private readonly IJwtService _jwtService;
        private Timer _timer;

        public JwtRefreshTokenCache(IJwtService jwtAuthManager)
        {
            _jwtService = jwtAuthManager;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            // remove expired refresh tokens from cache every minute
#pragma warning disable CS8622
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
#pragma warning restore CS8622
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _jwtService.RemoveExpiredRefreshTokens(DateTime.Now);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
