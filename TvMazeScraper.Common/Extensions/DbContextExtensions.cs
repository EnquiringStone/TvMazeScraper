using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TvMazeScraper.Common.Data;

namespace TvMazeScraper.Common.Extensions
{
    public static class DbContextExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Unable to create connection string. Database configuration connection string not found.");
            }

            var databasePassword = configuration.GetValue<string>("database_password");

            if (string.IsNullOrEmpty(databasePassword))
            {
                throw new InvalidOperationException("Unable to create connection string. Password is empty.");
            }

            connectionString = connectionString.Replace("{{database_password}}", databasePassword);

            services.AddDbContext<TvMazeDbContext>(cfg =>
            {
                cfg.UseNpgsql(connectionString, opt => opt.MigrationsAssembly("TvMazeScraper.Scraper"));
            });
        }
    }
}
