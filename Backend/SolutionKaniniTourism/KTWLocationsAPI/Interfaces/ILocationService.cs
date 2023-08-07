using KTWLocationsAPI.Models;
using KTWLocationsAPI.Models.DTO;

namespace KTWLocationsAPI.Interfaces
{
    public interface ILocationService
    {
        Task<bool> AddLocation(LocationDTO locationDTO);
        Task<bool> RemoveLocation(LocationStatusDTO locationDTO);
        Task<ICollection<Location>> GetAllLocations();

        Task<ICollection<Location>> GetAllNearByLocations(string location);

        Task<bool> AddSpeciality(LocationSpecialityDTO locationSpeciality);

        Task<Image> AddImage(ImageDTO img);
        Task<ICollection<Image>> GetImages(string location);

        Task<ICollection<Speciality>> GetSpecialities(string location);
    }
}
