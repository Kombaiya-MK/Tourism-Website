using KTWBookingAPI.Models;
using KTWBookingAPI.Models.DTO;

namespace KTWBookingAPI.Interfaces
{
    public interface IBookingService
    {
        // Booking functions
        Task<ICollection<Booking>> GetAllBookings();
        Task<ICollection<Booking>> GetBookingById(string UserId);
        Task<bool> CreateBooking(BookingDTO booking);
        Task<bool> UpdateBooking(UpdateBookingDTO updateBookingDTO);
        Task<bool> UpdatePackageBooking(PackageBooking packageBooking);
        Task<bool> UpdateCustomer(CustomerDTO customer);
        Task<double> CalculateTourPackagePrice(GeneratePriceDTO generatePriceDTO);
        Task<BillDTO> GenerateBookingConfirmation(UpdateStatusDTO updateStatusDTO);
        Task<bool> HandleTourPackageCancellation(CancelPackDTO cancelPackDTO);

        Task<Customer> AddCustomer(CustomerDTO customer);
        Task<Customer> RemoveCustomer(CustomerDTO customer);
    }
}
