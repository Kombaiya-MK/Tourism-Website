using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;

namespace UserManagementAPI.Services.Commands
{
    public class UserCommandsRepo : ICommandRepo<User, string>
    {
        private readonly UserManagementContext _context;
        private readonly ILogger<User> _logger;

        public UserCommandsRepo(UserManagementContext context, ILogger<User> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add User 
        public async Task<User> Add(User item)
        {
            var transaction = _context.Database.BeginTransaction();
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Travel Agent Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var user = _context.Users.Add(item);
            if (user == null)
            {
                _logger.LogError("Unable to add object");
                await transaction.RollbackAsync();
                throw new UnableToAddException("Unable To Add Travel Service");
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation("Travel Service Added Successfully");
            return item;
            throw new UnableToAddException("Unable To Add Travel Service");
        }


        //Update User
        public async Task<User> Update(User item)
        {
            var transaction = _context.Database.BeginTransaction();
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("user Object is null");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == item.Email)
                ?? throw new EmptyValueException("Invalid Object!!! No such user Exist!!");

            if (item != null)
            {
                user.HashKey = item.HashKey ?? user.HashKey;
                user.Password = item.Password ?? user.Password;
                user.Role = item.Role ?? user.Role;
                user.Status = item.Status ?? user.Status;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return user;
            throw new UnableToUpdateException("Unable to update the user");
        }
    }
}
