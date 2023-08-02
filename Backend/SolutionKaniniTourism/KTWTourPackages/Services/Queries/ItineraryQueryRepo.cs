using KTWTourPackages.Interfaces;
using KTWTourPackages.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWTourPackages.Services.Queries
{
    public class ItineraryQueryRepo : IQueryRepo<Itinerary , string>
    {
        private readonly PackageContext _context;
        private readonly ILogger<Itinerary> _logger;

        public ItineraryQueryRepo(PackageContext context, ILogger<Itinerary> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Itinerary> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var Itinerary = await _context.Touraries.FirstOrDefaultAsync(x => x.PackId == key);
            if (Itinerary == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(Itinerary));
            }
            return Itinerary;
        }

        public async Task<ICollection<Itinerary>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of Itineraries");
            return await _context.Touraries.ToListAsync();
        }
    }
}
