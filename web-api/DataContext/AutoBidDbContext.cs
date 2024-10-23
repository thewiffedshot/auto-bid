using Microsoft.EntityFrameworkCore;
using WebApi.DataContext.Models;

namespace AutoBid.WebApi.DataContext
{
    public class AutoBidDbContext : DbContext
    {
        public DbSet<CarOffer> CarOffers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CarImage> CarImages { get; set; }

        public AutoBidDbContext(DbContextOptions<AutoBidDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<CarOffer>()
                .Property(e => e.Make)
                .HasConversion<string>();
        }
    }
}