#nullable disable
using Microsoft.EntityFrameworkCore;

namespace KTWFeedbackAPI.Models
{
    public class FeedbackContext : DbContext
    {
            public FeedbackContext(DbContextOptions options) : base(options)
            {

            }

            public DbSet<Feedback> Feedbacks { get; set; }
    }
}
