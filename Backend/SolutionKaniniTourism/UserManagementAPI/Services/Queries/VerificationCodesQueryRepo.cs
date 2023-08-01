using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services.Queries
{
    public class VerificationCodesQueryRepo : IQueryRepo<VerificationCodes , string>
    {
        private readonly UserManagementContext _context;
        private readonly ILogger<VerificationCodes> _logger;

        public VerificationCodesQueryRepo(UserManagementContext context, ILogger<VerificationCodes> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<VerificationCodes> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var code = await _context.VerificationCodes.FirstOrDefaultAsync(x => x.Email == key);
            if (code == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(code));
            }
            return code;
        }

        public async Task<ICollection<VerificationCodes>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of VerificationCodess");
            return await _context.VerificationCodes.ToListAsync();
        }
    }
}
