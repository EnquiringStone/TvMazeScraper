using TvMazeScraper.Scraper.Models.Data;

namespace TvMazeScraper.Scraper.Data
{
    public interface ICastMemberRepository
    {
        /// <summary>
        ///     Saves the specified cast member.
        /// </summary>
        /// <param name="castMember">The cast member to save.</param>
        void Save(CastMemberSaveModel castMember);
        
        /// <summary>
        ///     Save the changes into the database.
        /// </summary>
        void SaveChanges();
    }
}
