using KTWLocationsAPI.Interfaces;
using KTWLocationsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWLocationsAPI.Services.Queries
{
    public class ImageQueryRepo : IQueryRepo<Image , string>
    {
        private readonly LocationContext _context;
        private readonly ILogger<Image> _logger;

        public ImageQueryRepo(LocationContext context, ILogger<Image> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Image> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var Image = await _context.Images.FirstOrDefaultAsync(x => x.LocationId == key);
            if (Image == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(Image));
            }
            return Image;
        }

        public async Task<ICollection<Image>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of Images");
            return await _context.Images.ToListAsync();
        }
    }
}
