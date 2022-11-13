using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using LandLeaser.Shared.DTOs;

namespace LandLeaser.Shared.Models
{
    public class ListingImage
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public Guid ListingId { get; set; }
        public GetListingDto Listing { get; set; }
    }
}
