using KTWBookingAPI.Models;

namespace KTWBookingAPI.Interfaces
{
    public interface IBookingService
    {
        // Booking functions
        Task<ICollection<Booking>> GetAllBookings();
        Task<Booking> GetBookingById(int bookingId);
        Task<int> CreateBooking(Booking booking);
        Task<bool> UpdateBooking(Booking booking);
        Task<bool> DeleteBooking(int bookingId);
        Task<bool> UpdatePackageBooking(PackageBooking packageBooking);
        Task<bool> UpdateCustomer(Customer customer);
        Task<decimal> CalculateTourPackagePrice(int packageBookingId, int numberOfParticipants);
        Task<bool> ApplyTourPackageDiscount(int packageBookingId, string discountCode);
        Task<bool> ValidateTourPackageBooking(Booking booking);
        Task<string> GenerateBookingConfirmation(Booking booking);
        Task<bool> HandleTourPackageCancellation(int packageBookingId);
    }
}
