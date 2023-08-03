using KTWWishListAPI.Models;
using KTWWishListAPI.Models.DTO;

namespace KTWWishListAPI.Interfaces
{
    public interface IAdapter
    {
        Wishlist DTOtoWishlist(CartDTO cart);
    }
}
