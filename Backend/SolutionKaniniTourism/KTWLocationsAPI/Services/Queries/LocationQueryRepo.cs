using KTWBookingAPI.Interfaces;
using KTWLocationsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWLocationsAPI.Services.Queries
{
    public class LocationQueryRepo : IQueryRepo<Location , string>
    {
        private readonly LocationContext _context;
        private readonly ILogger<Location> _logger;

        public LocationQueryRepo(LocationContext context, ILogger<Location> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Location> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var location = await _context.Locations.FirstOrDefaultAsync(x => x.LocationId == key);
            if (location == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(location));
            }
            return location;
        }

        public async Task<ICollection<Location>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of Locations");
            return await _context.Locations.ToListAsync();
        }
    }
}
