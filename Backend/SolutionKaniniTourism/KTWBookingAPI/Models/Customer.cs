using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTWBookingAPI.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer Id is required")]
        public string? CustomerId { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Customer Gender is required")]
        public string? CustomerGender { get; set; }

        [Required(ErrorMessage = "Customer Age is required")]
        public string? CustomerAge { get; set; }


    }
}
