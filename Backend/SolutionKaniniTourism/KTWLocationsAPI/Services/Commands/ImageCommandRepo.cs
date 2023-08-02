using KTWBookingAPI.Interfaces;
using KTWLocationsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWLocationsAPI.Services.Commands
{
    public class ImageCommandRepo : ICommandRepo<Image, string>
    {
        private readonly LocationContext _context;
        private readonly ILogger<Image> _logger;

        public ImageCommandRepo(LocationContext context, ILogger<Image> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add PackageBooking 
        public async Task<Image> Add(Image item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Image Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var PackageBooking = _context.Images.Add(item);
            if (PackageBooking == null)
            {
                _logger.LogError("Unable to add object");
                throw new UnableToAddException("Unable To Add Images");
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation("Image Added Successfully");
            throw new UnableToAddException("Unable To Add Images");
        }


        //Update PackageBooking
        public async Task<Image> Update(Image item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("PackageBooking Object is null");
            }
            var image = new Image();
            image = await _context.Images.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (image == null)
                throw new EmptyValueException("Invalid Object!!! No such Image Exist!!");
            if (item != null)
            {
                image.Picture = item.Picture ?? image.Picture;
                await _context.SaveChangesAsync();
            }
            return image;
            throw new UnableToUpdateException("Unable to update the Image");
        }
    }
}
