using Microsoft.EntityFrameworkCore;

namespace KodisoftAspNetWebApi.Models
{
    public class FeedsContext : DbContext
    {
        public DbSet<FeedItem> Items { get; set; }
        public DbSet<FeedList> UrlsList { get; set; }
        public FeedsContext(DbContextOptions<FeedsContext> options) : base(options)
        { }
    }
}