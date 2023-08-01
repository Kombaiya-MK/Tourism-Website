using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services.Queries
{
    public class UserDetailsQueryRepo : IQueryRepo<UserDetails, string>
    {
        private readonly UserManagementContext _context;
        private readonly ILogger<UserDetails> _logger;

        public UserDetailsQueryRepo(UserManagementContext context, ILogger<UserDetails> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<UserDetails> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var details = await _context.Details.FirstOrDefaultAsync(x => x.Email == key);
            if (details == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(details));
            }
            return details;
        }

        public async Task<ICollection<UserDetails>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of UserDetails");
            return await _context.Details.ToListAsync();
        }
    }
}
