using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KTWTourPackages.Models
{
    public class ItineraryItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Itinerary Id is required")]
        public string? ItineraryId { get; set; }
        [ForeignKey("ItineraryId")]
        public Itinerary? Itinerary { get; set; }

        [Required(ErrorMessage = "Pack Id is required")]
        public string? PackId { get; set; }
        [ForeignKey("PackId")]
        public TourPackage? Package { get; set; }

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


    }
}
