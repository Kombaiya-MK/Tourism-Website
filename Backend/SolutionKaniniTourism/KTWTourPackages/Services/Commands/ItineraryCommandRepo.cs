using KTWTourPackages.Interfaces;
using KTWTourPackages.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWTourPackages.Services.Commands
{
    public class ItineraryCommandRepo : ICommandRepo<Itinerary, string>
    {
        private readonly PackageContext _context;
        private readonly ILogger<Itinerary> _logger;

        public ItineraryCommandRepo(PackageContext context, ILogger<Itinerary> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add Itinerary 
        public async Task<Itinerary> Add(Itinerary item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Itinerary Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var Itinerary = _context.Touraries.Add(item);
            if (Itinerary == null)
            {
                _logger.LogError("Unable to add object");
                throw new UnableToAddException("Unable To Add Itinerary");
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation("Itinerary Added Successfully");
            return item;
            throw new UnableToAddException("Unable To Add Itinerary");
        }


        //Update Itinerary
        public async Task<Itinerary> Update(Itinerary item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Itinerary Object is null");
            }
            var Itinerary = new Itinerary();
            Itinerary = await _context.Touraries.FirstOrDefaultAsync(x => x.ItineraryId == item.ItineraryId);
            if (Itinerary == null)
                throw new EmptyValueException("Invalid Object!!! No such Itinerary Exist!!");
            if (item != null)
            {
                Itinerary.Name = item.Name ?? Itinerary.Name;
                Itinerary.Description = item.Description ?? Itinerary.Description;
                Itinerary.PackId = item.PackId ?? Itinerary.PackId;
                await _context.SaveChangesAsync();
            }
            return Itinerary;
            throw new UnableToUpdateException("Unable to update the travel agent");
        }
    }
}
