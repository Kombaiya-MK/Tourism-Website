namespace KTWWishListAPI.Models.DTO
{
    public class UpdateCartStatusDTO
    {
        public string? Email { get; set; }
        public string? CartId { get; set; }
        public string? Status { get; set; }
    }
}