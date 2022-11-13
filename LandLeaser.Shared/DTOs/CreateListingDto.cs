using System;
using System.Collections.Generic;
using System.Text;
using LandLeaser.Shared.Models;

namespace LandLeaser.Shared.DTOs
{
    public class CreateListingDto
    {
        public string Location { get; set; }
        public string Size { get; set; }
        public string Cost { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public Guid AppUserId { get; set; }
        public List<ListingImage>? Images { get; set; }

    }
}
