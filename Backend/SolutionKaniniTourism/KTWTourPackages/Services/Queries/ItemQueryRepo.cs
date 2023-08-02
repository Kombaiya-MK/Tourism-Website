using KTWTourPackages.Interfaces;
using KTWTourPackages.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWTourPackages.Services.Queries
{
    public class ItemQueryRepo : IQueryRepo<ItineraryItem , string>
    {
        private readonly PackageContext _context;
        private readonly ILogger<ItineraryItem> _logger;

        public ItemQueryRepo(PackageContext context, ILogger<ItineraryItem> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ItineraryItem> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var ItineraryItem = await _context.TourariesItem.FirstOrDefaultAsync(x => x.PackId == key);
            if (ItineraryItem == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(ItineraryItem));
            }
            return ItineraryItem;
        }

        public async Task<ICollection<ItineraryItem>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of ItineraryItems");
            return await _context.TourariesItem.ToListAsync();
        }
    }
}
