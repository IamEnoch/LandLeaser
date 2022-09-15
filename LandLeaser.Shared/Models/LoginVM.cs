using System.ComponentModel.DataAnnotations;

namespace LandLeaser.API.ViewModel
{
    public class LoginVM
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
