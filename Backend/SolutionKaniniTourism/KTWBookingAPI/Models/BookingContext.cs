#nullable disable
using Microsoft.EntityFrameworkCore;

namespace KTWBookingAPI.Models
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<PackageBooking> Packages { get; set; }
    }
}
