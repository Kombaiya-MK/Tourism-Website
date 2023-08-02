using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KTWWishListAPI.Models
{
    public class Wishlist
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        public string? CartId { get; set; }

        [Required(ErrorMessage = "Email is Required!!")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Pack Id is Required!!")]
        public string? PackId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime AddedDate { get; set; }
    }
}
