using System.Net.Http.Headers;
using LandLeaser.APP.Helpers;
using LandLeaser.APP.Interfaces;
using LandLeaser.Shared.Models;
using Newtonsoft.Json;

namespace LandLeaser.APP.Services
{
    public class UserService : IUserService
    {
        private readonly ILoginService _loginService;
        public HttpClient Client = new HttpClient(); 
        public TokenValidateHelper TokenValidater = new TokenValidateHelper();

        public UserService(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// Get user method
        /// </summary>
        /// <param name="email">Email of the particular user</param>
        /// <param name="accessToken">Access token belonging to the user</param>
        /// <returns>User basic information(Full name, email and phone number)</returns>
        public async Task<UserBasicInfo> GetUser(string email, string accessToken)
        {
            //Url for the desired endpoint
            var url = $"https://landleaserapi.azurewebsites.net/api/Users/{email}";

            //Validate the token
            var validToken = await TokenValidater.ValidateToken(accessToken);

            //Refresh token
            if(validToken != true)
            {
                var tokenRequest = new TokenRequest()
                {
                    Token = accessToken,
                    //RefreshToken = Preferences.Get(nameof(App.RefreshToken), "");
                    RefreshToken = await SecureStorage.GetAsync(nameof(App.RefreshToken))
                };

                var tokenRefreshResult = await _loginService.RefreshToken(tokenRequest);
                accessToken = tokenRefreshResult.Token;

                //Set the new security sensitive preferences
                await SecureStorage.SetAsync(nameof(App.Token), tokenRefreshResult.Token);
                await SecureStorage.SetAsync(nameof(App.Token), tokenRefreshResult.RefreshToken);

            }

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //Call the api to get info of a particular user
            var response = await Client.GetAsync(url) ;

            if(response.IsSuccessStatusCode)
            {
                //Change from type http content to json string
                var responseStr = await response.Content.ReadAsStringAsync();

                //Deserialize the reponse
                var userDetails = JsonConvert.DeserializeObject<UserBasicInfo>(responseStr);

                return userDetails;
            }

            return null;
        }
    }
}
