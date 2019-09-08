using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TvMazeScraper.Scraper.Services;

namespace TvMazeScraper.Scraper.HostedServices
{
    public class HostedTvMazeScrapeService : IHostedService, IDisposable
    {
        private readonly ILogger<HostedTvMazeScrapeService> logger;
        private readonly ITvMazeScrapeService tvMazeScrapeService;
        private Timer timer;

        public HostedTvMazeScrapeService(
            ILogger<HostedTvMazeScrapeService> logger,
            IServiceProvider serviceProvider)
        {
            this.logger = logger;
            tvMazeScrapeService = (ITvMazeScrapeService)serviceProvider.GetService(typeof(ITvMazeScrapeService));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Scraper service has started.");

            timer = new Timer(DoScrape, null, TimeSpan.Zero, TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Scraper service has stopped.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        private void DoScrape(object state)
        {
            tvMazeScrapeService.ScrapeAsync();
        }
    }
}
