using KTWWishListAPI.Models;
using KTWWishListAPI.Models.DTO;

namespace KTWWishListAPI.Interfaces
{
    public interface IWishlistServices
    {
        Task<ICollection<Wishlist>> GetWishlists();
        Task<Wishlist> AddToCart(CartDTO cart);
        Task<Wishlist> RemoveFromCart(UpdateCartStatusDTO updateCart);
        Task<Wishlist> UpdateProduct(UpdateProductDetailsDTO updateProduct);
    }
}
