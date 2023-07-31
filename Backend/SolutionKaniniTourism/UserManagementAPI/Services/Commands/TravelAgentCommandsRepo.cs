using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public class TravelAgentCommandsRepo : ICommandRepo<TravelAgent, string>
    {
        private readonly UserManagementContext _context;
        private readonly ILogger<TravelAgent> _logger;

        public TravelAgentCommandsRepo(UserManagementContext context , ILogger<TravelAgent> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add TravelAgent 
        public async Task<TravelAgent> Add(TravelAgent item)
        {
            var transaction = _context.Database.BeginTransaction();
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Travel Agent Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var travelagent = _context.TravelAgents.Add(item);
            if (travelagent == null)
            {
                _logger.LogError("Unable to add object");
                await transaction.RollbackAsync();
                throw new UnableToAddException("Unable To Add Travel Service");
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation("Travel Service Added Successfully");
            throw new UnableToAddException("Unable To Add Travel Service");
        }


        //Update TravelAgent
        public async Task<TravelAgent> Update(TravelAgent item)
        {
            var transaction = _context.Database.BeginTransaction();
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Travel Agent Object is null");
            }
            var travelagent = new TravelAgent();
            travelagent = await _context.TravelAgents.FirstOrDefaultAsync(x => x.Email == item.Email);
            if (travelagent == null)
                throw new EmptyValueException("Invalid Object!!! No such user Exist!!");
            if(item != null)
            {
                travelagent.AgencyEmail = item.AgencyEmail ?? travelagent.AgencyEmail;
                travelagent.AgencyPhone = item.AgencyPhone ?? travelagent.AgencyPhone;
                travelagent.AgencyName = item.AgencyName ?? travelagent.AgencyName;
                travelagent.AgencyAddress = item.AgencyAddress ?? travelagent.AgencyAddress;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return travelagent;
            throw new UnableToUpdateException("Unable to update the travel agent");
        }
    }
}
