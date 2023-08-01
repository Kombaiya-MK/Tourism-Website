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
        public DbSet<VerificationCodes> VerificationCodes { get;set; }

    }
}
