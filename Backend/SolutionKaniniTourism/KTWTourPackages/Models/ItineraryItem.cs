using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KTWTourPackages.Models
{
    public class ItineraryItem
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Required(ErrorMessage = "Itinerary Item id is required")]
        public string? ItemId { get; set; }

        [Required(ErrorMessage = "Itinerary Id is required")]
        public string? ItineraryId { get; set; }
        [ForeignKey("ItineraryId")]
        public Itinerary? Itinerary { get; set; }

        [Required(ErrorMessage = "DayNumber is required")]
        public int DayNumber { get; set; }

        [Required(ErrorMessage = "Activity is required")]
        public string? Activity { get; set; }

        [Required(ErrorMessage = "StartTime is required")]
        public string? StartTime { get; set; }

        [Required(ErrorMessage = "EndTime is required")]
        public string? EndTime { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }


    }
}
