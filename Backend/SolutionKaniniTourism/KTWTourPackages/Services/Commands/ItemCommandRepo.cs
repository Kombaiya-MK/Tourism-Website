using KTWTourPackages.Interfaces;
using KTWTourPackages.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWTourPackages.Services.Commands
{
    public class ItemCommandRepo : ICommandRepo<ItineraryItem , string>
    {
        private readonly PackageContext _context;
        private readonly ILogger<ItineraryItem> _logger;

        public ItemCommandRepo(PackageContext context, ILogger<ItineraryItem> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add ItineraryItem 
        public async Task<ItineraryItem> Add(ItineraryItem item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("ItineraryItem Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var ItineraryItem = _context.TourariesItem.Add(item);
            if (ItineraryItem == null)
            {
                _logger.LogError("Unable to add object");
                throw new UnableToAddException("Unable To Add ItineraryItem");
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation("ItineraryItem Added Successfully");
            return item;
            throw new UnableToAddException("Unable To Add ItineraryItem");
        }


        //Update ItineraryItem
        public async Task<ItineraryItem> Update(ItineraryItem item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("ItineraryItem Object is null");
            }
            var ItineraryItem = new ItineraryItem();
            ItineraryItem = await _context.TourariesItem.FirstOrDefaultAsync(x => x.ItineraryId == item.ItineraryId);
            if (ItineraryItem == null)
                throw new EmptyValueException("Invalid Object!!! No such ItineraryItem Exist!!");
            if (item != null)
            {
                ItineraryItem.Activity = item.Activity ?? ItineraryItem.Activity;
                ItineraryItem.StartTime = item.StartTime ?? ItineraryItem.StartTime;
                ItineraryItem.EndTime = item.EndTime ?? ItineraryItem.EndTime;
                ItineraryItem.Location = item.Location ?? ItineraryItem.Location;
                ItineraryItem.DayNumber = ItineraryItem.DayNumber == 0 ? item.DayNumber : ItineraryItem.DayNumber;
                await _context.SaveChangesAsync();
            }
            return ItineraryItem;
            throw new UnableToUpdateException("Unable to update the travel agent");
        }
    }
}
