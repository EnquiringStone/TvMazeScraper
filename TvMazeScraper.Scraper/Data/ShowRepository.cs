using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TvMazeScraper.Common.Data;
using TvMazeScraper.Common.Data.Entities;

namespace TvMazeScraper.Scraper.Data
{
    public class ShowRepository : IShowRepository
    {
        private readonly TvMazeDbContext dbContext;
        private readonly IMapper mapper;

        public ShowRepository(
            TvMazeDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Save(Show show)
        {
            var entity = dbContext.Shows.SingleOrDefault(s => s.ExternalId == show.ExternalId);
            if (entity == null) //Insert
            {
                show.CastMembers = new List<CastMember>();
                dbContext.Shows.Add(show);
            }
            else //Update
            {
                entity.Name = show.Name;
            }
        }
    }
}
