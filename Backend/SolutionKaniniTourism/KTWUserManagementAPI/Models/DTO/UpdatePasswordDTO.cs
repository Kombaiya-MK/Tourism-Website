namespace UserManagementAPI.Interfaces
{
    public class UpdatePasswordDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}