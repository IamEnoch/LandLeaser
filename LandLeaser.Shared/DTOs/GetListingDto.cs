using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using LandLeaser.Shared.Models;

namespace LandLeaser.Shared.DTOs
{
    public class GetListingDto : CreateListingDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

    }
}
