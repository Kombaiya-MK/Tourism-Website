using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTWLocationsAPI.Models
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Location Id is required")]
        public string? LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location? Location { get; set; }

        [Required(ErrorMessage = "Picture is required")]
        public IFormFile? Picture { get; set; }
    }
}
