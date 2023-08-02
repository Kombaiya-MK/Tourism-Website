using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTWFeedbackAPI.Models
{
    public class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Feedback Id is required")]
        public string? FeedbackId { get; set; }

        [Required(ErrorMessage = "Email is Required!!")]
        [EmailAddress]
        public string? Email { get; set;}

        [Required(ErrorMessage = "Service type is required")]
        public string? ServiceType { get; set; }

        [Required(ErrorMessage = "Service Id is required")]
        public string? PackId { get; set; }

        [Required(ErrorMessage = "Service Name is required")]
        public string? ServiceName { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Review is Required")]
        public string? Review { get; set;}

        [Required(ErrorMessage = "Feedback Date is Required")]
        public DateTime FeedbackDate { get; set; }
    }
}
