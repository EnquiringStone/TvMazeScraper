using Microsoft.AspNetCore.Hosting;

namespace TvMazeScraper.Common.Extensions
{
    public static class HostingEnvironmentExtensions
    {
        public static bool IsProductionEnvironment(this IHostingEnvironment hostingEnvironment)
        {
            return hostingEnvironment.IsProduction() || hostingEnvironment.IsEnvironment("P");
        }
    }
}
