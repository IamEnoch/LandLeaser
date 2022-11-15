using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using LandLeaser.Shared.DTOs;
using LandLeaserApp.Interfaces;

namespace LandLeaserApp.Services
{
    public class RestService : IRestService
    {
        private HttpClient _httpClient;

        public RestService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://landleaserapi.azurewebsites.net/");
            
        }

        public async Task<List<T>> GetItemsAsync<T>(string authToken, string endpoint)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            
            List<T> items = new();
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    items = await response.Content.ReadFromJsonAsync<List<T>>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception thrown is {ex}");
            }

            return items;
        }

        public async Task<T> GetItemAsync<T>(string authToken, string id, string endpoint)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            
            T item = default(T);

            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    item = await response.Content.ReadFromJsonAsync<T>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception thrown is {ex}");
            }

            return item;
        }
    }
}
