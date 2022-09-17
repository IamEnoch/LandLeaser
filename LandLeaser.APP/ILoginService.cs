using LandLeaser.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp
{
    public interface ILoginService
    {
        public async Task<LoginResult> Authenticate(LoginRequest loginRequest);
    }
}
