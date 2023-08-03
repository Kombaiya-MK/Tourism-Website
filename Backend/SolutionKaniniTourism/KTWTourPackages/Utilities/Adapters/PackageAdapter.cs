using KTWTourPackages.Interfaces;
using KTWTourPackages.Models;
using KTWTourPackages.Models.DTO;

namespace KTWTourPackages.Utilities.Adapters
{
    public class PackageAdapter : IAdapter
    {
        public Itinerary DTOtoItineraryAdapter(ItemDTO item , int count)
        {
            Itinerary itinerary = new()
            {
                ItineraryId = "Itr00" + count.ToString(),
                Name = item.Name,
                Description = item.Description,
                PackId = item.PackId,
            };
            return itinerary;
        }

        public ItineraryItem DTOtoItineraryItemAdapter(ItemDTO item , int count)
        {
            ItineraryItem itineraryItem = new()
            {
                ItemId = "Item00" + count.ToString(),
                DayNumber = item.DayNumber,
                ItineraryId = item.ItineraryId,
                Activity = item.Activity,
                Location = item.Location,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                Status = item.Status,
            };
            return itineraryItem;
        }

        public TourPackage DTOtoPackAdapter(PackageDTO package, int count)
        {
            TourPackage tourPackage = new()
            {
                PackId = "Pack00" + count.ToString(),
                Name = package.Name,
                Description = package.Description,
                Duration = package.Duration,
                Capacity = package.Capacity,
                Price = package.Price,
                Status = package.Status,
                LocationId = package.LocationId,
            };
            return tourPackage;
        }
    }
}
