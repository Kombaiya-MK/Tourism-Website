using KTWLocationsAPI.Interfaces;
using KTWLocationsAPI.Models;
using KTWLocationsAPI.Models.DTO;

namespace KTWLocationsAPI.Utilities.Adapters
{
    public class LocationAdapter : IAdapter
    {
        public Location DTOtoLocationAdapter(LocationDTO locationDTO , int count)
        {
            Location location = new()
            {
                LocationId = "LOC00" + count.ToString(),
                Name = locationDTO.Location,
                Latitiude = locationDTO.Latitude,
                Longitude = locationDTO.Longitude,
                Description = locationDTO.Description,
                Status = "Active",
            };
            return location;
        }
    }
}
