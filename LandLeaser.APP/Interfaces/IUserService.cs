using LandLeaser.Shared.Models;

namespace LandLeaser.App.Interfaces
{
    public interface IUserService
    {
        public Task<UserBasicInfo> GetUser(string email, string accessToken);
    }
}
