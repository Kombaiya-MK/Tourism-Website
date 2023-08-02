using KTWBookingAPI.Interfaces;
using KTWBookingAPI.Models;

namespace KTWBookingAPI.Services
{
    public class BookingService : IBookingService
    {
        public BookingService()
        {
            
        }
        public Task<bool> ApplyTourPackageDiscount(int packageBookingId, string discountCode)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> CalculateTourPackagePrice(int packageBookingId, int numberOfParticipants)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBooking(int bookingId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateBookingConfirmation(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Booking>> GetAllBookings()
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetBookingById(int bookingId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HandleTourPackageCancellation(int packageBookingId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePackageBooking(PackageBooking packageBooking)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateTourPackageBooking(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
