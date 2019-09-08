using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TvMazeScraper.Common.Data;
using TvMazeScraper.Common.Data.Entities;

namespace TvMazeScraper.ApiClient.Data
{
    public class ShowRepository : IShowRepository
    {
        private readonly TvMazeDbContext dbContext;

        public ShowRepository(
            TvMazeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Show> Get(int page, int pageSize)
        {
            var shows = dbContext.Shows
                .Include(s => s.CastMembers)
                    .ThenInclude(c => c.Person)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(d => d.Name)
                .ToArray();

            return shows;
        }
    }
}
