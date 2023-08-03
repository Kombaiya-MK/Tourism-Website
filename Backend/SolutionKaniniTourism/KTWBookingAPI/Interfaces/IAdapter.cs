using KTWBookingAPI.Models;
using KTWBookingAPI.Models.DTO;

namespace KTWBookingAPI.Interfaces
{
    public interface IAdapter
    {
        Booking BookingDTOtoBookingAdapter(BookingDTO bookingDTO);
        Booking UpdateBookingDTOtoBookingAdapter(UpdateBookingDTO bookingDTO);
        Customer CustomerDTOtoCustomer(CustomerDTO customerDTO);
        PackageBooking UpdateBookingDTOtoPackageBooking(UpdateBookingDTO updateBookingDTO);
    }
}
