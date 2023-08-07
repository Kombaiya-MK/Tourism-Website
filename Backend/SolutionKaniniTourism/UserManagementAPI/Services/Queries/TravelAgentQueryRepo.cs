using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services.Queries
{
    public class TravelAgentQueryRepo : IQueryRepo<TravelAgent, string>
    {
        private readonly UserManagementContext _context;
        private readonly ILogger<TravelAgent> _logger;

        public TravelAgentQueryRepo(UserManagementContext context , ILogger<TravelAgent> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TravelAgent> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException("The give email address is null" , key);
            }
            var agent = await _context.TravelAgents.FirstOrDefaultAsync(x => x.AgencyEmail == key);
            if (agent == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException("No such agent is available");
            }
            return agent;
        }

        public async Task<ICollection<TravelAgent>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of TravelAgents");
            return await _context.TravelAgents.ToListAsync();
        }
    }
}
