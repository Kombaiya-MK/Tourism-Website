namespace KTWFeedbackAPI.Models.DTO
{
    public class UpdateFeedbackDTO
    {
        public string? Email { get; set; }  
        public string? PackId { get; set; }
        public string? Servicename { get; set; }
        public string? Description { get; set;}
    }
}
