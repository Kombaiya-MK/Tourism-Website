using KTWWishListAPI.Interfaces;
using KTWWishListAPI.Models;
using KTWWishListAPI.Models.DTO;

namespace KTWWishListAPI.Utilities.Adapters
{
    public class WishlistAdapter : IAdapter
    {
        public Wishlist DTOtoWishlist(CartDTO cart)
        {
            Wishlist wishlist = new()
            {
                CartId = cart.CartId,
                Email = cart.Email,
                AddedDate = cart.AddedDate,
                Status = (cart.Status == null || cart.Status.Length == 0) ? "Active" : cart.Status,
                PackId = cart.PackId,
                Price = cart.Price,
                Quantity = cart.Quantity,
            };
            return wishlist;
        }
    }
}
