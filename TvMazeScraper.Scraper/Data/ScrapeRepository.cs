using AutoMapper;
using System.Linq;
using TvMazeScraper.Common.Data;
using TvMazeScraper.Common.Data.Entities;

namespace TvMazeScraper.Scraper.Data
{
    public class ScrapeRepository : IScrapeRepository
    {
        private readonly TvMazeDbContext dbContext;
        private readonly IMapper mapper;

        public ScrapeRepository(
            TvMazeDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public Scrape Get(string name)
        {
            return dbContext.Scrapes.SingleOrDefault(s => s.Name == name);
        }

        public void Save(Scrape scrape)
        {
            var entity = dbContext.Scrapes.SingleOrDefault(s => s.Name == scrape.Name);
            if (entity == null)
            {
                dbContext.Scrapes.Add(scrape);
            }
            else
            {
                mapper.Map(scrape, entity);
            }
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
