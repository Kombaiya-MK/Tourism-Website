using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public class UserDetailsCommandsRepo : ICommandRepo<UserDetails, string>
    {
        private readonly UserManagementContext _context;
        private readonly ILogger<UserDetails> _logger;

        public UserDetailsCommandsRepo(UserManagementContext context, ILogger<UserDetails> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add UserDetails 
        public async Task<UserDetails> Add(UserDetails item)
        {
            var transaction = _context.Database.BeginTransaction();
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Travel Agent Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var userdetails = _context.Details.Add(item);
            if (userdetails == null)
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


        //Update UserDetails
        public async Task<UserDetails> Update(UserDetails item)
        {
            var transaction = _context.Database.BeginTransaction();
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Travel Agent Object is null");
            }

            var userdetails = await _context.Details.FirstOrDefaultAsync(x => x.Email == item.Email)
                ?? throw new EmptyValueException("Invalid Object!!! No such user Exist!!");

            if (item != null)
            {
                userdetails.UserName = item.UserName ?? userdetails.UserName;
                userdetails.PhoneNumber = item.PhoneNumber ?? userdetails.PhoneNumber;
                userdetails.Address = item.Address ?? userdetails.Address;
                userdetails.DateofBirth = item.DateofBirth ?? userdetails.DateofBirth;
                userdetails.Gender = item.Gender ?? userdetails.Gender;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return userdetails;
            throw new UnableToUpdateException("Unable to update the travel agent");
        }
    }
}
