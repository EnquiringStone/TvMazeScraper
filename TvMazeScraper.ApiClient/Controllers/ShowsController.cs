using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TvMazeScraper.ApiClient.Data;
using TvMazeScraper.ApiClient.Models;

namespace TvMazeScraper.ApiClient.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ShowsController : ControllerBase
    {
        private readonly IShowRepository showRepository;
        private readonly IMapper mapper;

        public ShowsController(
            IShowRepository showRepository,
            IMapper mapper)
        {
            this.showRepository = showRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShowModel>))]
        public IActionResult Get(int page, int pageSize)
        {
            var result = showRepository.Get(page, pageSize);

            var shows = mapper.Map<IEnumerable<ShowModel>>(result);

            foreach (var show in shows)
            {
                show.Cast = show.Cast.OrderByDescending(s => s.DateOfBirth);
            }

            return Ok(shows);
        }
    }
}
