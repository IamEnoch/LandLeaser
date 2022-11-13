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
        public HttpClient Client { get; set; }

        public HttpDataHelper(string baseAddress, string authToken, IDictionary<string, string> headersDictionary = null)
        {
            Client = new HttpClient();

            Client.BaseAddress = new Uri(baseAddress);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            foreach (var header in headersDictionary)
            {
                Client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        /// <summary>
        /// Get api call
        /// </summary>
        /// <param name="endpoint">Specific endpoint for the get call</param>
        /// <returns></returns>
        public async Task<ResponseMessage> GetAsync<T>(string endpoint)
        {
            var response = await Client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return default;

            var responseContent = await response.Content.ReadFromJsonAsync<T>();

            return new ResponseMessage(responseContent, response.StatusCode, response.IsSuccessStatusCode);
        }


    }
}
