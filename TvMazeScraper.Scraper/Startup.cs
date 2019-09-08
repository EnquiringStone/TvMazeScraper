using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using TvMazeScraper.Common;
using TvMazeScraper.Common.ConfigurationSections;
using TvMazeScraper.Scraper.Data;
using TvMazeScraper.Scraper.HostedServices;
using TvMazeScraper.Scraper.Services;

namespace TvMazeScraper.Scraper
{
    public class Startup : StartupCore
    {
        public Startup(IConfiguration configuration) : base(configuration) { }

        protected override void OnServiceConfiguration(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var tvMazeApiSettings = Configuration.GetSection(TvMazeApiSettings.SectionName).Get<TvMazeApiSettings>();

            services
                .AddHttpClient(TvMazeApiSettings.SectionName, client =>
                {
                    client.BaseAddress = new Uri(tvMazeApiSettings.BaseUrl);
                    client.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
                })
                .AddPolicyHandler(GetRetryPolicy());

            services.AddTransient<ICastMemberRepository, CastMemberRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IScrapeRepository, ScrapeRepository>();
            services.AddTransient<IShowRepository, ShowRepository>();
            services.AddTransient<ITvMazeScrapeService, TvMazeScrapeService>();

            services.AddHostedService<HostedTvMazeScrapeService>();
        }

        private IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(20));
        }
    }
}
