#nullable disable
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace UserManagementAPI.Models
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> Details { get;set; }
        public DbSet<TravelAgent> TravelAgents { get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var hmac = new HMACSHA256();
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = 1,
                Role = "Admin",
                Email = "Admin@gmail.com",
                HashKey = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin@123")),
                Password = hmac.Key
            }); ;
        }
    }
}
