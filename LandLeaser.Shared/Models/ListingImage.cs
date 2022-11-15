using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using LandLeaser.Shared.DTOs;

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
