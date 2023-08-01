using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services.Queries
{
    public class UserQueryRepo : IQueryRepo<User , string>
    {
        private readonly UserManagementContext _context;
        private readonly ILogger<User> _logger;

        public UserQueryRepo(UserManagementContext context, ILogger<User> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<User> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == key);
            if (user == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(user));
            }
            return user;
        }

        public async Task<ICollection<User>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of User");
            return await _context.Users.ToListAsync();
        }
    }
}
