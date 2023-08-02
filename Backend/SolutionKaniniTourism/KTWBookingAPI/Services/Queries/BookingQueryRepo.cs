using KTWBookingAPI.Interfaces;
using KTWBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWBookingAPI.Services.Queries
{
    public class BookingQueryRepo : IQueryRepo<Booking,string>
    {
        private readonly BookingContext _context;
        private readonly ILogger<Booking> _logger;

        public BookingQueryRepo(BookingContext context, ILogger<Booking> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Booking> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var book = await _context.Bookings.FirstOrDefaultAsync(x => x.Email == key);
            if (book == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(book));
            }
            return book;
        }

        public async Task<ICollection<Booking>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of Bookings");
            return await _context.Bookings.ToListAsync();
        }
    }
}
