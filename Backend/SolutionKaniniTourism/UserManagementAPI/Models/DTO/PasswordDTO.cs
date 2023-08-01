namespace UserAPI.Models.DTO
{
    public class PasswordDTO
    {
        public string? Email { get; set; }
        public string? CurrentPassword { get; set; }
        public string? UpdatedPassword { get; set; }
        public byte[]? UpdatedHash { get; set; }
    }
}
