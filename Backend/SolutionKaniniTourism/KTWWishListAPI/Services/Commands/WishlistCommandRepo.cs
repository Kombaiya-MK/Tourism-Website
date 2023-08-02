
using KTWWishListAPI.Interfaces;
using KTWWishListAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWWishListAPI.Services.Commands
{
    public class WishlistCommandRepo : ICommandRepo<Wishlist , string>
    {
        private readonly WishlistContext _context;
        private readonly ILogger<Wishlist> _logger;

        public WishlistCommandRepo(WishlistContext context, ILogger<Wishlist> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add User 
        public async Task<Wishlist> Add(Wishlist item)
        {
            var transaction = _context.Database.BeginTransaction();
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Wishlist Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var user = _context.Wishlists.Add(item);
            if (user == null)
            {
                _logger.LogError("Unable to add object");
                await transaction.RollbackAsync();
                throw new UnableToAddException("Unable To Add Wishlist");
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation("Wishlist Added Successfully");
            return item;
            throw new UnableToAddException("Unable To Add Wishlist");
        }


        //Update User
        public async Task<Wishlist> Update(Wishlist item)
        {
            var transaction = _context.Database.BeginTransaction();
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Wishlist Object is null");
            }

            var Wishlist = await _context.Wishlists.FirstOrDefaultAsync(x => x.Email == item.Email)
                ?? throw new EmptyValueException("Invalid Object!!! No such Wishlist Exist!!");

            if (item != null)
            {
                Wishlist.Price = item.Price ?? Wishlist.Price;
                Wishlist.Quantity = item.Quantity;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return Wishlist;
            throw new UnableToUpdateException("Unable to update the Wishlist");
        }
    }
}
