using KTWWishListAPI.Interfaces;
using KTWWishListAPI.Models;
using KTWWishListAPI.Models.DTO;
using KTWWishListAPI.Utilities.CustomExceptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Models;
using static UserAPI.Models.Error;

namespace KTWWishListAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class WishListController : ControllerBase
    {
        private readonly IWishlistServices _service;

        public WishListController(IWishlistServices service)
        {
            _service = service;
        }

        //Add Item To Cart
        [HttpPost]
        [ProducesResponseType(typeof(Wishlist), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Wishlist>> AddToCart(CartDTO cart)
        {
            try
            {
                var cartitem = await _service.AddToCart(cart);
                if (cartitem != null)
                {
                    return Created("item added Successfully!!!", cartitem);
                }
                return BadRequest(new Error((int)(ErrorCode.BadRequest), "Process Failed"));
            }
            catch (EmptyValueException ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            catch (UnableToAddException uex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), uex.Message));
            }

            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Get All Items in the cart
        [HttpGet]
        [ProducesResponseType(typeof(Wishlist), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<Wishlist>>> GetAllItemsIntheCart()
        {
            try
            {
                var items = await _service.GetWishlists();
                if (items.Count >= 1)
                {
                    return Ok(items);
                }
                return NotFound(new Error((int)(ErrorCode.NotFound), "No  data available!!!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
        }

        //Update Cart
        [HttpPut]
        [ProducesResponseType(typeof(Wishlist), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Wishlist>> UpdateCart(UpdateCartStatusDTO statusDTO)
        {
            try
            {
                var result = await _service.RemoveFromCart(statusDTO);
                if (result != null)
                {
                    return Ok("Cart Updated Successfully");
                }
            }
            catch (EmptyValueException nex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), nex.Message));
            }
            catch (UnableToUpdateException eex)
            {
                return NotFound(new Error((int)(ErrorCode.NotFound), eex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            return BadRequest(new Error((int)(ErrorCode.BadRequest), "Cart not updated!!!"));
        }

        //Update Product
        [HttpPut]
        [ProducesResponseType(typeof(Wishlist), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> UpdateProduct(UpdateProductDetailsDTO productDetailsDTO)
        {
            try
            {
                var result = await _service.UpdateProduct(productDetailsDTO);
                if (result != null)
                {
                    return Ok("Cart Updated Successfully");
                }
            }
            catch (EmptyValueException nex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), nex.Message));
            }
            catch (UnableToUpdateException eex)
            {
                return NotFound(new Error((int)(ErrorCode.NotFound), eex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error((int)(ErrorCode.BadRequest), ex.Message));
            }
            return BadRequest(new Error((int)(ErrorCode.BadRequest), "Cart not updated!!!"));
        }
    }
}
