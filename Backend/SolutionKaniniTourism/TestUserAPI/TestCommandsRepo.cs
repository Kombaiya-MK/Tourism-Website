using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UserManagementAPI.Models;

namespace TestUserAPI
{
    [TestClass]
    public class TestCommandsRepo
    {
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


    }
}