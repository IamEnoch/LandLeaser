using System.Text.Json.Serialization;

namespace LandLeaser.Shared.Models
{
    public class ListingImage
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("listingId")]
        public string ListingId { get; set; }
    }
}
