using LandLeaser.Shared.Models;

namespace LandLeaser.APP.Interfaces
{
    public interface ILoginService
    {
        public Task<LoginResult> Authenticate(LoginRequest loginRequest, string authToken = null);
        public Task<LoginResult> RefreshToken(TokenRequest tokenRequest, string authToken = null);
    }
}
