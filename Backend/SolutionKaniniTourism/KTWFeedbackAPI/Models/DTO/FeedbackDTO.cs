namespace KTWFeedbackAPI.Models.DTO
{
    public class FeedbackDTO
    {
        public string? Email { get; set; }
        public string? Servicename { get; set; }
        public string? ServiceType { get; set; }
        public string? PackId { get; set; }
        public string? Review { get; set; } 
        public int Rating { get; set; }
    }
}
