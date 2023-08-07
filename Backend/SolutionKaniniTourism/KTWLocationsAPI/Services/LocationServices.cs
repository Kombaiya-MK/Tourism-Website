using KTWLocationsAPI.Interfaces;
using KTWLocationsAPI.Models;
using KTWLocationsAPI.Models.DTO;
using KTWLocationsAPI.Utilities.CustomExceptions;

namespace KTWLocationsAPI.Services
{
    public class LocationServices : ILocationService
    {
        private readonly ICommandRepo<Location, string> _cmdLocRepo;
        private readonly ICommandRepo<Image, string> _cmdImageRepo;
        private readonly ICommandRepo<Speciality, string> _cmdSpecRepo;
        private readonly IQueryRepo<Location, string> _qryLocRepo;
        private readonly IQueryRepo<Image, string> _qryImageRepo;
        private readonly IQueryRepo<Speciality, string> _qrySplRepo;
        private readonly IAdapter _adapter;

        public LocationServices(ICommandRepo<Location , string> cmdLocRepo , ICommandRepo<Image, string> cmdImageRepo , 
            ICommandRepo<Speciality, string> cmdSpecialityRepo , IQueryRepo<Location , string> queryLocRepo , 
            IQueryRepo<Image , string> queryImageRepo , IQueryRepo<Speciality , string> querySpecialityRepo,
            IAdapter adapter
            )
        {
            _cmdLocRepo = cmdLocRepo;
            _cmdImageRepo = cmdImageRepo;
            _cmdSpecRepo = cmdSpecialityRepo;
            _qryLocRepo = queryLocRepo;
            _qryImageRepo = queryImageRepo;
            _qrySplRepo = querySpecialityRepo;
            _adapter = adapter;
        }
        public async Task<bool> AddLocation(LocationDTO locationDTO)
        {
            int count = GetLocationCount();
            bool result = false;
            var loc = _adapter.DTOtoLocationAdapter(locationDTO , count+1);
            var loc1 = await _cmdLocRepo.Add(loc);
            if(loc1 != null)
            {
                result = true;
            }
            return result;
        }

        public async Task<bool> AddSpeciality(LocationSpecialityDTO locationSpeciality)
        {
            Speciality speciality = new()
            {
                LocationId = locationSpeciality.LocationId,
                Special = locationSpeciality.Speciality,
                Description = locationSpeciality.Description,
            };
            var spl = await _cmdSpecRepo.Add(speciality);
            if( spl != null )
            {
                return true;
            }
            return false;

        }

        public async Task<ICollection<Location>> GetAllLocations()
        {
            return await _qryLocRepo.GetAll();
        }

        public async Task<ICollection<Location>> GetAllNearByLocations(string location)
        {
            var locs = await GetAllLocations();
            var loc = locs.FirstOrDefault(locs => locs.LocationId == location) ?? throw new NoValueException("Invalid Location");
            List<Location> nearbyloc = new();
            foreach ( var loc1 in locs )
            {
                NearByLocDTO obj = new()
                {
                    latitude1 = loc.Latitiude,
                    latitude2 = loc1.Latitiude,
                    longitude1 = loc.Longitude,
                    longitude2 = loc1.Longitude,
                };
                if(CalculateDistance(obj) <= 10)
                    nearbyloc.Add(loc1);
            }
            return nearbyloc;
        }

        public async Task<bool> RemoveLocation(LocationStatusDTO locationDTO)
        {
            Location location = new()
            {
                LocationId = locationDTO.LocationId,
                Status = locationDTO.Status,
            };
             var loc = await _cmdLocRepo.Update(location);
            if(loc != null)
            {
                return true;
            }
            return false;
        }

        private int GetLocationCount()
        {
            var locs = GetAllLocations();
            return locs.Result.Count;
        }


        //Calculate Distance between cities using latitude and longitude
        private static double CalculateDistance(NearByLocDTO locDTO)
        {
            double lat1Radians = ToRadians(Convert.ToDouble(locDTO.latitude1));
            double lon1Radians = ToRadians(Convert.ToDouble(locDTO.longitude1));
            double lat2Radians = ToRadians(Convert.ToDouble(locDTO.latitude2));
            double lon2Radians = ToRadians(Convert.ToDouble(locDTO.longitude2));

            double deltaLat = lat2Radians - lat1Radians;
            double deltaLon = lon2Radians - lon1Radians;

            double a = Math.Pow(Math.Sin(deltaLat / 2.0), 2) +
                       Math.Cos(lat1Radians) * Math.Cos(lat2Radians) * Math.Pow(Math.Sin(deltaLon / 2.0), 2);

            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = 6371.0 * c;
            return distance;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        public async Task<Image> AddImage(ImageDTO img)
        {
            Image image = new Image()
            {
                Picture = img.Picture,
                LocationId = img.LocationId,
            };
            return await _cmdImageRepo.Add(image);
        }

        public async Task<ICollection<Image>> GetImages(string location)
        {
            var images = await _qryImageRepo.GetAll();
            return images.Where(x => x.LocationId == location).ToList();
        }

        public async Task<ICollection<Speciality>> GetSpecialities(string location)
        {
            var specialities = await _qrySplRepo.GetAll();
            return specialities.Where(x=>x.LocationId == location).ToList();
        }
    }
}
