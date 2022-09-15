using System.ComponentModel.DataAnnotations;

namespace LandLeaser.API.ViewModel
{
    public class RegisterVM
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
