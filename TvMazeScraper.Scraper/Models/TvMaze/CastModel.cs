using Newtonsoft.Json;

namespace TvMazeScraper.Scraper.Models.TvMaze
{
    public class CastModel
    {
        [JsonProperty("person")]
        public PersonModel Person { get; set; }
    }
}
