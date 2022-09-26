using LandLeaser.Shared.Models;
using LandLeaserApp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        HttpClient Client = new HttpClient();
        public RegisterService(ILoginService loginService, IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;
        }
        public async Task<LoginResult> CreateUser(Register registerDetails)
        {
            //Base url
            var Url = "https://landleaserapi.azurewebsites.net/api/Authentication/register";

            //Serialize object to a readable string
            var userStr = JsonConvert.SerializeObject(registerDetails);

            //Convert from type string to http content
            var content = new StringContent(userStr);

            var response = await Client.PostAsync(Url, content);

            if (response.IsSuccessStatusCode)
            {
                //Get user details using email

                //Login the user
                var result = await _loginService.Authenticate(new LoginRequest
                {
                    EmailAddress = registerDetails.Password,
                    Password = registerDetails.Password
                });
                                
                if(result != null)
                {
                    //Save preferences
                    return result;
                }
                return null;
            }
            return null;
            
        }
    }
}
