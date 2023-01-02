namespace LandLeaser.App.Interfaces
{
    public interface IRestService
    {
        public Task<List<T>> GetItemsAsync<T>(string authToken, string endpoint);
        public Task<T> GetItemAsync<T>(string authToken, string id, string endpoint);
        public Task<TResult> PostItemAsync<TPost, TResult>(string authToken, TPost item, string endpoint);
    }
}
