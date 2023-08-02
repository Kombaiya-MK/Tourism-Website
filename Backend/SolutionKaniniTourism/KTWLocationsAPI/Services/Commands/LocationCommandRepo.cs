using KTWBookingAPI.Interfaces;
using KTWLocationsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWLocationsAPI.Services.Commands
{
    public class LocationCommandRepo : ICommandRepo<Location , string>
    {
        private readonly LocationContext _context;
        private readonly ILogger<Location> _logger;

        public LocationCommandRepo(LocationContext context, ILogger<Location> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add User 
        public async Task<Location> Add(Location item)
        {
            var transaction = _context.Database.BeginTransaction();
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Location Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var user = _context.Locations.Add(item);
            if (user == null)
            {
                _logger.LogError("Unable to add object");
                await transaction.RollbackAsync();
                throw new UnableToAddException("Unable To Add Location");
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation("Location Added Successfully");
            return item;
            throw new UnableToAddException("Unable To Add Location");
        }


        //Update User
        public async Task<Location> Update(Location item)
        {
            var transaction = _context.Database.BeginTransaction();
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("location Object is null");
            }

            var location = await _context.Locations.FirstOrDefaultAsync(x => x.LocationId == item.LocationId)
                ?? throw new EmptyValueException("Invalid Object!!! No such location Exist!!");

            if (item != null)
            {
                location.Name = item.Name ?? location.Name;
                location.Description = item.Description ?? location.Description;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return location;
            throw new UnableToUpdateException("Unable to update the location");
        }
    }
}
