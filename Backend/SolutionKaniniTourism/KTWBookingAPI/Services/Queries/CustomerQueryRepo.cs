using KTWBookingAPI.Interfaces;
using KTWBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWBookingAPI.Services.Queries
{
    public class CustomerQueryRepo : IQueryRepo<Customer , string>
    {
        private readonly BookingContext _context;
        private readonly ILogger<Customer> _logger;

        public CustomerQueryRepo(BookingContext context, ILogger<Customer> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Customer> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var book = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == key);
            if (book == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(book));
            }
            return book;
        }

        public async Task<ICollection<Customer>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of Customers");
            return await _context.Customers.ToListAsync();
        }
    }
}
