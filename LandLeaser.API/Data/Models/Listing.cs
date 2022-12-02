using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LandLeaser.API.Data.Models
{
    public class Listing
    {
        

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Location { get; set; }
        public string  Size { get; set; }
        public string Cost { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public Guid AppUserId { get; set; }
        
        public virtual ICollection<ListingImage> Images { get; set; }
        [JsonIgnore]
        public  virtual ApplicationUser ApplicationUser { get; set; }

    }
}
