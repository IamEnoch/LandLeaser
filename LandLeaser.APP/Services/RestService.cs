using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using LandLeaser.APP.Interfaces;
using Newtonsoft.Json;

namespace LandLeaser.APP.Services
{
    public class RestService : IRestService
    {
        private HttpClient _httpClient;

        public RestService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://landleaserapi.azurewebsites.net/");
            
        }

        /// <summary>
        /// Get action to get list of items
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="authToken">Bearer authentication token</param>
        /// <param name="endpoint">api endpoint</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get action to get an item
        /// </summary>
        /// <typeparam name="T">Generic item</typeparam>
        /// <param name="authToken">Bearer authentication token</param>
        /// <param name="id">Item id</param>
        /// <param name="endpoint">api endpoint</param>
        /// <returns>Specified generic type</returns>
        public async Task<T> GetItemAsync<T>(string authToken, string id, string endpoint)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            
            T item = default;

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

        /// <summary>
        /// Post action to post an item
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <typeparam name="TResult">Result type</typeparam>
        /// <typeparam name="TPost">Post item type</typeparam>
        /// <param name="authToken">Bearer authentication token</param>
        /// <param name="item">Post item</param>
        /// <param name="itemPost">Post item</param>
        /// <param name="endpoint">api endpoint</param>
        /// <returns>Specified generic type</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TResult> PostItemAsync<TPost, TResult>(string authToken, TPost itemPost, string endpoint)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var content = new StringContent(JsonConvert.SerializeObject(itemPost), Encoding.UTF8, "application/json");
            TResult item = default;

            try
            {
                var response = await _httpClient.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    item = await response.Content.ReadFromJsonAsync<TResult>();
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
