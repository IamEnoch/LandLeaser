﻿namespace LandLeaser.API.ViewModel
{
    public class LoginResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
