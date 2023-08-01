using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementAPI.Models
{
    public class TravelAgent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Email id is required!!!")]
        [EmailAddress]
        [Key]
        public string? Email { get; set;}

        [ForeignKey("Email")]
        public User? User { get; set; }

        [Required(ErrorMessage = "Agency Name required")]
        [MaxLength(50)]  
        public string? AgencyName { get; set; }

        [Required(ErrorMessage = "Agency Name required")]
        [MaxLength(15)]
        public string? AgencyPhone { get; set; }
        [Required(ErrorMessage = "Email id is required!!!")]
        [EmailAddress]
        public string? AgencyEmail { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string? AgencyAddress { get; set; }
        

    }
}
