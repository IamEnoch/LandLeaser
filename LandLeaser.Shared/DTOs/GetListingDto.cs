using System;
using System.Collections.Generic;
using System.Text;
using LandLeaser.Shared.Models;

namespace LandLeaser.Shared.DTOs
{
    public class GetListingDto : CreateListingDto
    {
        public Guid Id { get; set; }
        
    }
}
