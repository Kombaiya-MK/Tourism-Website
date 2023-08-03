using KTWBookingAPI.Interfaces;
using KTWBookingAPI.Models;
using KTWBookingAPI.Models.DTO;

namespace KTWBookingAPI.Utilities.Adapters
{
    public class BookingAdapter : IAdapter
    {
        public Booking BookingDTOtoBookingAdapter(BookingDTO bookingDTO)
        {
            Booking booking = new()
            {
                BookingId = bookingDTO.BookingId,
                BookedDate = bookingDTO.BookedDate,
                CheckInDate = bookingDTO.CheckInDate,
                CheckOutDate = bookingDTO.CheckOutDate, 
                PaymentMethod = bookingDTO.PaymentMethod,
                Price = bookingDTO.Price,
                Email = bookingDTO.Email,
                Status = bookingDTO.Status,
            };
            return booking;
        }

        public Customer CustomerDTOtoCustomer(CustomerDTO customerDTO)
        {
            Customer customer = new()
            {
                CustomerId = customerDTO.CustomerId,
                CustomerAge = customerDTO.CustomerAge,
                CustomerGender = customerDTO.CustomerGender,
                CustomerName = customerDTO.CustomerName,
                CustomerStatus = customerDTO.CustomerStatus ?? "Active"
            };
            return customer;
        }

        public Booking UpdateBookingDTOtoBookingAdapter(UpdateBookingDTO bookingDTO)
        {
            Booking booking = new()
            {
                BookingId = bookingDTO.BookingId,
                BookedDate = bookingDTO.BookedDate,
                CheckInDate = bookingDTO.CheckInDate,
                CheckOutDate = bookingDTO.CheckOutDate,
                PaymentMethod = bookingDTO.PaymentMethod,
                Price = bookingDTO.Price,
                Email = bookingDTO.Email,
            };
            return booking;
        }

        public PackageBooking UpdateBookingDTOtoPackageBooking(UpdateBookingDTO updateBookingDTO)
        {
            PackageBooking pack = new()
            {
                NoofAdults = updateBookingDTO.NoofAdults,
                BookingId = updateBookingDTO.BookingId,
                NoofChildren = updateBookingDTO.NoofChildren,
            };
            return pack;
        }

    }
}
