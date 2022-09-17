using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp
{
    public interface ILoginService
    {
        public async Task<LoginResponse> Authenticate(LoginRequestDTO loginRequestDTO)
    }
}
