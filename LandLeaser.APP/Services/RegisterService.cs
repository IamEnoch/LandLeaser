﻿using LandLeaser.Shared.Models;
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
        HttpClient Client = new HttpClient();
        public RegisterService(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public async Task<bool> CreateUser(Register registerDetails)
        {
            //Base url
            var Url = "https://landleaserapi.azurewebsites.net/api/Authentication/register";

            //Serialize object to a readable string
            var userStr = JsonConvert.SerializeObject(registerDetails);

            //Convert from type string to http content
            var content = new StringContent(userStr, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(Url, content);

            if (response.IsSuccessStatusCode)
            {
                return true;                
            }
            return false;
            
        }
    }
}
