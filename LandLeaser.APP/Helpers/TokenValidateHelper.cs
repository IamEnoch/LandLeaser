using System.IdentityModel.Tokens.Jwt;

namespace LandLeaser.APP.Helpers
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
