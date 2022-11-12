using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LandLeaser.API.Data.Models
{
    public class ListingImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey(nameof(Listing))]
        public Guid ListingId { get; set; }

        [JsonIgnore]
        public virtual Listing Listing { get; set; }

    }
}
