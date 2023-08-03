using System.ComponentModel.DataAnnotations;

namespace KTWBookingAPI.Models.DTO
{
    public class BookingDTO
    {
        public string? BookingId { get; set; }
        public string? Email { get; set; }
        public DateTime? BookedDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public double? Price { get; set; }
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }
        public int NoofAdults { get; set; }
        public int NoofChildren { get; set; }
    }
}