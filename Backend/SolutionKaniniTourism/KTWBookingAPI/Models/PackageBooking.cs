using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTWBookingAPI.Models
{
    public class PackageBooking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Required(ErrorMessage = "Package Id is required")]
        public string? PackageId { get; set; }

        [Required(ErrorMessage = "Booking Id is required")]
        public string? BookingId { get; set; }
        [ForeignKey("BookindId")]
        public Booking? Booking { get; set; }

        [Required(ErrorMessage = "NoofAdults is required")]
        public int NoofAdults { get; set; }

        [Required(ErrorMessage = "NoofChildren is required")]
        public int NoofChildren { get; set; }
    }
}
