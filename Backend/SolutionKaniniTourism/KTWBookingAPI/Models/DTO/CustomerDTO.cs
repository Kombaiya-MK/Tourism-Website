using System.ComponentModel.DataAnnotations;

namespace KTWBookingAPI.Models.DTO
{
    public class CustomerDTO
    {
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerGender { get; set; }
        public string? CustomerAge { get; set; }

        public string? CustomerStatus { get; set;}
    }
}