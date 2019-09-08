using System.Collections.Generic;
using TvMazeScraper.Common.Data.Entities;

namespace TvMazeScraper.ApiClient.Data
{
    public interface IShowRepository
    {
        /// <summary>
        ///     Gets all shows by the page and page size.
        /// </summary>
        /// <param name="page">Which page to select.</param>
        /// <param name="pageSize">Indicates how large the page size should be.</param>
        /// <returns>All shows for the page and page size.</returns>
        IEnumerable<Show> Get(int page, int pageSize);
    }
}
