using System.ComponentModel.DataAnnotations;

namespace LandLeaser.API.ViewModel
{
    public class TokenRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
