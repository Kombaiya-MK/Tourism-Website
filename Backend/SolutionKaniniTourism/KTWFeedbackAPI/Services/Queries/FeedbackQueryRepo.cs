using KTWFeedbackAPI.Interfaces;
using KTWFeedbackAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWFeedbackAPI.Services.Queries
{
    public class FeedbackQueryRepo : IQueryRepo<Feedback , string>
    {
        private readonly FeedbackContext _context;
        private readonly ILogger<Feedback> _logger;

        public FeedbackQueryRepo(FeedbackContext context, ILogger<Feedback> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Feedback> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(x => x.Email == key);
            if (feedback == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(feedback));
            }
            return feedback;
        }

        public async Task<ICollection<Feedback>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of Feedbacks");
            return await _context.Feedbacks.ToListAsync();
        }
    }
}
