using LandLeaser.Shared.Models;

namespace LandLeaser.APP.Interfaces
{
    public interface IRegisterService
    {
        public Task<bool> CreateUser(Register registerDetails);
    }
}
