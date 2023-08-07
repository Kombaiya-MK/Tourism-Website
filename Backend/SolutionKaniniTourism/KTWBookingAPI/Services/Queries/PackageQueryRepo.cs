using KTWBookingAPI.Interfaces;
using KTWBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWBookingAPI.Services.Queries
{
    public class PackageQueryRepo : IQueryRepo<PackageBooking, string>
    {
        private readonly BookingContext _context;
        private readonly ILogger<PackageBooking> _logger;

        public PackageQueryRepo(BookingContext context, ILogger<PackageBooking> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<PackageBooking> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given id is null");
                throw new ArgumentNullException(nameof(key));
            }
            var pack = await _context.Packages.FirstOrDefaultAsync(x => x.PackageId == key);
            if (pack == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(pack));
            }
            return pack;
        }

        public async Task<ICollection<PackageBooking>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of booked packages");
            return await _context.Packages.ToListAsync();
        }
    }
}
