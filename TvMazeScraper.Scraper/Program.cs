using Microsoft.AspNetCore.Hosting;
using TvMazeScraper.Common;

namespace TvMazeScraper.Scraper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            Service.ConfigureWebHost<Startup>(typeof(Program).Namespace);
    }
}
