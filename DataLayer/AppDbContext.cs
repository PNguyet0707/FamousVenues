using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
namespace DataLayer.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Venue> Venues { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Venue>()
                .ToTable(v => v.HasCheckConstraint("CK_Venue_Latitude", "Latitude >= -90 AND Latitude <= 90"));
            modelBuilder.Entity<Venue>()
            .ToTable(v => v.HasCheckConstraint("CK_Venue_Longitude", "Longitude >= -180 AND Longitude <= 180"));
        }
    }
}
