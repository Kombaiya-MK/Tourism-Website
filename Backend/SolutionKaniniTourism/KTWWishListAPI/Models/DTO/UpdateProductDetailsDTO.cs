namespace KTWWishListAPI.Models.DTO
{
    public class UpdateProductDetailsDTO
    {
        public string? Email { get; set; }
        public string? CartId { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }

    }
}
