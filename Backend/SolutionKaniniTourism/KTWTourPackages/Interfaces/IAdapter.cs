using KTWTourPackages.Models.DTO;
using KTWTourPackages.Models;

namespace KTWTourPackages.Interfaces
{
    public interface IAdapter
    {
        TourPackage DTOtoPackAdapter(PackageDTO package , int count);
        ItineraryItem DTOtoItineraryItemAdapter(ItemDTO item , int count);
        Itinerary DTOtoItineraryAdapter(ItemDTO item , int count);
    }
}
