using KTWTourPackages.Interfaces;
using KTWTourPackages.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWTourPackages.Services.Queries
{
    public class PackageQueryRepo : IQueryRepo<TourPackage , string>
    {
        private readonly PackageContext _context;
        private readonly ILogger<TourPackage> _logger;

        public PackageQueryRepo(PackageContext context, ILogger<TourPackage> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TourPackage> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var TourPackage = await _context.TourPackages.FirstOrDefaultAsync(x => x.PackId == key);
            if (TourPackage == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(TourPackage));
            }
            return TourPackage;
        }

        public async Task<ICollection<TourPackage>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of TourPackages");
            return await _context.TourPackages.ToListAsync();
        }
    }
}
