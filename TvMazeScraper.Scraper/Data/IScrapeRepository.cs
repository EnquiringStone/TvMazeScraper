using TvMazeScraper.Common.Data.Entities;

namespace TvMazeScraper.Scraper.Data
{
    public interface IScrapeRepository
    {
        /// <summary>
        ///     Saves the specified data of the scrape.
        /// </summary>
        /// <param name="scrape">The data to save.</param>
        void Save(Scrape scrape);

        /// <summary>
        ///     Saves the changes into the database.
        /// </summary>
        void SaveChanges();

        /// <summary>
        ///     Gets the scrape information for the specified name.
        /// </summary>
        /// <param name="name">The name of the scrape data.</param>
        /// <returns>The scrape data or null.</returns>
        Scrape Get(string name);
    }
}
