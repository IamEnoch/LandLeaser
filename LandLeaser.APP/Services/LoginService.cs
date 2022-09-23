using LandLeaser.Shared.Models;
using LandLeaserApp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp.Services
{
    public class LoginService : ILoginService
    {
        HttpClient Client = new HttpClient();
        public string BaseUrl { get; set; }

        /// <summary>
        /// Authenticate method
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>New access token with its expiry and a refresh token</returns>
        public async Task<LoginResult> Authenticate(LoginRequest loginRequest)
        {
            //login url
            BaseUrl = "https://landleaserapi.azurewebsites.net/api/Authentication/login";

            //Serialize the object
            var loginRequestStr = JsonConvert.SerializeObject(loginRequest);

            //Json string to content type conversion
            var content = new StringContent(loginRequestStr, Encoding.UTF8, "application/json");

            //Api call
            var response = await Client.PostAsync(BaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
                return loginResult;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// Refresh token method
        /// </summary>
        /// <param name="tokenRequest">Access token and the refresh token</param>
        /// <returns>New access token with its expiry and it`s refresh token</returns>
        public async Task<LoginResult> RefreshToken(TokenRequest tokenRequest)
        {
            //Refresh token url
            BaseUrl = "https://landleaserapi.azurewebsites.net/api/Authentication/refreshToken";

            //Serialize the token request
            var tokenRequstStr = JsonConvert.SerializeObject(tokenRequest);

            //Change from strig to content type
            var content = new StringContent(tokenRequstStr, Encoding.UTF8, "application/json");

            //Api call
            var response = await Client.PostAsync(BaseUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var loginResult = await response.Content.ReadFromJsonAsync<LoginResult>();
                return loginResult;
            }
            else
            {
                return null;
            }
        }
    }
}
