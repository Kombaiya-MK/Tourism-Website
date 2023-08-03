using KTWBookingAPI.Interfaces;
using KTWBookingAPI.Models;
using KTWBookingAPI.Models.DTO;
using KTWBookingAPI.Utilities.CustomExceptions;

namespace KTWBookingAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly ICommandRepo<Booking, string> _cmdBookRepo;
        private readonly ICommandRepo<PackageBooking, string> _cmdPackRepo;
        private readonly ICommandRepo<Customer, string> _cmdCustomerRepo;
        private readonly IQueryRepo<Booking, string> _qryBookRepo;
        private readonly IQueryRepo<PackageBooking, string> _qryPackRepo;
        private readonly IQueryRepo<Customer, string> _qryCustomerRepo;
        private readonly IAdapter _adapter;

        public BookingService(ICommandRepo<Booking, string> cmdBookRepo, ICommandRepo<PackageBooking, string> cmdPackRepo,
            ICommandRepo<Customer, string> cmdCustomerRepo, IQueryRepo<Booking, string> querybookRepo,
            IQueryRepo<PackageBooking, string> queryPackRepo, IQueryRepo<Customer, string> queryCustomerRepo,
            IAdapter adapter)
        {
            _cmdBookRepo = cmdBookRepo;
            _cmdPackRepo = cmdPackRepo;
            _cmdCustomerRepo = cmdCustomerRepo;
            _qryBookRepo = querybookRepo;
            _qryPackRepo = queryPackRepo;
            _qryCustomerRepo = queryCustomerRepo;
            _adapter = adapter;
        }

        public async Task<Customer> AddCustomer(CustomerDTO customer)
        {
            customer.CustomerId = "JPNTRCUST00" + GetCustomerCount().ToString();
            var customer1 = _adapter.CustomerDTOtoCustomer(customer) ??
                throw new EmptyValueException("Adapter returned null for Customer object");
            var result = await _cmdCustomerRepo.Add(customer1) ??
                throw new UnableToAddException("Unable to add Customer");
            return result;
        }

        public Task<double> CalculateTourPackagePrice(GeneratePriceDTO generatePriceDTO)
        {
            var price = generatePriceDTO.Price * generatePriceDTO.Quantity;
            var discount = (generatePriceDTO.Discount * generatePriceDTO.Price) / 100;
            var gst = (10 * generatePriceDTO.Price) / 100;
            Task<double> TotalPrice = Task.FromResult((price + discount + gst));
            return TotalPrice;
        }

        public async Task<bool> CreateBooking(BookingDTO booking)
        {
            booking.BookingId = "JPNTRPK00" + GetBookingsCount().ToString();
            var book = _adapter.BookingDTOtoBookingAdapter(booking) ??
                throw new EmptyValueException("Adapter returned null for Book object");
            PackageBooking pack = new()
            {
                PackageId = "JPNBOOKPACK00" + GetPackageBookingCount().ToString(),
                NoofAdults = booking.NoofAdults,
                NoofChildren = booking.NoofChildren,
                BookingId = booking.BookingId,
            };
            var result = await _cmdBookRepo.Add(book) ??
                throw new UnableToAddException("Unable to add Booking");
            _ = await _cmdPackRepo.Add(pack) ??
                throw new UnableToAddException("Unable to add Package");
            if (result != null)
            {
                return true;
            }
            return false;
        }
         
        public Task<BillDTO> GenerateBookingConfirmation(UpdateStatusDTO updateStatusDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Booking>> GetAllBookings()
        {
           return await _qryBookRepo.GetAll();
        }

        public async Task<ICollection<Booking>> GetBookingById(string UserId)
        {
            var bookings = await _qryBookRepo.GetAll();
            return bookings.Where(x=>x.Email?.ToLower() == UserId?.ToLower()).ToList();
        }

        public async Task<bool> HandleTourPackageCancellation(CancelPackDTO cancelPackDTO)
        {
            Booking booking = new()
            {
                BookingId = cancelPackDTO.BookingId,
                Status = cancelPackDTO.Status,
            };
            var result = await _cmdBookRepo.Update(booking)
                ?? throw new UnableToUpdateException("Unable To Update Booking Details");
            if (result != null)
            {
                return true;
            }
            return false;
        }

        public async Task<Customer> RemoveCustomer(CustomerDTO customer)
        {
            Customer customer1 = _adapter.CustomerDTOtoCustomer(customer) ??
                throw new EmptyValueException("Adapter Thrown Null value for customer Object");
            var result = await _cmdCustomerRepo.Update(customer1) ??
                throw new UnableToUpdateException("Unable to remove customer");
            return result;
        }

        public async Task<bool> UpdateBooking(UpdateBookingDTO updateBookingDTO)
        {
            var book = _adapter.UpdateBookingDTOtoBookingAdapter(updateBookingDTO) ??
                throw new EmptyValueException("Adapter returned null for Book object");
            var result = await _cmdBookRepo.Update(book) ??
                throw new UnableToUpdateException("Unable to Update Booking");
            var packs = await _qryPackRepo.GetAll();
            var pack = packs.FirstOrDefault(x => x.BookingId?.ToLower() == updateBookingDTO?.BookingId?.ToLower()) ??
                throw new EmptyValueException("No pack Available");
            pack.NoofChildren = updateBookingDTO.NoofChildren;
            pack.NoofAdults = updateBookingDTO.NoofAdults;
            var result1 = await UpdatePackageBooking(pack);
            if (result != null && result1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCustomer(CustomerDTO customer)
        {
            Customer customer1 = _adapter.CustomerDTOtoCustomer(customer)
                ?? throw new EmptyValueException("Adapter Thrown Null value for customer Object");
            var result = await _cmdCustomerRepo.Update(customer1) ??
                throw new UnableToUpdateException("Unable to remove customer");
            if (result != null)
                return true;
            return false;
        }

        public async Task<bool> UpdatePackageBooking(PackageBooking packageBooking)
        {
            var result = await _cmdPackRepo.Update(packageBooking) ??
                throw new UnableToUpdateException("Unable to remove customer");
            if (result != null)
                return true;
            return false;
        }

        private int GetBookingsCount()
        {
            return GetAllBookings().Result.Count + 1;
        }
        private int GetCustomerCount()
        {
            return _qryCustomerRepo.GetAll().Result.Count + 1;
        }
        private int GetPackageBookingCount()
        {
            return _qryPackRepo.GetAll().Result.Count + 1;
        }
    }
}
