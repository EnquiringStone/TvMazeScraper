using TvMazeScraper.Common.Data.Entities;

namespace TvMazeScraper.Scraper.Data
{
    public interface IPersonRepository
    {
        /// <summary>
        ///     Saves the specified person.
        /// </summary>
        /// <param name="person">The person to save.</param>
        void Save(Person person);

        /// <summary>
        ///     Saves the changes into the database.
        /// </summary>
        void SaveChanges();
    }
}
