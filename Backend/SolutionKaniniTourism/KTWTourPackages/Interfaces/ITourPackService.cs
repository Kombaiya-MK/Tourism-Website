using KTWTourPackages.Models;
using KTWTourPackages.Models.DTO;

namespace KTWTourPackages.Interfaces
{
    public interface ITourPackService
    {
        Task<TourPackage> AddPackage(PackageDTO package);
        Task<TourPackage> UpdatePackage(UpdatePackDTO package);
        Task<TourPackage> DeletePackage(UpdatePackStatusDTO package);
        Task<ICollection<TourPackage>> GetTourPackages();
        Task<TourPackage> GetPackage(string packageId);
        Task<ItineraryItem> AddItinerary(ItemDTO item);
        Task<ItineraryItem> UpdateItinerary(ItemDTO item);
        Task<ItineraryItem> DeleteItinerary(UpdateItemDTO item);
        Task<ICollection<Itinerary>> GetItineraries(string packid);

    }
}
