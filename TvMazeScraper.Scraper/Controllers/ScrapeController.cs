using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using TvMazeScraper.Scraper.Services;

namespace TvMazeScraper.Scraper.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ScrapeController : ControllerBase
    {
        private readonly ITvMazeScrapeService tvMazeScrapeService;

        public ScrapeController(
            ITvMazeScrapeService tvMazeScrapeService)
        {
            this.tvMazeScrapeService = tvMazeScrapeService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> ForceScrape()
        {
            await tvMazeScrapeService.ScrapeAsync();

            return Ok();
        }
    }
}
