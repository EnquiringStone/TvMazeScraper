using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Debugging;
using System;
using System.IO;

namespace TvMazeScraper.Common
{
    public static class Service
    {
        public static string GetEnvironment() => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        public static IWebHostBuilder ConfigureWebHost<TStartupClass>(string serviceName)
            where TStartupClass : class
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseEnvironment(GetEnvironment())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var environment = hostingContext.HostingEnvironment;
                    config.SetBasePath(environment.ContentRootPath);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    Log.Logger = new LoggerConfiguration()
                     .ReadFrom.Configuration(hostingContext.Configuration)
                     .Enrich.FromLogContext()
                     .WriteTo.Console()
                     .CreateLogger();

                    Log.Logger.Information(
                        $"Starting up service {serviceName} on environment {GetEnvironment()}");

                    SelfLog.Enable(msg => { Console.WriteLine($"SELF LOG: {msg}{Environment.NewLine}"); });
                    logging.AddSerilog();
                })
                .UseStartup<TStartupClass>();
        }
    }
}
