namespace UserManagementAPI.Models.DTO
{
    public class ForgotPasswordDTO
    {
        public string? Email { get; set; }
        public int Code { get; set; }   
    }
}
