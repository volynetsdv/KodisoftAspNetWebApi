using Microsoft.EntityFrameworkCore;

namespace KodisoftAspNetWebApi.Models
{
    public class FeedsContext : DbContext
    {
        public DbSet<FeedItem> Items { get; set; }
        public DbSet<FeedList> UrlsList { get; set; }
        public FeedsContext(DbContextOptions<FeedsContext> options) : base(options)
        { }

        public FeedsContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=feedsdbstore;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}