using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LandLeaser.API.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //Navigation properties
        [JsonIgnore]
        public virtual ICollection<Listing> Listings { get; set; }
    }
}
