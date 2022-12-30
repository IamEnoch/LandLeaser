using System.Text.Json.Serialization;

namespace LandLeaser.Shared.DTOs
{
    public class GetListingDto : CreateListingDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

    }
}
