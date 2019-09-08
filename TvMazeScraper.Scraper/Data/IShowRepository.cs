using TvMazeScraper.Common.Data.Entities;

namespace TvMazeScraper.Scraper.Data
{
    public interface IShowRepository
    {
        /// <summary>
        ///     Saves the specified show.
        /// </summary>
        /// <param name="shows">The show to save.</param>
        void Save(Show show);

        /// <summary>
        ///     Saves the changes into the database.
        /// </summary>
        void SaveChanges();
    }
}
