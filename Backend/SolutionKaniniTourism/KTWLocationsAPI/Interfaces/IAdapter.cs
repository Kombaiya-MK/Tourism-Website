using KTWLocationsAPI.Models;
using KTWLocationsAPI.Models.DTO;

namespace KTWLocationsAPI.Interfaces
{
    public interface IAdapter
    {
        Location DTOtoLocationAdapter(LocationDTO locationDTO , int count);
    }
}
