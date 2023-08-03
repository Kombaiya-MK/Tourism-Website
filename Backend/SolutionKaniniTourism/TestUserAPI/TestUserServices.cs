using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Security.Cryptography;
using System.Text;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;
using UserManagementAPI.Services.Commands;

namespace TestUserAPI
{
    [TestClass]
    public class TestUserServices
    {
        private static UserManagementContext? _context;

        public static DbContextOptions<UserManagementContext> GetDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<UserManagementContext>()
                .UseInMemoryDatabase(databaseName: "UserManagement")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            _context = new UserManagementContext(options);
            return options;
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context?.Database.EnsureDeleted();
            _context?.Dispose();
        }

        [TestMethod]
        public async Task Login_User_Returns_UserDTO()
        {
            var hmac = new HMACSHA512();

            using (var context = new UserManagementContext(GetDbContextOptions()))
            {
                context.Users.Add(new User
                {
                    Id = 1,
                    Role = "Admin",
                    Email = "Admin@gmail.com",
                    HashKey = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin@123")),
                    Password = hmac.Key,
                    Status = "Active"
                });
                context.Users.Add(new User
                {
                    Id = 2,
                    Role = "TravelAgent",
                    Email = "Agent@gmail.com",
                    HashKey = hmac.ComputeHash(Encoding.UTF8.GetBytes("agent@123")),
                    Password = hmac.Key,
                    Status = "Active"
                });
                context.Users.Add(new User
                {
                    Id = 2,
                    Role = "user",
                    Email = "user@gmail.com",
                    HashKey = hmac.ComputeHash(Encoding.UTF8.GetBytes("user@123")),
                    Password = hmac.Key,
                    Status = "Active"
                });
                await context.SaveChangesAsync();
            }
            var mock = new Mock<ILogger<User>>();
            ILogger<User> logger = mock.Object;
            IQueryRepo<User, string> repo = new UserCommandsRepo(_context, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(3, data.Count);


        }


    }
}