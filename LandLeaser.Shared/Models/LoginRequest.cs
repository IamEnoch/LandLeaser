using System.ComponentModel.DataAnnotations;

namespace LandLeaser.Shared.Models
{
    public class LoginRequest
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
