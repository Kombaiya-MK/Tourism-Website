using KTWBookingAPI.Interfaces;
using KTWBookingAPI.Models;
using KTWBookingAPI.Services.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Cryptography;
using System.Text;

namespace BookingTest
{
    [TestClass]
    public class TestBookingRepo
    {

        private static BookingContext? _context;

        public static DbContextOptions<BookingContext> GetDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase(databaseName: "BookingManagement")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            _context = new BookingContext(options);
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
        public async Task Add_Booking_Returns_Booking()
        {
            using (var context = new BookingContext(GetDbContextOptions()))
            {
                context.Bookings.Add(new Booking
                {
                    Id = 1,
                    Status = "Confirmed",
                    BookedDate = DateTime.UtcNow,
                    BookingId = "Book001",
                    CheckInDate = new DateTime(2023, 8, 8),
                    CheckOutDate = new DateTime(2023, 8, 12),
                    PaymentMethod = "UPI",
                    Price = 2000,
                    Email = "user1@gmail.com"

                });
                context.Bookings.Add(new Booking
                {
                    Id = 2,
                    Status = "Confirmed",
                    BookedDate = DateTime.UtcNow,
                    BookingId = "Book002",
                    CheckInDate = new DateTime(2023, 8, 8),
                    CheckOutDate = new DateTime(2023, 8, 12),
                    PaymentMethod = "UPI",
                    Price = 2000,
                    Email = "user2@gmail.com"
                });
                context.Bookings.Add(new Booking
                {
                    Id = 3,
                    Status = "Confirmed",
                    BookedDate = DateTime.UtcNow,
                    BookingId = "Book003",
                    CheckInDate = new DateTime(2023, 8, 8),
                    CheckOutDate = new DateTime(2023, 8, 12),
                    PaymentMethod = "UPI",
                    Price = 2000,
                    Email = "user3@gmail.com"
                });
                await context.SaveChangesAsync();
            }
            using var BookingContext = new BookingContext(GetDbContextOptions());
            var mock = new Mock<ILogger<Booking>>();
            ILogger<Booking> logger = mock.Object;
            IQueryRepo<Booking, string> repo = new BookingQueryRepo(BookingContext, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(3, data.Count);
        }


        [TestMethod]
        public async Task GetAll_Booking_Returns_Booking()
        {
            using (var context = new BookingContext(GetDbContextOptions()))
            {
                context.Bookings.Add(new Booking
                {
                    Id = 1,
                    Status = "Confirmed",
                    BookedDate = DateTime.UtcNow,
                    BookingId = "Book001",
                    CheckInDate = new DateTime(2023, 8, 8),
                    CheckOutDate = new DateTime(2023, 8, 12),
                    PaymentMethod = "UPI",
                    Price = 2000,
                    Email = "user1@gmail.com"

                });
                context.Bookings.Add(new Booking
                {
                    Id = 2,
                    Status = "Confirmed",
                    BookedDate = DateTime.UtcNow,
                    BookingId = "Book002",
                    CheckInDate = new DateTime(2023, 8, 8),
                    CheckOutDate = new DateTime(2023, 8, 12),
                    PaymentMethod = "UPI",
                    Price = 2000,
                    Email = "user2@gmail.com"
                });
                context.Bookings.Add(new Booking
                {
                    Id = 3,
                    Status = "Confirmed",
                    BookedDate = DateTime.UtcNow,
                    BookingId = "Book003",
                    CheckInDate = new DateTime(2023, 8, 8),
                    CheckOutDate = new DateTime(2023, 8, 12),
                    PaymentMethod = "UPI",
                    Price = 2000,
                    Email = "user3@gmail.com"
                });
                await context.SaveChangesAsync();
            }
            using var BookingContext = new BookingContext(GetDbContextOptions());
            var mock = new Mock<ILogger<Booking>>();
            ILogger<Booking> logger = mock.Object;
            IQueryRepo<Booking, string> repo = new BookingQueryRepo(BookingContext, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(3, data.Count);
        }

        [TestMethod]
        public async Task Get_Booking_Returns_Booking()
        {
            using (var context = new BookingContext(GetDbContextOptions()))
            {
                context.Bookings.Add(new Booking
                {
                    Id = 1,
                    Status = "Confirmed",
                    BookedDate = DateTime.UtcNow,
                    BookingId = "Book001",
                    CheckInDate = new DateTime(2023, 8, 8),
                    CheckOutDate = new DateTime(2023, 8, 12),
                    PaymentMethod = "UPI",
                    Price = 2000,
                    Email = "user1@gmail.com"

                });
                context.Bookings.Add(new Booking
                {
                    Id = 2,
                    Status = "Confirmed",
                    BookedDate = DateTime.UtcNow,
                    BookingId = "Book002",
                    CheckInDate = new DateTime(2023, 8, 8),
                    CheckOutDate = new DateTime(2023, 8, 12),
                    PaymentMethod = "UPI",
                    Price = 2000,
                    Email = "user2@gmail.com"
                });
                context.Bookings.Add(new Booking
                {
                    Id = 3,
                    Status = "Confirmed",
                    BookedDate = DateTime.UtcNow,
                    BookingId = "Book003",
                    CheckInDate = new DateTime(2023, 8, 8),
                    CheckOutDate = new DateTime(2023, 8, 12),
                    PaymentMethod = "UPI",
                    Price = 2000,
                    Email = "user3@gmail.com"
                });
                await context.SaveChangesAsync();
            }
            using var BookingContext = new BookingContext(GetDbContextOptions());
            var mock = new Mock<ILogger<Booking>>();
            ILogger<Booking> logger = mock.Object;
            IQueryRepo<Booking, string> repo = new BookingQueryRepo(BookingContext, logger);
            var data = await repo.Get("user3@gmail.com");
            Assert.AreEqual(data.Price, 2000);
        }

        [TestMethod]
        public async Task Update_Booking_Returns_Booking()
        {
            using (var context = new BookingContext(GetDbContextOptions()))
            {
                context.Bookings.Add(new Booking
                {
                    Id = 1,
                    Status = "Confirmed",
                    BookedDate = DateTime.UtcNow,
                    BookingId = "Book001",
                    CheckInDate = new DateTime(2023, 8, 8),
                    CheckOutDate = new DateTime(2023, 8, 12),
                    PaymentMethod = "UPI",
                    Price = 2000,
                    Email = "user1@gmail.com"

                });
                context.Bookings.Add(new Booking
                {
                    Id = 2,
                    Status = "Confirmed",
                    BookedDate = DateTime.UtcNow,
                    BookingId = "Book002",
                    CheckInDate = new DateTime(2023, 8, 8),
                    CheckOutDate = new DateTime(2023, 8, 12),
                    PaymentMethod = "UPI",
                    Price = 2000,
                    Email = "user2@gmail.com"
                });
                context.Bookings.Add(new Booking
                {
                    Id = 3,
                    Status = "Confirmed",
                    BookedDate = DateTime.UtcNow,
                    BookingId = "Book003",
                    CheckInDate = new DateTime(2023, 8, 8),
                    CheckOutDate = new DateTime(2023, 8, 12),
                    PaymentMethod = "UPI",
                    Price = 2000,
                    Email = "user3@gmail.com"
                });
                await context.SaveChangesAsync();
            }
            using var BookingContext = new BookingContext(GetDbContextOptions());
            var mock = new Mock<ILogger<Booking>>();
            ILogger<Booking> logger = mock.Object;
            BookingContext.Update(new Booking
            {
                Id = 3,
                Status = "Confirmed",
                BookedDate = DateTime.UtcNow,
                BookingId = "Book003",
                CheckInDate = new DateTime(2023, 8, 8),
                CheckOutDate = new DateTime(2023, 8, 12),
                PaymentMethod = "Cash On Delivery",
                Price = 2000,
                Email = "user3@gmail.com"
            });
            IQueryRepo<Booking, string> repo = new BookingQueryRepo(BookingContext, logger);
            var data = await repo.Get("user3@gmail.com");
            Assert.AreEqual(data.PaymentMethod, "Cash On Delivery");
        }

        [TestMethod]
        public async Task Add_BookingDetail_Returns_BookingDetail()
        {
            using (var context = new BookingContext(GetDbContextOptions()))
            {
                context.Packages.Add(new PackageBooking
                {
                    Id = 1,
                    PackageId = "Pack001",
                    BookingId = "Book001",
                    NoofAdults = 2,
                    NoofChildren = 1

                });
                await context.SaveChangesAsync();
            }
            using var userContext = new BookingContext(GetDbContextOptions());
            var mock = new Mock<ILogger<PackageBooking>>();
            ILogger<PackageBooking> logger = mock.Object;
            IQueryRepo<PackageBooking, string> repo = new PackageQueryRepo(userContext, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(1, data.Count);
        }


        [TestMethod]
        public async Task GetAll_PackageBooking_Returns_PackageBooking()
        {
            using (var context = new BookingContext(GetDbContextOptions()))
            {
                context.Packages.Add(new PackageBooking
                {
                    Id = 1,
                    PackageId = "Pack001",
                    BookingId = "Book001",
                    NoofAdults = 2,
                    NoofChildren = 1

                });
                await context.SaveChangesAsync();
            }
            using BookingContext userContext = new(GetDbContextOptions());
            var mock = new Mock<ILogger<PackageBooking>>();
            ILogger<PackageBooking> logger = mock.Object;
            IQueryRepo<PackageBooking, string> repo = new PackageQueryRepo(userContext, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(1, data.Count);
        }

        [TestMethod]
        public async Task Get_PackageBooking_Returns_PackageBooking()
        {
            using (var context = new BookingContext(GetDbContextOptions()))
            {
                context.Packages.Add(new PackageBooking
                {
                    Id = 1,
                    PackageId = "Pack001",
                    BookingId = "Book001",
                    NoofAdults = 2,
                    NoofChildren = 1

                });
                await context.SaveChangesAsync();
            }
            using var userContext = new BookingContext(GetDbContextOptions());
            var mock = new Mock<ILogger<PackageBooking>>();
            ILogger<PackageBooking> logger = mock.Object;
            IQueryRepo<PackageBooking, string> repo = new PackageQueryRepo(userContext, logger);
            var data = await repo.Get("Pack001");
            Assert.AreEqual(data.NoofAdults, 2);
        }

        [TestMethod]
        public async Task Update_PackageBooking_Returns_PackageBooking()
        {
            using (var context = new BookingContext(GetDbContextOptions()))
            {
                context.Packages.Add(new PackageBooking
                {
                    Id = 1,
                    PackageId = "Pack001",
                    BookingId = "Book001",
                    NoofAdults = 2,
                    NoofChildren = 1

                });
                await context.SaveChangesAsync();
            }
            using var userContext = new BookingContext(GetDbContextOptions());
            var mock = new Mock<ILogger<PackageBooking>>();
            ILogger<PackageBooking> logger = mock.Object;
            userContext.Update(new PackageBooking
            {
                Id = 1,
                PackageId = "Pack001",
                BookingId = "Book001",
                NoofAdults = 2,
                NoofChildren = 2
            });
            IQueryRepo<PackageBooking, string> repo = new PackageQueryRepo(userContext, logger);
            var data = await repo.Get("Pack001");
            Assert.AreEqual(data.NoofChildren, 2);
        }

        [TestMethod]
        public async Task Add_Customer_Returns_Customer()
        {
            using (var context = new BookingContext(GetDbContextOptions()))
            {
                context.Customers.Add(new Customer
                {
                    Id = 1,
                    CustomerId = "C001",
                    CustomerAge = "22" ,
                    CustomerGender = "Male",
                    CustomerName = "User1",
                    CustomerStatus = "Confirmed"

                });
                await context.SaveChangesAsync();
            }
            using var userContext = new BookingContext(GetDbContextOptions());
            var mock = new Mock<ILogger<Customer>>();
            ILogger<Customer> logger = mock.Object;
            IQueryRepo<Customer, string> repo = new CustomerQueryRepo(userContext, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(1, data.Count);
        }


        [TestMethod]
        public async Task GetAll_Customer_Returns_Customers()
        {
            using (var context = new BookingContext(GetDbContextOptions()))
            {
                context.Customers.Add(new Customer
                {
                    Id = 1,
                    CustomerId = "C001",
                    CustomerAge = "22",
                    CustomerGender = "Male",
                    CustomerName = "User1",
                    CustomerStatus = "Confirmed"

                });
                await context.SaveChangesAsync();
            }
            using BookingContext userContext = new(GetDbContextOptions());
            var mock = new Mock<ILogger<Customer>>();
            ILogger<Customer> logger = mock.Object;
            IQueryRepo<Customer, string> repo = new CustomerQueryRepo(userContext, logger);
            var data = await repo.GetAll();
            Assert.AreEqual(1, data.Count);
        }

        [TestMethod]
        public async Task Get_Customer_Returns_Customer()
        {
            using (var context = new BookingContext(GetDbContextOptions()))
            {
                context.Customers.Add(new Customer
                {
                    Id = 1,
                    CustomerId = "C001",
                    CustomerAge = "22",
                    CustomerGender = "Male",
                    CustomerName = "User1",
                    CustomerStatus = "Confirmed"

                });
                await context.SaveChangesAsync();
            }
            using var userContext = new BookingContext(GetDbContextOptions());
            var mock = new Mock<ILogger<Customer>>();
            ILogger<Customer> logger = mock.Object;
            IQueryRepo<Customer, string> repo = new CustomerQueryRepo(userContext, logger);
            var data = await repo.Get("C001");
            Assert.AreEqual(data.CustomerAge, "22");
        }

        [TestMethod]
        public async Task Update_Customer_Returns_Customer()
        {
            using (var context = new BookingContext(GetDbContextOptions()))
            {
                context.Customers.Add(new Customer
                {
                    Id = 1,
                    CustomerId = "C001",
                    CustomerAge = "22",
                    CustomerGender = "Male",
                    CustomerName = "User1",
                    CustomerStatus = "Confirmed"

                });
                await context.SaveChangesAsync();
            }
            using var userContext = new BookingContext(GetDbContextOptions());
            var mock = new Mock<ILogger<Customer>>();
            ILogger<Customer> logger = mock.Object;
            userContext.Update(new Customer
            {
                Id = 1,
                CustomerId = "C001",
                CustomerAge = "25",
                CustomerGender = "Male",
                CustomerName = "User1",
                CustomerStatus = "Confirmed"

            });
            IQueryRepo<Customer, string> repo = new CustomerQueryRepo(userContext, logger);
            var data = await repo.Get("C001");
            Assert.AreEqual(data.CustomerAge, "25");
        }
    }
}