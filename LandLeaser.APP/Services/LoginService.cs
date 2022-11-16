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
        private readonly IRestService _restService;
        public LoginService(IRestService restService)
        {
            _restService = restService;
        }
        public string BaseUrl { get; set; }

        /// <summary>
        /// Authenticate method
        /// </summary>
        /// <param name="authToken">api endpoint</param>
        /// <param name="loginRequest"></param>
        /// <returns>New access token with its expiry and a refresh token</returns>
        public async Task<LoginResult> Authenticate(LoginRequest loginRequest, string authToken = null)
        {

            string endpoint = "api/Authentication/login";
            var response = await _restService.PostItemAsync<LoginRequest,LoginResult>(authToken, loginRequest, endpoint);
            return response;
            
        }

        /// <summary>
        /// Refresh token method
        /// </summary>
        /// <param name="authToken">Bearer authentication token</param>
        /// <param name="tokenRequest">Access token and the refresh token</param>
        /// <returns>New access token with its expiry and it`s refresh token</returns>
        public async Task<LoginResult> RefreshToken( TokenRequest tokenRequest, string authToken)
        {
            string endpoint = "api/Authentication/refreshToken";
            var response = await _restService.PostItemAsync<TokenRequest, LoginResult>(authToken, tokenRequest, endpoint);
            return response;
        }
    }
}
