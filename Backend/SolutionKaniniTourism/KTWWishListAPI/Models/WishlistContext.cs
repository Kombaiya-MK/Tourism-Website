#nullable disable
using KTWWishListAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KTWWishListAPI.Models
{
    public class WishlistContext : DbContext
    {
        public WishlistContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Wishlist> Wishlists { get; set; }

    }
}
