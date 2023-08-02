using KTWWishListAPI.Interfaces;
using KTWWishListAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KTWWishListAPI.Services.Queries
{
    public class WishlistQueryRepo : IQueryRepo<Wishlist , string>
    {
        private readonly WishlistContext _context;
        private readonly ILogger<Wishlist> _logger;

        public WishlistQueryRepo(WishlistContext context, ILogger<Wishlist> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Wishlist> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                _logger.LogError("Given Email address is null");
                throw new ArgumentNullException(nameof(key));
            }
            var Wishlist = await _context.Wishlists.FirstOrDefaultAsync(x => x.Email == key);
            if (Wishlist == null)
            {
                _logger.LogError("Invalid id or no data available");
                throw new ArgumentException(nameof(Wishlist));
            }
            return Wishlist;
        }

        public async Task<ICollection<Wishlist>> GetAll()
        {
            _logger.LogInformation("Inside GetAll of Wishlists");
            return await _context.Wishlists.ToListAsync();
        }
    }
}
