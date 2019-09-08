using Newtonsoft.Json;
using System;

namespace TvMazeScraper.Scraper.Models.TvMaze
{
    public class PersonModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("birthday")]
        public DateTime? DateOfBirth { get; set; }
    }
}
