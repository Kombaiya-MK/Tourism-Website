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

        [Required(ErrorMessage = "Longtitude is required")]
        public string? Longtitude { get; set; }

        public ICollection<Image>? Images { get; set; }
        public ICollection<Speciality>? Specialities { get; set; }
    }
}
