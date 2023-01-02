using LandLeaser.App.Helpers;
using LandLeaser.App.Interfaces;
using LandLeaser.Shared.DTOs;
using LandLeaser.Shared.Models;

namespace LandLeaser.App.Services
{
    public class ListingService : IListingService
    {
        private readonly IRestService _restService;
        private readonly ILoginService _loginService;
        public TokenValidateHelper TokenValidater = new TokenValidateHelper();
        public ListingService(IRestService restService, ILoginService loginService)
        {
            _restService = restService;
            _loginService = loginService;
        }
        
        /// <summary>
        /// Get action to get listings
        /// </summary>
        /// <param name="authToken">Bearer authentication token</param>
        /// <returns>List of Listings</returns>
        public async Task<List<GetListingDto>> GetListingsAsync(string authToken)
        {
            string endpoint = "api/Listings";

            authToken = await RefreshToken(authToken);

            var response = await _restService.GetItemsAsync<GetListingDto>(authToken, endpoint);

            return response;
        }

        /// <summary>
        /// Get action to get a listing
        /// </summary>
        /// <param name="authToken">Bearer authentication token</param>
        /// <param name="id">Listing id</param>
        /// <returns></returns>
        public async Task<GetListingDto> GetListingAsync(string authToken, string id)
        {
            string endpoint = $"api/listings/{id}";

            authToken = await RefreshToken(authToken);

            var response = await _restService.GetItemAsync<GetListingDto>(authToken, id, endpoint);

            return response;
        }

        /// <summary>
        /// Method to post a listing
        /// </summary>
        /// <param name="authToken">Bearer authentication token</param>
        /// <param name="createListing">Listing to be created</param>
        /// <returns></returns>
        public async Task<GetListingDto> PostListingAsync(string authToken, CreateListingDto createListing)
        {
            string endpoint = "api/listings";

            authToken = await RefreshToken(authToken);

            var response =
                await _restService.PostItemAsync<CreateListingDto, GetListingDto>(authToken, createListing, endpoint);

            return response;
        }

        public async Task<string> RefreshToken(string accessToken)
        {
            //Validate the token
            var validToken = await TokenValidater.ValidateToken(accessToken);

            //Refresh token
            if (validToken != true)
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
                await SecureStorage.SetAsync(nameof(App.RefreshToken), tokenRefreshResult.RefreshToken);

                
            }
            return accessToken;
        }
    }
}
