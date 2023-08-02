#nullable disable
using KTWTourPackages.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWTourPackages.Models
{
    public class PackageContext : DbContext
    {
        public PackageContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<TourPackage> TourPackages { get; set; }
        public DbSet<Itinerary> Touraries { get; set; }
        public DbSet<ItineraryItem> TourariesItem { get; set;}
    }
}
