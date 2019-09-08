using Newtonsoft.Json;

namespace TvMazeScraper.Scraper.Models.TvMaze
{
    public class ShowModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
