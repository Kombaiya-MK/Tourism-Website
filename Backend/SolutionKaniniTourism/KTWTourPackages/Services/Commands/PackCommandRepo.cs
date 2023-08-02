using KTWTourPackages.Interfaces;
using KTWTourPackages.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWTourPackages.Services.Commands
{
    public class PackCommandRepo : ICommandRepo<TourPackage , string>
    {
        private readonly PackageContext _context;
        private readonly ILogger<TourPackage> _logger;

        public PackCommandRepo(PackageContext context, ILogger<TourPackage> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add Package 
        public async Task<TourPackage> Add(TourPackage item)
        {
            var transaction = _context.Database.BeginTransaction();
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("TourPackage Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var user = _context.TourPackages.Add(item);
            if (user == null)
            {
                _logger.LogError("Unable to add object");
                await transaction.RollbackAsync();
                throw new UnableToAddException("Unable To Add TourPackage");
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation("TourPackage Added Successfully");
            return item;
            throw new UnableToAddException("Unable To Add TourPackage");
        }


        //Update Package
        public async Task<TourPackage> Update(TourPackage item)
        {
            var transaction = _context.Database.BeginTransaction();
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("TourPackage Object is null");
            }

            var TourPackage = await _context.TourPackages.FirstOrDefaultAsync(x => x.PackId == item.PackId)
                ?? throw new EmptyValueException("Invalid Object!!! No such TourPackage Exist!!");

            if (item != null)
            {
                TourPackage.Price = TourPackage.Price == 0 ? item.Price : TourPackage.Price ;
                TourPackage.Capacity = item.Capacity;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return TourPackage;
            throw new UnableToUpdateException("Unable to update the TourPackage");
        }
    }
}
