using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KTWTourPackages.Models.DTO
{
    public class ItemDTO
    {
        public string? Name { get; set; }   
        public string? Description { get; set; }
        public string? PackId { get; set; }
        public string? ItineraryId { get; set; }
        public string? ItemId { get; set; }
        public int DayNumber { get; set; }
        public string? Activity { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }

        public string? Location { get; set; }
        public string? Status { get; set; }
    }
}