using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTWLocationsAPI.Models
{
    public class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Required(ErrorMessage = "Location Id is required")]
        public string? LocationId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Latitiude is required")]
        public string? Latitiude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        public string? Longitude { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string? LocationType { get; set; }

        public ICollection<Image>? Images { get; set; }
        public ICollection<Speciality>? Specialities { get; set; }
    }
}
