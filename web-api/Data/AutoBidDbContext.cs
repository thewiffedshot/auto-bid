using Microsoft.EntityFrameworkCore;
using WebApi.Data.Models;

namespace AutoBid.WebApi.Data
{
    public class AutoBidDbContext : DbContext       
    {
        public DbSet<CarOffer> CarOffers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<CarAuction> CarAuctions { get; set; }
        
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