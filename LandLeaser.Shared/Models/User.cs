using System;
using System.Collections.Generic;
using System.Text;

namespace LandLeaser.Shared.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
