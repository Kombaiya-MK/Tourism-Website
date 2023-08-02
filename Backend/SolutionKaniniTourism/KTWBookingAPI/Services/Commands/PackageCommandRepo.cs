using KTWBookingAPI.Interfaces;
using KTWBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWBookingAPI.Services.Commands
{
    public class PackageCommandRepo : ICommandRepo<PackageBooking, string>
    {
        private readonly BookingContext _context;
        private readonly ILogger<PackageBooking> _logger;

        public PackageCommandRepo(BookingContext context, ILogger<PackageBooking> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add PackageBooking 
        public async Task<PackageBooking> Add(PackageBooking item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Package Booking Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var PackageBooking = _context.Packages.Add(item);
            if (PackageBooking == null)
            {
                _logger.LogError("Unable to add object");
                throw new UnableToAddException("Unable To Add Package Booking Service");
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation("Package Booking Added Successfully");
            throw new UnableToAddException("Unable To Add Package Booking");
        }


        //Update PackageBooking
        public async Task<PackageBooking> Update(PackageBooking item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("PackageBooking Object is null");
            }
            var packageBooking = new PackageBooking();
            packageBooking = await _context.Packages.FirstOrDefaultAsync(x => x.PackageId == item.PackageId);
            if (packageBooking == null)
                throw new EmptyValueException("Invalid Object!!! No such user Exist!!");
            if (item != null)
            {
                packageBooking.BookingId = item.BookingId ?? packageBooking.BookingId;
                packageBooking.NoofChildren = item.NoofChildren == 0 ? packageBooking.NoofChildren : item.NoofChildren;
                packageBooking.NoofAdults = item.NoofAdults == 0 ? packageBooking.NoofAdults : item.NoofAdults;
                await _context.SaveChangesAsync();
            }
            return packageBooking;
            throw new UnableToUpdateException("Unable to update the travel agent");
        }
    }
}
