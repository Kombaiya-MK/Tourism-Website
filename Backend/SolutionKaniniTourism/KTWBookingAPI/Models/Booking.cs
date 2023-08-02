using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTWBookingAPI.Models
{
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Booking Id is required")]
        [Key] 
        public string? BookingId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set;}

        [Required(ErrorMessage = "Booked Date is required")]
        public DateTime? BookedDate { get; set; }

        [Required(ErrorMessage = "Check in Date is required")]
        public DateTime? CheckInDate { get; set; }

        [Required(ErrorMessage = "Checkout Date is required")]
        public DateTime? CheckOutDate { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "Payment Mode is required")]
        public string? PaymentMethod { get; set; }
    }
}
