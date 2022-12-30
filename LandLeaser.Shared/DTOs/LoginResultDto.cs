using System;
using System.Collections.Generic;
using System.Text;
using LandLeaser.Shared.Models;

namespace LandLeaser.Shared.DTOs
{
    public class LoginResultDto
    {
        public LoginResult LoginResult { get; set; }
        public User User { get; set; }
    }
}
