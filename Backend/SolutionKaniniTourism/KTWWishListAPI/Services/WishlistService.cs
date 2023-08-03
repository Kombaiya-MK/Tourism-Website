using KTWWishListAPI.Interfaces;
using KTWWishListAPI.Models;
using KTWWishListAPI.Models.DTO;
using KTWWishListAPI.Utilities.CustomExceptions;

namespace KTWWishListAPI.Services
{
    public class WishlistService : IWishlistServices
    {
        private readonly IAdapter _adapter;
        private readonly ICommandRepo<Wishlist, string> _cmdRepo;
        private readonly IQueryRepo<Wishlist, string> _qryRepo;

        public WishlistService(ICommandRepo<Wishlist , string> cmdWishlistRepo , IQueryRepo<Wishlist , string> queryWishlistRepo ,
            IAdapter adapter) 
        {
            _adapter = adapter;
            _cmdRepo = cmdWishlistRepo;
            _qryRepo = queryWishlistRepo;
        }

        public async Task<Wishlist> AddToCart(CartDTO cart)
        {
            if (cart == null || cart.Email == null)
            {
                throw new EmptyValueException("You're trying to insert Empty cart");
            }
            var cartid = await GetUserWishlistCount(cart.Email) + 1;
            cart.CartId = "CART00" + cartid.ToString();
            var newcart = _adapter.DTOtoWishlist(cart) ??
                throw new EmptyValueException("Adapter Thrown empty object for cart");
            var wishlist = await _cmdRepo.Add(newcart) ??
                throw new UnableToAddException("Unable to add item to cart!!");
            return wishlist;
        }

        public async Task<ICollection<Wishlist>> GetWishlists()
        {
            return await _qryRepo.GetAll();
        }

        public async Task<Wishlist> RemoveFromCart(UpdateCartStatusDTO updateCart)
        {
            Wishlist cart = new()
            {
                Email = updateCart.Email,
                CartId = updateCart.CartId,
                Status = updateCart.Status,
            };
            var result = await _cmdRepo.Update(cart) ??
                throw new UnableToUpdateException("Unable to update wishlist");
            return result;
        }

        public async Task<Wishlist> UpdateProduct(UpdateProductDetailsDTO updateProduct)
        {
            Wishlist cart = new()
            {
                Email = updateProduct.Email,
                CartId = updateProduct.CartId,
                Price = updateProduct.Price,
                Quantity = updateProduct.Quantity,
            };
            var result = await _cmdRepo.Update(cart) ??
                throw new UnableToUpdateException("Unable to update wishlist");
            return result;
        }

        private async Task<int> GetUserWishlistCount(string email)
        {
            var cart = await _qryRepo.GetAll();
            int count = cart.Where(x => x.Email == email).Count();
            return count;
        }
    }
}
