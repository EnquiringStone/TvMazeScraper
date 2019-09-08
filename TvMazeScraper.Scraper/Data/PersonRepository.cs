using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TvMazeScraper.Common.Data;
using TvMazeScraper.Common.Data.Entities;

namespace TvMazeScraper.Scraper.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly TvMazeDbContext dbContext;
        private readonly IMapper mapper;

        public PersonRepository(
            TvMazeDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void Save(Person person)
        {
            var entity = dbContext.People.SingleOrDefault(s => s.ExternalId == person.ExternalId);
            if (entity == null)
            {
                person.CastMembers = new List<CastMember>();

                dbContext.People.Add(person);
            }
            else
            {
                mapper.Map(person, entity);
            }
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
