using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementAPI.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string? Email { get; set; }
        public byte[]? Password { get; set; }
        public byte[]? HashKey { get; set; }
        public string? Role { get; set; }
        public string? Status { get; set;}

        public ICollection<UserDetails>? UserDetails { get; set; }
        public ICollection<TravelAgent>? TravelAgent { get; set; }
    }
}
