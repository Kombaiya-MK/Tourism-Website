using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTWLocationsAPI.Models
{
    public class Speciality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Location Id is required")]
        public string? LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location? Location { get; set; }

        [Required(ErrorMessage = "Speciality is required")]
        public string? Special { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }


    }
}
