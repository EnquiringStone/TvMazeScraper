using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TvMazeScraper.ApiClient.Data;
using TvMazeScraper.Common;

namespace TvMazeScraper.ApiClient
{
    public class Startup : StartupCore
    {
        public Startup(IConfiguration configuration) : base(configuration) { }

        protected override void OnServiceConfiguration(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<IShowRepository, ShowRepository>();
        }
    }
}
