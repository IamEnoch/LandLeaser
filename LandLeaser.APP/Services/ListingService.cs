using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using LandLeaser.Shared.DTOs;
using LandLeaserApp.Helpers;
using LandLeaserApp.Interfaces;

namespace LandLeaserApp.Services
{
    public class ListingService : IListingService
    {
        private readonly IRestService _restService;
        public ListingService(IRestService restService)
        {
            _restService = restService;
        }
        
        public async Task<List<GetListingDto>> GetListingsAsync(string authToken)
        {
            string endpoint = "api/Listings";
            
            var response = await _restService.GetItemsAsync<GetListingDto>(authToken, endpoint);

            return response;
        }

        public async Task<GetListingDto> GetListingAsync(string authToken, string id)
        {
            string endpoint = $"api/listing/{id}";

            var response = await _restService.GetItemAsync<GetListingDto>(authToken, id, endpoint);

            return response;
        }
    }
}
