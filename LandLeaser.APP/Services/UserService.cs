using LandLeaserApp.Interfaces;
using LandLeaser.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using LandLeaserApp.Helpers;
using LandLeaser.APP;

namespace LandLeaserApp.Services
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
            var validToken = TokenValidater.ValidateToken(accessToken);

            //Refresh token
            if(validToken != true)
            {
                var tokenRequest = new TokenRequest()
                {
                    Token = accessToken,
                    RefreshToken = Preferences.Get(nameof(App.RefreshToken), "")
                };

                var tokenRefreshResult = await _loginService.RefreshToken(tokenRequest);
                accessToken = tokenRefreshResult.Token;

                //Set the new preferences
                Preferences.Set(nameof(App.Token), tokenRefreshResult.Token);
                Preferences.Set(nameof(App.RefreshToken), tokenRefreshResult.RefreshToken);

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
            else { return null; }
        }
    }
}
