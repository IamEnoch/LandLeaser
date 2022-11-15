using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp.Interfaces
{
    public interface IRestService
    {
        public Task<List<T>> GetItemsAsync<T>(string authToken, string endpoint);
        public Task<T> GetItemAsync<T>(string authToken, string id, string endpoint);
    }
}
