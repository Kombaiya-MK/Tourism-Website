namespace UserManagementAPI.Models.DTO
{
    public class RegisterDTO : UserDetails
    {
        public string? PasswordClear { get; set; }
    }
}
