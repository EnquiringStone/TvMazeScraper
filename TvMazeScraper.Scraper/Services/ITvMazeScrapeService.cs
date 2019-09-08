using System.Threading.Tasks;

namespace TvMazeScraper.Scraper.Services
{
    public interface ITvMazeScrapeService
    {
        /// <summary>
        ///     Scrapes the TvMaze public api
        /// </summary>
        Task ScrapeAsync();
    }
}
