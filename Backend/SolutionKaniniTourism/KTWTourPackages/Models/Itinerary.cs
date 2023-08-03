using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KTWTourPackages.Models
{
    public class Itinerary
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Key]
        [Required(ErrorMessage = "Itinerary Id is required")]
        public string? ItineraryId { get; set; }

        [Required(ErrorMessage = "Pack Id is required")]
        public string? PackId { get; set; }
        [ForeignKey("PackId")]
        public TourPackage? Package { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        public ICollection<ItineraryItem>? Items { get; set; }
    }
}
