using Microsoft.EntityFrameworkCore;
using TvMazeScraper.Common.Data.Entities;

namespace TvMazeScraper.Common.Data
{
    public class TvMazeDbContext : DbContext
    {
        public TvMazeDbContext(
            DbContextOptions<TvMazeDbContext> options) : base(options)
        {

        }

        public DbSet<Show> Shows { get; set; }

        public DbSet<CastMember> CastMembers { get; set; }

        public DbSet<Scrape> Scrapes { get; set; }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Show>()
                .HasIndex(s => s.ExternalId)
                .IsUnique();

            modelBuilder.Entity<Scrape>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Person>()
                .HasIndex(s => s.ExternalId)
                .IsUnique();
        }
    }
}
