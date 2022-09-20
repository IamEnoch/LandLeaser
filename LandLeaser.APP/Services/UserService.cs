using LandLeaserApp.Interfaces;
using LandLeaserApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp.Services
{
    public class UserService : IUserService
    {
        HttpClient Client = new HttpClient();   
        public async Task<UserBasicInfo> GetUser(string email)
        {
            //Url for the desired endpoint
            var url = $"https://landleaserapi.azurewebsites.net/api/Users/{email}";

            //Call the api to get info of a particular user
            var response = await Client.GetAsync(url) ;

            if(response != null)
            {
                //Change from type http content to json string
                var responseStr = await response.Content.ReadAsStringAsync();

                //Deserialize the reponse
                var userDetails = JsonConvert.DeserializeObject<UserBasicInfo>(responseStr);

                return userDetails;
            }
            else { return null; }
        }
    }
}
