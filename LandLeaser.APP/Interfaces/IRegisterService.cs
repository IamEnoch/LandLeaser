using LandLeaser.Shared.Models;

namespace LandLeaser.App.Interfaces
{
    public interface IRegisterService
    {
        public Task<bool> CreateUser(Register registerDetails);
    }
}
