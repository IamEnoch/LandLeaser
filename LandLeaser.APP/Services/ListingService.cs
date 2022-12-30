﻿using LandLeaser.APP.Interfaces;
using LandLeaser.Shared.DTOs;

namespace LandLeaser.APP.Services
{
    public class ListingService : IListingService
    {
        private readonly IRestService _restService;
        public ListingService(IRestService restService)
        {
            _restService = restService;
        }
        
        /// <summary>
        /// Get action to get listings
        /// </summary>
        /// <param name="authToken">Bearer authentication token</param>
        /// <returns>List of Listings</returns>
        public async Task<List<GetListingDto>> GetListingsAsync(string authToken)
        {
            string endpoint = "api/Listings";
            
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

            var response =
                await _restService.PostItemAsync<CreateListingDto, GetListingDto>(authToken, createListing, endpoint);

            return response;
        }
    }
}
