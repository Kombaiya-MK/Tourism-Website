#nullable disable
using Microsoft.EntityFrameworkCore;

namespace KTWLocationsAPI.Models
{
    public class LocationContext : DbContext
    {
        public LocationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Speciality> Specializations { get; set; }
    }
}
