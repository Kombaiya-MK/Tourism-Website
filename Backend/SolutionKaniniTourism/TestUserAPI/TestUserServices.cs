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
using UserManagementAPI.Services.Queries;

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

        //Positive Testing

        [TestMethod]
        public async Task Add_User_Returns_User()
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
            using var userContext = new UserManagementContext(GetDbContextOptions());
            var mock = new Mock<ILogger<User>>();
            ILogger<User> logger = mock.Object;
            IQueryRepo<User, string> repo = new UserQueryRepo(userContext, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(3, data.Count);
        }


        [TestMethod]
        public async Task GetAll_User_Returns_Users()
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
            using var userContext = new UserManagementContext(GetDbContextOptions());
            var mock = new Mock<ILogger<User>>();
            ILogger<User> logger = mock.Object;
            IQueryRepo<User, string> repo = new UserQueryRepo(userContext, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(3, data.Count);
        }

        [TestMethod]
        public async Task Get_User_Returns_User()
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
            using var userContext = new UserManagementContext(GetDbContextOptions());
            var mock = new Mock<ILogger<User>>();
            ILogger<User> logger = mock.Object;
            IQueryRepo<User, string> repo = new UserQueryRepo(userContext, logger);
            var data = await repo.Get("Agent@gmail.com");
            Assert.AreEqual(data.Role, "TravelAgent");
        }

        [TestMethod]
        public async Task Update_User_Returns_User()
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
                    Id = 3,
                    Role = "user",
                    Email = "user@gmail.com",
                    HashKey = hmac.ComputeHash(Encoding.UTF8.GetBytes("user@123")),
                    Password = hmac.Key,
                    Status = "Active"
                });
                await context.SaveChangesAsync();
            }
            using var userContext = new UserManagementContext(GetDbContextOptions());
            var mock = new Mock<ILogger<User>>();
            ILogger<User> logger = mock.Object;
            userContext.Update(new User
            {
                Id = 2,
                Role = "Admin",
                Email = "user@gmail.com",
                HashKey = hmac.ComputeHash(Encoding.UTF8.GetBytes("user@123")),
                Password = hmac.Key,
                Status = "Active"
            });
            IQueryRepo<User, string> repo = new UserQueryRepo(userContext, logger);
            var data = await repo.Get("user@gmail.com");
            Assert.AreEqual(data.Role, "Admin");
        }

        [TestMethod]
        public async Task Add_UserDetail_Returns_UserDetail()
        {
            using (var context = new UserManagementContext(GetDbContextOptions()))
            {
                context.Details.Add(new UserDetails
                {
                    Id = 1,
                    Email = "Admin@gmail.com",
                    PhoneNumber = "9994291196",
                    Address = "No 1 , ABC st , Coimbatore",
                    Gender = "Male",
                    UserName = "Test1",
                    DateofBirth = DateTime.Now,

                });
                context.Details.Add(new UserDetails
                {
                    Id = 1,
                    Email = "user@gmail.com",
                    PhoneNumber = "9786543210",
                    Address = "No 1 , ABC st , Coimbatore",
                    Gender = "Male",
                    UserName = "Test2",
                    DateofBirth = DateTime.Now,

                });
                context.Details.Add(new UserDetails
                {
                    Id = 1,
                    Email = "agent@gmail.com",
                    PhoneNumber = "6382205871",
                    Address = "No 1 , ABC st , Coimbatore",
                    Gender = "Male",
                    UserName = "Test3",
                    DateofBirth = DateTime.Now,

                });
                await context.SaveChangesAsync();
            }
            using var userContext = new UserManagementContext(GetDbContextOptions());
            var mock = new Mock<ILogger<UserDetails>>();
            ILogger<UserDetails> logger = mock.Object;
            IQueryRepo<UserDetails, string> repo = new UserDetailsQueryRepo(userContext, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(3, data.Count);
        }


        [TestMethod]
        public async Task GetAll_UserDetails_Returns_UserDetails()
        {
            using (var context = new UserManagementContext(GetDbContextOptions()))
            {
                context.Details.Add(new UserDetails
                {
                    Id = 1,
                    Email = "Admin@gmail.com",
                    PhoneNumber = "9994291196",
                    Address = "No 1 , ABC st , Coimbatore",
                    Gender = "Male",
                    UserName = "Test1",
                    DateofBirth = DateTime.Now,

                });
                context.Details.Add(new UserDetails
                {
                    Id = 1,
                    Email = "user@gmail.com",
                    PhoneNumber = "9786543210",
                    Address = "No 1 , ABC st , Coimbatore",
                    Gender = "Male",
                    UserName = "Test2",
                    DateofBirth = DateTime.Now,

                });
                context.Details.Add(new UserDetails
                {
                    Id = 1,
                    Email = "agent@gmail.com",
                    PhoneNumber = "6382205871",
                    Address = "No 1 , ABC st , Coimbatore",
                    Gender = "Male",
                    UserName = "Test3",
                    DateofBirth = DateTime.Now,

                });
                await context.SaveChangesAsync();
            }
            using UserManagementContext userContext = new(GetDbContextOptions());
            var mock = new Mock<ILogger<UserDetails>>();
            ILogger<UserDetails> logger = mock.Object;
            IQueryRepo<UserDetails, string> repo = new UserDetailsQueryRepo(userContext, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(3, data.Count);
        }

        [TestMethod]
        public async Task Get_UserDetails_Returns_UserDetails()
        {
            using (var context = new UserManagementContext(GetDbContextOptions()))
            {
                context.Details.Add(new UserDetails
                {
                    Id = 1,
                    Email = "Admin@gmail.com",
                    PhoneNumber = "9994291196",
                    Address = "No 1 , ABC st , Coimbatore",
                    Gender = "Male",
                    UserName = "Test1",
                    DateofBirth = DateTime.Now,

                });
                context.Details.Add(new UserDetails
                {
                    Id = 1,
                    Email = "user@gmail.com",
                    PhoneNumber = "9786543210",
                    Address = "No 1 , ABC st , Coimbatore",
                    Gender = "Male",
                    UserName = "Test2",
                    DateofBirth = DateTime.Now,

                });
                context.Details.Add(new UserDetails
                {
                    Id = 1,
                    Email = "agent@gmail.com",
                    PhoneNumber = "6382205871",
                    Address = "No 1 , ABC st , Coimbatore",
                    Gender = "Male",
                    UserName = "Test3",
                    DateofBirth = DateTime.Now,

                });
                await context.SaveChangesAsync();
            }
            using var userContext = new UserManagementContext(GetDbContextOptions());
            var mock = new Mock<ILogger<UserDetails>>();
            ILogger<UserDetails> logger = mock.Object;
            IQueryRepo<UserDetails, string> repo = new UserDetailsQueryRepo(userContext, logger);
            var data = await repo.Get("agent@gmail.com");
            Assert.AreEqual(data.UserName, "Test3");
        }

        [TestMethod]
        public async Task Update_UserDetails_Returns_UserDetails()
        {
            using (var context = new UserManagementContext(GetDbContextOptions()))
            {
                context.Details.Add(new UserDetails
                {
                    Id = 1,
                    Email = "Admin@gmail.com",
                    PhoneNumber = "9994291196",
                    Address = "No 1 , ABC st , Coimbatore",
                    Gender = "Male",
                    UserName = "Test1",
                    DateofBirth = DateTime.Now,

                });
                context.Details.Add(new UserDetails
                {
                    Id = 1,
                    Email = "user@gmail.com",
                    PhoneNumber = "9786543210",
                    Address = "No 1 , ABC st , Coimbatore",
                    Gender = "Male",
                    UserName = "Test2",
                    DateofBirth = DateTime.Now,

                });
                context.Details.Add(new UserDetails
                {
                    Id = 1,
                    Email = "agent@gmail.com",
                    PhoneNumber = "6382205871",
                    Address = "No 1 , ABC st , Coimbatore",
                    Gender = "Male",
                    UserName = "Test3",
                    DateofBirth = DateTime.Now,

                });
                await context.SaveChangesAsync();
            }
            using var userContext = new UserManagementContext(GetDbContextOptions());
            var mock = new Mock<ILogger<UserDetails>>();
            ILogger<UserDetails> logger = mock.Object;
            userContext.Update(new UserDetails
            {
                Id = 1,
                Email = "Admin@gmail.com",
                PhoneNumber = "9994291196",
                Address = "No 1 , ABC st , Coimbatore",
                Gender = "Male",
                UserName = "Test1"
            });
            IQueryRepo<UserDetails, string> repo = new UserDetailsQueryRepo(userContext, logger);
            var data = await repo.Get("Admin@gmail.com");
            Assert.AreEqual(data.UserName, "Test1");
        }

        [TestMethod]
        public async Task Add_Agent_Returns_Agent()
        {
            using (var context = new UserManagementContext(GetDbContextOptions()))
            {
                context.TravelAgents.Add(new TravelAgent
                {
                    Id = 1,
                    Email = "Admin@gmail.com",
                    AgencyEmail = "Agency1@gmail.com",
                    AgencyPhone = "9994291196",
                    AgencyAddress = "No 1 , ABC st , Coimbatore",
                    AgencyName = "Agency1",

                });
                context.TravelAgents.Add(new TravelAgent
                {
                    Id = 2,
                    Email = "user@gmail.com",
                    AgencyEmail = "Agency2@gmail.com",
                    AgencyPhone = "9786543210",
                    AgencyAddress = "No 1 , ABC st , Coimbatore",
                    AgencyName = "Agency2",

                });
                context.TravelAgents.Add(new TravelAgent
                {
                    Id = 3,
                    Email = "agent@gmail.com",
                    AgencyEmail = "Agency3@gmail.com",
                    AgencyPhone = "6382205871",
                    AgencyAddress = "No 1 , ABC st , Coimbatore",
                    AgencyName = "Agency3",

                });
                await context.SaveChangesAsync();
            }
            using var userContext = new UserManagementContext(GetDbContextOptions());
            var mock = new Mock<ILogger<TravelAgent>>();
            ILogger<TravelAgent> logger = mock.Object;
            IQueryRepo<TravelAgent, string> repo = new TravelAgentQueryRepo(userContext, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(3, data.Count);
        }


        [TestMethod]
        public async Task GetAll_TravelAgent_Returns_TravelAgents()
        { 
            using (var context = new UserManagementContext(GetDbContextOptions()))
            {
                context.TravelAgents.Add(new TravelAgent
                {
                    Id = 1,
                    Email = "Admin@gmail.com",
                    AgencyEmail = "Agency1@gmail.com",
                    AgencyPhone = "9994291196",
                    AgencyAddress = "No 1 , ABC st , Coimbatore",
                    AgencyName = "Agency1",

                });
                context.TravelAgents.Add(new TravelAgent
                {
                    Id = 2,
                    Email = "user@gmail.com",
                    AgencyEmail = "Agency2@gmail.com",
                    AgencyPhone = "9786543210",
                    AgencyAddress = "No 1 , ABC st , Coimbatore",
                    AgencyName = "Agency2",

                });
                context.TravelAgents.Add(new TravelAgent
                {
                    Id = 3,
                    Email = "agent@gmail.com",
                    AgencyEmail = "Agency3@gmail.com",
                    AgencyPhone = "6382205871",
                    AgencyAddress = "No 1 , ABC st , Coimbatore",
                    AgencyName = "Agency3",

                });
                await context.SaveChangesAsync();
            }
            using UserManagementContext userContext = new(GetDbContextOptions());
            var mock = new Mock<ILogger<TravelAgent>>();
            ILogger<TravelAgent> logger = mock.Object;
            IQueryRepo<TravelAgent, string> repo = new TravelAgentQueryRepo(userContext, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(3, data.Count);
        }

        [TestMethod]
        public async Task Get_TravelAgent_Returns_TravelAgent()
        {
            using (var context = new UserManagementContext(GetDbContextOptions()))
            {
                context.TravelAgents.Add(new TravelAgent
                {
                    Id = 1,
                    Email = "Admin@gmail.com",
                    AgencyEmail = "Agency1@gmail.com",
                    AgencyPhone = "9994291196",
                    AgencyAddress = "No 1 , ABC st , Coimbatore",
                    AgencyName = "Agency1",

                });
                context.TravelAgents.Add(new TravelAgent
                {
                    Id = 2,
                    Email = "user@gmail.com",
                    AgencyEmail = "Agency2@gmail.com",
                    AgencyPhone = "9786543210",
                    AgencyAddress = "No 1 , ABC st , Coimbatore",
                    AgencyName = "Agency2",

                });
                context.TravelAgents.Add(new TravelAgent
                {
                    Id = 3,
                    Email = "agent@gmail.com",
                    AgencyEmail = "Agency3@gmail.com",
                    AgencyPhone = "6382205871",
                    AgencyAddress = "No 1 , ABC st , Coimbatore",
                    AgencyName = "Agency3",

                });
                await context.SaveChangesAsync();
            }
            using var userContext = new UserManagementContext(GetDbContextOptions());
            var mock = new Mock<ILogger<TravelAgent>>();
            ILogger<TravelAgent> logger = mock.Object;
            IQueryRepo<TravelAgent, string> repo = new TravelAgentQueryRepo(userContext, logger);
            var data = await repo.Get("agent@gmail.com");
            Assert.AreEqual(data.AgencyPhone, "6382205871");
        }

        [TestMethod]
        public async Task Update_TravelAgent_Returns_TravelAgent()
        {
            using (var context = new UserManagementContext(GetDbContextOptions()))
            {
                context.TravelAgents.Add(new TravelAgent
                {
                    Id = 1,
                    Email = "Admin@gmail.com",
                    AgencyEmail = "Agency1@gmail.com",
                    AgencyPhone = "9994291196",
                    AgencyAddress = "No 1 , ABC st , Coimbatore",
                    AgencyName = "Agency1",

                });
                context.TravelAgents.Add(new TravelAgent
                {
                    Id = 2,
                    Email = "user@gmail.com",
                    AgencyEmail = "Agency2@gmail.com",
                    AgencyPhone = "9786543210",
                    AgencyAddress = "No 1 , ABC st , Coimbatore",
                    AgencyName = "Agency2",

                });
                context.TravelAgents.Add(new TravelAgent
                {
                    Id = 3,
                    Email = "agent@gmail.com",
                    AgencyEmail = "Agency3@gmail.com",
                    AgencyPhone = "6382205871",
                    AgencyAddress = "No 1 , ABC st , Coimbatore",
                    AgencyName = "Agency3",

                });
                await context.SaveChangesAsync();
            }
            using var userContext = new UserManagementContext(GetDbContextOptions());
            var mock = new Mock<ILogger<TravelAgent>>();
            ILogger<TravelAgent> logger = mock.Object;
            userContext.Update(new TravelAgent
            {
                Id = 3,
                Email = "agent@gmail.com",
                AgencyEmail = "Agency3@gmail.com",
                AgencyPhone = "6382205872",
                AgencyAddress = "No 1 , ABC st , Coimbatore",
                AgencyName = "Agency3",

            });
            IQueryRepo<TravelAgent, string> repo = new TravelAgentQueryRepo(userContext, logger);
            var data = await repo.Get("user@gmail.com");
            Assert.AreEqual(data.AgencyPhone, "6382205872");
        }
    }
}