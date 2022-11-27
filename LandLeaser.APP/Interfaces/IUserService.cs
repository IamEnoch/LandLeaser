using LandLeaser.Shared.Models;

namespace LandLeaser.APP.Interfaces
{
    public interface IUserService
    {
        public Task<UserBasicInfo> GetUser(string email, string accessToken);
    }
}
