using LandLeaser.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp.Interfaces
{
    public interface ILoginService
    {
        public Task<LoginResult> Authenticate(LoginRequest loginRequest, string authToken = null);
        public Task<LoginResult> RefreshToken(TokenRequest tokenRequest, string authToken = null);
    }
}
