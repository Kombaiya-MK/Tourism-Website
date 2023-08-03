namespace KTWTourPackages.Models.DTO
{
    public class UpdateItemDTO
    {
        public string? PackId {  get; set; }    
        public string? ItineraryId { get; set; }
        public string? ItemId { get; set; }
        public string? Status { get; set; }
    }
}