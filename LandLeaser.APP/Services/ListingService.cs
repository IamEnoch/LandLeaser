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
        public HttpDataHelper Client { get; set; }

        public String BaseAddress = "https://landleaserapi.azurewebsites.net/";

        public async Task<List<GetListingDto>> GetListingsAsync(string authToken)
        {
            string endpoint = "api/listings";
            Client = new HttpDataHelper(BaseAddress, authToken);

            var response = await Client.GetAsync<List<GetListingDto>>(endpoint);

            var listings = await response.Item2.Content.ReadFromJsonAsync<List<GetListingDto>>();
            
            return listings;

        }

        public async Task<GetListingDto> GetListingAsync(string authToken, string id)
        {
            string endpoint = $"api/listing/{id}";
            Client = new HttpDataHelper(BaseAddress, authToken);

            var response = await Client.GetAsync<GetListingDto>(endpoint);

            var listing = await response.Item2.Content.ReadFromJsonAsync<GetListingDto>();

            return listing;
        }
    }
}
