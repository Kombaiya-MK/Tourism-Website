<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UserManagementAPI.Models;

=======
>>>>>>> 4937c5a40d898d528f734aa7630daff1936145df
namespace TestUserAPI
{
    [TestClass]
    public class TestCommandsRepo
    {
<<<<<<< HEAD
        private static UserManagementContext? _context;

        public static DbContextOptions<UserManagementContext> GetDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<UserManagementContext>()
                .UseInMemoryDatabase(databaseName: "EmployeeBranchInMemory")
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


=======
        [TestMethod]
        public void TestMethod1()
        {
        }
>>>>>>> 4937c5a40d898d528f734aa7630daff1936145df
    }
}