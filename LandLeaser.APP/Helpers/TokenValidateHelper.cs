using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp.Helpers
{
    public class TokenValidateHelper
    {
        public async Task<bool> ValidateToken(string accessToken)
        {
            //Create JwtSecurityTokenHandler
            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadJwtToken(accessToken);

            if(token.ValidTo < DateTime.UtcNow)
            {
                return false; 
            }
            return true;
        }
    }
}
