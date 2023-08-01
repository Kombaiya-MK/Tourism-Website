using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementAPI.Models
{
    public class UserDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Email id is required!!!")]
        [EmailAddress]
        [Key]
        public string? Email { get; set; }
        [ForeignKey("Email")]
        public User? User { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime? DateofBirth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set;}

        [Required(ErrorMessage = "phone number is  required")]
        [StringLength(10, ErrorMessage = "Phone number length is miss matched!!!")]
        public string? PhoneNumber { get; set;}


    }
}
