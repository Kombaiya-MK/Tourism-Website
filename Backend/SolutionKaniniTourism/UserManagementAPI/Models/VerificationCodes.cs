using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementAPI.Models
{
    public class VerificationCodes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string? Email { get; set; }
        [ForeignKey("Email")]
        public User? User { get; set; }

        [Required(ErrorMessage ="Verification codes are required")]
        public int Codes { get; set; }
    }
}
