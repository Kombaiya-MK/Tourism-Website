using KTWTourPackages.Interfaces;
using KTWTourPackages.Models;
using KTWTourPackages.Models.DTO;

namespace KTWTourPackages.Services
{
    public class PackageService : ITourPackService
    {
        public Task<ItineraryItem> AddItinerary(ItineraryItem item)
        {
            throw new NotImplementedException();
        }

        public Task<TourPackage> AddPackage(PackageDTO package)
        {
            throw new NotImplementedException();
        }

        public Task<ItineraryItem> DeleteItinerary(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TourPackage> DeletePackage(UpdatePackStatusDTO package)
        {
            throw new NotImplementedException();
        }

        public Task<TourPackage> GetPackage(string packageId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TourPackage>> GetTourPackages()
        {
            throw new NotImplementedException();
        }

        public Task<ItineraryItem> UpdateItinerary(ItineraryItem item)
        {
            throw new NotImplementedException();
        }

        public Task<TourPackage> UpdatePackage(UpdatePackDTO package)
        {
            throw new NotImplementedException();
        }
    }
}
