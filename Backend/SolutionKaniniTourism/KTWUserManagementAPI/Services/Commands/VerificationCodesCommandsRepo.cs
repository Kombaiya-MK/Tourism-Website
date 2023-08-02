using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services.Commands
{
    public class VerificationCodesCommandsRepo : ICommandRepo<VerificationCodes , string>
    {
        private readonly UserManagementContext _context;
        private readonly ILogger<VerificationCodes> _logger;

        public VerificationCodesCommandsRepo(UserManagementContext context, ILogger<VerificationCodes> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add VerificationCodes 
        public async Task<VerificationCodes> Add(VerificationCodes item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Codes Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var VerificationCodes = _context.VerificationCodes.Add(item);
            if (VerificationCodes == null)
            {
                _logger.LogError("Unable to add object");
                throw new UnableToAddException("Unable To Add Codes");
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation("Travel Service Added Successfully");
            throw new UnableToAddException("Unable To Add Codes");
        }


        //Update VerificationCodes
        public async Task<VerificationCodes> Update(VerificationCodes item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Codes Object is null");
            }
            var VerificationCodes = new VerificationCodes();
            VerificationCodes = await _context.VerificationCodes.FirstOrDefaultAsync(x => x.Email == item.Email);
            if (VerificationCodes == null)
                throw new EmptyValueException("Invalid Object!!! No such user Exist!!");
            if (item != null)
            {
                VerificationCodes.Codes = item.Codes !=0 ? item.Codes : VerificationCodes.Codes;
                await _context.SaveChangesAsync();
            }
            return VerificationCodes;
            throw new UnableToUpdateException("Unable to update the Codes");
        }
    }
}
