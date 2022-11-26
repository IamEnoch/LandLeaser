using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using LandLeaser.Shared.Models;

namespace LandLeaser.Shared.DTOs
{
    public class CreateListingDto
    {

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }

        [JsonPropertyName("cost")]
        public string Cost { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("appUserId")]
        public string AppUserId { get; set; }

        [JsonPropertyName("images")]
        public List<ListingImage>? Images { get; set; }

    }
}
