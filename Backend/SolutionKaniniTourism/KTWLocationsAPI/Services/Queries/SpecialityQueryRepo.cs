using KTWBookingAPI.Interfaces;
using KTWLocationsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWLocationsAPI.Services.Queries
{
    public class SpecialityQueryRepo : IQueryRepo<Speciality , string>
    {
        private readonly LocationContext _context;
        private readonly ILogger<Speciality> _logger;

        public SpecialityQueryRepo(LocationContext context, ILogger<Speciality> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Speciality> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var Speciality = await _context.Specializations.FirstOrDefaultAsync(x => x.LocationId == key);
            if (Speciality == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(Speciality));
            }
            return Speciality;
        }

        public async Task<ICollection<Speciality>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of Specialitys");
            return await _context.Specializations.ToListAsync();
        }
    }
}
