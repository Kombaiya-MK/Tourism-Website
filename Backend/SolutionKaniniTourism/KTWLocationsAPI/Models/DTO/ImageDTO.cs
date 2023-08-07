using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KTWLocationsAPI.Models.DTO
{
    public class ImageDTO
    {
        public string? LocationId { get; set; }
        public string? Picture { get; set; }
    }
}
