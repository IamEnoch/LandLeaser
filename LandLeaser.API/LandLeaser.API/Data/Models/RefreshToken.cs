using System.ComponentModel.DataAnnotations.Schema;

namespace LandLeaser.API.Data.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateExpire { get; set; }

        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
