using KTWLocationsAPI.Interfaces;
using KTWLocationsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWLocationsAPI.Services.Commands
{
    public class SpecialityCommandRepo : ICommandRepo<Speciality , string>
    {
        private readonly LocationContext _context;
        private readonly ILogger<Speciality> _logger;

        public SpecialityCommandRepo(LocationContext context, ILogger<Speciality> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add Speciality 
        public async Task<Speciality> Add(Speciality item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Speciality Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var Speciality = _context.Specializations.Add(item);
            if (Speciality == null)
            {
                _logger.LogError("Unable to add object");
                throw new UnableToAddException("Unable To Add Speciality");
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation("Speciality Added Successfully");
            throw new UnableToAddException("Unable To Add Speciality");
        }


        //Update Speciality
        public async Task<Speciality> Update(Speciality item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Speciality Object is null");
            }
            var Speciality = new Speciality();
            Speciality = await _context.Specializations.FirstOrDefaultAsync(x => x.LocationId == item.LocationId);
            if (Speciality == null)
                throw new EmptyValueException("Invalid Object!!! No such Speciality Exist!!");
            if (item != null)
            {
                Speciality.Location = item.Location ?? Speciality.Location;
                Speciality.Special = item.Special ?? Speciality.Special;
                Speciality.Description = item.Special ?? Speciality.Special;
                await _context.SaveChangesAsync();
            }
            return Speciality;
            throw new UnableToUpdateException("Unable to update the travel agent");
        }
    }
}
