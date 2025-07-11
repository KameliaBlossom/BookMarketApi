using BookMarketApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BookMarketApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<OnlineBook> OnlineBooks { get; set; }
    public DbSet<MarketBook> MarketBooks { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Tag> Tags { get; set; }
}