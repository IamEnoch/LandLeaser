using System.Text.Json.Serialization;
using LandLeaser.Shared.DTOs;

namespace LandLeaser.Shared.DTOs
{
    public class GetListingDto : CreateListingDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

    }
}
