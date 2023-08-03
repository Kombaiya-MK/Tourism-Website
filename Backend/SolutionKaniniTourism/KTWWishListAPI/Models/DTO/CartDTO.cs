using System.ComponentModel.DataAnnotations;

namespace KTWWishListAPI.Models.DTO
{
    public class CartDTO
    {
        public string? CartId { get; set; }
        public string? Email { get; set; }
        public string? PackId { get; set; }
        public int Quantity { get; set; }
        public double? Price { get; set; }
        public DateTime AddedDate { get; set; }
        public string? Status { get; set; }
    }
}