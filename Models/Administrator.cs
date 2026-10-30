using System.ComponentModel.DataAnnotations;

namespace Access_Management_Application.Models
{
    public class Administrator
    {
        [Key]
        public int AdministratorId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}