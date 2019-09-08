using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TvMazeScraper.Common.ConfigurationSections;
using TvMazeScraper.Common.Data.Entities;
using TvMazeScraper.Common.Extensions;
using TvMazeScraper.Scraper.Data;
using TvMazeScraper.Scraper.Models.Data;
using TvMazeScraper.Scraper.Models.TvMaze;

namespace TvMazeScraper.Scraper.Services
{
    public class TvMazeScrapeService : ITvMazeScrapeService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IScrapeRepository scrapeRepository;
        private readonly ILogger<TvMazeScrapeService> logger;
        private readonly IMapper mapper;
        private readonly IShowRepository showRepository;
        private readonly IPersonRepository personRepository;
        private readonly ICastMemberRepository castMemberRepository;

        public TvMazeScrapeService(
            IHttpClientFactory httpClientFactory,
            IScrapeRepository scrapeRepository,
            ILogger<TvMazeScrapeService> logger,
            IMapper mapper,
            IShowRepository showRepository,
            IPersonRepository personRepository,
            ICastMemberRepository castMemberRepository)
        {
            this.httpClientFactory = httpClientFactory;
            this.scrapeRepository = scrapeRepository;
            this.logger = logger;
            this.mapper = mapper;
            this.showRepository = showRepository;
            this.personRepository = personRepository;
            this.castMemberRepository = castMemberRepository;
        }

        public async Task ScrapeAsync()
        {
            var scrape = FindScrape(TvMazeApiSettings.SectionName);

            if (scrape.IsScrapeInProgress)
            {
                logger.LogInformation($"Scrape already in progres. Started at {scrape.StartDate}");

                return;
            }

            try
            {
                scrape.IsScrapeInProgress = true;
                scrape.StartDate = DateTime.Now;
                scrapeRepository.Save(scrape);
                scrapeRepository.SaveChanges();

                var shows = await SaveShowsAsync();
                await SaveCastMembersAsync(shows);

                SetScrapeToFinished(scrape);

                logger.LogInformation("Scraping succeeded");
            }
            catch (Exception exception)
            {
                logger.LogWarning(exception, "Scraping failed.");
                SetScrapeToFinished(scrape);
                throw;
            }
        }

        private void SetScrapeToFinished(Scrape scrape)
        {
            scrape.IsScrapeInProgress = false;
            scrapeRepository.Save(scrape);
            scrapeRepository.SaveChanges();
        }

        private Scrape FindScrape(string name)
        {
            var scrape = scrapeRepository.Get(name);
            if (scrape == null)
            {
                scrape = new Scrape
                {
                    IsScrapeInProgress = false,
                    Name = name
                };
                scrapeRepository.Save(scrape);
                scrapeRepository.SaveChanges();
            }

            return scrape;
        }

        private async Task<T[]> HandleResponseAsync<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                logger.LogWarning("Response is invalid.");
                return Array.Empty<T>();
            }

            var jsonResponse = await response.Content?.ReadAsStringAsync();
            if (jsonResponse == null)
            {
                logger.LogWarning("No content found.");
                return Array.Empty<T>();
            }

            return JsonConvert.DeserializeObject<T[]>(jsonResponse);
        }

        private async Task<IEnumerable<Show>> SaveShowsAsync()
        {
            using (var client = httpClientFactory.CreateClient(TvMazeApiSettings.SectionName))
            {
                List<Show> savedShows = new List<Show>();

                var response = await client.GetAsync("shows");

                var shows = await HandleResponseAsync<ShowModel>(response);

                foreach (var show in shows)
                {
                    var showEntity = mapper.Map<Show>(show);
                    showRepository.Save(showEntity);

                    savedShows.Add(showEntity);
                }

                showRepository.SaveChanges();

                return savedShows;
            }
        }

        private async Task SaveCastMembersAsync(IEnumerable<Show> shows)
        {
            foreach (var show in shows)
            {
                using (var client = httpClientFactory.CreateClient(TvMazeApiSettings.SectionName))
                {
                    var response = await client.GetAsync($"shows/{show.ExternalId}/cast");

                    var castMembers = await HandleResponseAsync<CastModel>(response);

                    var people = castMembers.Select(c => c.Person).ToArray();

                    SavePeopleAsCastMembers(show, people);
                }
            }
        }

        private void SavePeopleAsCastMembers(Show show, IEnumerable<PersonModel> people)
        {
            logger.LogInformation($"Adding people for show {show.ExternalId}");

            var castMembers = new List<CastMemberSaveModel>();

            foreach (var person in people.DistinctBy(p => p.Id))
            {
                personRepository.Save(mapper.Map<Person>(person));
                castMembers.Add(new CastMemberSaveModel
                {
                    ExternalPersonId = person.Id,
                    ExternalShowId = show.ExternalId
                });
            }

            personRepository.SaveChanges();

            foreach(var castMember in castMembers)
            {
                castMemberRepository.Save(castMember);
            }

            castMemberRepository.SaveChanges();
        }
        
    }
}
