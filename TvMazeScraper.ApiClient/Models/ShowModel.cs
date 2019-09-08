using System.Collections.Generic;

namespace TvMazeScraper.ApiClient.Models
{
    public class ShowModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CastMemberModel> Cast { get; set; }
    }
}
