using System.ComponentModel.DataAnnotations;

namespace LandLeaser.Shared.Models
{
    public class Register
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumeber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
