using LandLeaser.API.ViewModel;
using LandLeaserApp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp.Services
{
    public class LoginService : ILoginService
    {
        HttpClient Client;
        public async Task<LoginResult> Authenticate(LoginRequest loginRequest)
        {
            //login url
            var baseUri = "https://landleaserapi.azurewebsites.net/api/Authentication/login";

            //Serialize the object
            var loginRequestStr = JsonConvert.SerializeObject(loginRequest);

            //Json string to content type conversion
            var content = new StringContent(loginRequestStr, Encoding.UTF8, "application/json");

            //Api call
            var response = await Client.PostAsync(baseUri, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginResult>();
            }
            return null;

        }
    }
}
