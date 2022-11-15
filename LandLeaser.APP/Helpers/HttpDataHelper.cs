using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using LandLeaser.Shared.Models;

namespace LandLeaserApp.Helpers
{
    public class HttpDataHelper
    {
        private HttpClient _client = new();

        public HttpDataHelper(string baseAddress, string authToken, IDictionary<string, string> headersDictionary = null)
        {
            _client.BaseAddress = new Uri(baseAddress);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            foreach (var header in headersDictionary)
            {
                _client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        /// <summary>
        /// Get api call
        /// </summary>
        /// <param name="endpoint">Specific endpoint for the get call</param>
        /// <returns></returns>
        public async Task<(ResponseMessage, T)> GetAsync<T>(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return default;
            
            var responseContent = await response.Content.ReadFromJsonAsync<T>();

            return (new ResponseMessage(response.StatusCode, response.IsSuccessStatusCode), responseContent);
        }
    }
}
