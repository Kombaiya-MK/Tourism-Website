using KTWBookingAPI.Interfaces;
using KTWBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWBookingAPI.Services.Commands
{
    public class BookingCommandRepo : ICommandRepo<Booking, string>
    {
        private readonly BookingContext _context;
        private readonly ILogger<Booking> _logger;

        public BookingCommandRepo(BookingContext context, ILogger<Booking> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add Booking 
        public async Task<Booking> Add(Booking item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Booking Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var Booking = _context.Bookings.Add(item);
            if (Booking == null)
            {
                _logger.LogError("Unable to add object");
                throw new UnableToAddException("Unable To Add Booking Service");
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation("Booking Added Successfully");
            throw new UnableToAddException("Unable To Add Booking");
        }


        //Update Booking
        public async Task<Booking> Update(Booking item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Booking Object is null");
            }
            var booking = new Booking();
            booking = await _context.Bookings.FirstOrDefaultAsync(x => x.BookingId == item.BookingId);
            if (booking == null)
                throw new EmptyValueException("Invalid Object!!! No such user Exist!!");
            if (item != null)
            {
                booking.Email = item.Email ?? booking.Email;
                booking.CheckInDate = item.CheckInDate ?? booking.CheckInDate;
                booking.CheckOutDate = item.CheckOutDate ?? booking.CheckOutDate;
                booking.Price = item.Price ?? booking.Price;
                booking.Status = item.Status ?? booking.Status;
                booking.PaymentMethod = item.PaymentMethod ?? booking.PaymentMethod;
                await _context.SaveChangesAsync();
            }
            return booking;
            throw new UnableToUpdateException("Unable to update the travel agent");
        }
    }
}
