using KTWTourPackages.Interfaces;
using KTWTourPackages.Models;
using KTWTourPackages.Models.DTO;
using KTWTourPackages.Services.Commands;
using System.Data.SqlTypes;

namespace KTWTourPackages.Services
{
    public class PackageService : ITourPackService
    {
        private readonly ICommandRepo<TourPackage, string> _cmdPackRepo;
        private readonly ICommandRepo<Itinerary, string> _cmdItryRepo;
        private readonly ICommandRepo<ItineraryItem, string> _cmdItemRepo;
        private readonly IQueryRepo<TourPackage, string> _qryPackRepo;
        private readonly IQueryRepo<Itinerary, string> _qryItryRepo;
        private readonly IQueryRepo<ItineraryItem, string> _qryItemRepo;
        private readonly IAdapter _adapter;

        public PackageService(ICommandRepo<TourPackage , string> cmdPackRepo , ICommandRepo<Itinerary, string> cmdItinraryRepo,
            ICommandRepo<ItineraryItem, string> cmdItemRepo , IQueryRepo<TourPackage , string> queryPackRepo ,
            IQueryRepo<Itinerary, string> queryItineraryRepo, IQueryRepo<ItineraryItem, string> queryItemRepo,
            IAdapter adapter)
        {
            _cmdPackRepo = cmdPackRepo;
            _cmdItryRepo = cmdItinraryRepo;
            _cmdItemRepo = cmdItemRepo;
            _qryPackRepo = queryPackRepo;
            _qryItryRepo = queryItineraryRepo;
            _qryItemRepo = queryItemRepo;
            _adapter = adapter;
        }
        public async Task<ItineraryItem> AddItinerary(ItemDTO item)
        {
            if(item == null || item.PackId == null || item.ItineraryId == null) 
                throw new EmptyValueException("Given item is null");
            int itrcount = GetItineraryCount(item.PackId);
            int itemcount = await GetItemCount(item.ItineraryId);
            var itinerary = _adapter.DTOtoItineraryAdapter(item, itrcount) ??
                throw new EmptyValueException("Adapter Thrown null for Itinerary");
            var ItineraryItem = _adapter.DTOtoItineraryItemAdapter(item, itemcount) ??
                throw new EmptyValueException("Adapter Thrown null for Itinerary Item");
            _ = await _cmdItryRepo.Add(itinerary) ??
                throw new UnableToAddException("Unable to add Itinerary");
            var resultItem = await _cmdItemRepo.Add(ItineraryItem) ??
                throw new UnableToAddException("Unable to add Itinerary Item");
            return resultItem;

        }

        public async Task<TourPackage> AddPackage(PackageDTO package)
        {
            int count = GetPackageCount();
            var pack = _adapter.DTOtoPackAdapter(package,count) ??
                throw new EmptyValueException("Adapter returned empty pack object");
            var result = await _cmdPackRepo.Add(pack) ??
                throw new UnableToAddException("Unable to package!!");
            return result;
        }

        public async Task<ItineraryItem> DeleteItinerary(UpdateItemDTO item)
        {
            ItineraryItem item1 = new()
            {
                ItineraryId = item.ItineraryId,
                ItemId = item.ItemId,
                Status = "Inactive"
            };
            var result = await _cmdItemRepo.Update(item1) ??
                throw new UnableToUpdateException("Unable to update Itinrary item");
            return result;
        }

        public Task<TourPackage> DeletePackage(UpdatePackStatusDTO package)
        {
            TourPackage package1 = new()
            {
                PackId = package.PackId,
                Status = package.Status,
            };
            var result = _cmdPackRepo.Update(package1) ?? 
                throw new UnableToUpdateException("Unable to updat package" +  package.PackId);
            return result;
        }

        public async Task<ICollection<Itinerary>> GetItineraries(string packid)
        {
            var touraries = await _qryItryRepo.GetAll();
            var filteredtouraries = touraries.Where(x=>x.PackId == packid);
            return filteredtouraries.ToList();
        }

        public async Task<TourPackage> GetPackage(string packageId)
        {
            var pack = await _qryPackRepo.Get(packageId) ??
                throw new EmptyValueException("No such package exist!!" + packageId);
            return pack;
            
        }

        public async Task<ICollection<TourPackage>> GetTourPackages()
        {
            return await _qryPackRepo.GetAll();
        }

        public async Task<ItineraryItem> UpdateItinerary(ItemDTO item)
        {
            if (item == null || item.PackId == null || item.ItineraryId == null)
                throw new EmptyValueException("Given item is null");
            int itrcount = GetItineraryCount(item.PackId);
            int itemcount = await GetItemCount(item.ItineraryId);
            var itinerary = _adapter.DTOtoItineraryAdapter(item, itrcount) ??
                throw new EmptyValueException("Adapter Thrown null for Itinerary");
            var ItineraryItem = _adapter.DTOtoItineraryItemAdapter(item, itemcount) ??
                throw new EmptyValueException("Adapter Thrown null for Itinerary Item");
            _ = await _cmdItryRepo.Update(itinerary) ??
                throw new UnableToUpdateException("Unable to Update Itinerary");
            var resultItem = await _cmdItemRepo.Update(ItineraryItem) ??
                throw new UnableToUpdateException("Unable to Update Itinerary Item");
            return resultItem;
        }

        public async Task<TourPackage> UpdatePackage(UpdatePackDTO package)
        {
            TourPackage pack = new()
            {
                Name = package.Name,
                Description = package.Description,
                Capacity = package.Capacity,
                Duration = package.Duration,
                Price = package.Price
            };
            var result = await _cmdPackRepo.Update(pack) ??
                throw new UnableToAddException("Unable to package!!");
            return result;
        }

        private int GetPackageCount()
        {
            int count =  GetTourPackages().Result.Count;
            return count+1;
        }

        private  int GetItineraryCount(string packid)
        {
            int count = GetItineraries(packid).Result.Count; return count+1;
        }

        private async Task<int> GetItemCount( string itryId)
        {
            var Items = await _qryItemRepo.GetAll();
            var count = Items.Where(x => x.ItineraryId == itryId).ToList().Count + 1;
            return count;
        }
    }
}
