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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>().HasData(new Feedback()
            {
                Id = 1,
                FeedbackDate = DateTime.Now,
                FeedbackId = "FB001",
                Review = "Not good",
                Rating = 2,
                Email = "mksuresh044@gmail.com",
                PackId = "Pack001",
                ServiceName = "TP001",
                ServiceType = "Meal"
            });
        }



    }
}
