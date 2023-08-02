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
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("User details Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var userdetails = _context.Details.Add(item);
            if (userdetails == null)
            {
                _logger.LogError("Unable to add object");
                throw new UnableToAddException("Unable To Add User details");
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation("Travel Service Added Successfully");
            throw new UnableToAddException("Unable To Add User details");
        }


        //Update UserDetails
        public async Task<UserDetails> Update(UserDetails item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("User details Object is null");
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
            }
            return userdetails;
            throw new UnableToUpdateException("Unable to update the User details");
        }
    }
}
