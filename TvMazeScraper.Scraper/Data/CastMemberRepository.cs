using AutoMapper;
using System.Linq;
using TvMazeScraper.Common.Data;
using TvMazeScraper.Common.Data.Entities;
using TvMazeScraper.Scraper.Models.Data;

namespace TvMazeScraper.Scraper.Data
{
    public class CastMemberRepository : ICastMemberRepository
    {
        private readonly TvMazeDbContext dbContext;
        private readonly IMapper mapper;

        public CastMemberRepository(
            TvMazeDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void Save(CastMemberSaveModel castMember)
        {
            var entity = dbContext.CastMembers.SingleOrDefault(s => s.Person.ExternalId == castMember.ExternalPersonId && s.Show.ExternalId == castMember.ExternalShowId);
            if (entity != null) return;

            var person = dbContext.People.Single(s => s.ExternalId == castMember.ExternalPersonId);
            var show = dbContext.Shows.Single(s => s.ExternalId == castMember.ExternalShowId);

            dbContext.CastMembers.Add(new CastMember
            {
                Person = person,
                PersonId = person.Id,
                Show = show,
                ShowId = show.Id
            });
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
