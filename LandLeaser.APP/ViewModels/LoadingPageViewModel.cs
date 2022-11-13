using LandLeaser.APP;
using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using LandLeaserApp.Interfaces;
using LandLeaser.Shared.Models;
using LandLeaserApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandLeaserApp.Helpers;
using LandLeaserApp.Views;

namespace LandLeaserApp.ViewModels
{
    public partial class LoadingPageViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        public string UserDetailsStr { get; set; }

        public TokenValidateHelper TokenValidater = new TokenValidateHelper();
        public LoadingPageViewModel(ILoginService loginService, IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;
            Check();

        }

        /// <summary>
        /// Method to check and validate stored credentials. Called in the Loading page View model
        /// </summary>
        /// <returns></returns>
        public async Task Check()
        {
            IsBusy = true;

            var result = await CheckUserLoginDetails();

            if (result == true)
            {
                string accessToken = await SecureStorage.GetAsync(nameof(App.Token));
                string refreshToken = await SecureStorage.GetAsync(nameof(App.RefreshToken));
                var response = await ValidateAndRefresh(accessToken, refreshToken, UserDetailsStr);

                if (response == true)
                    // navigate to home Page
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                else
                    // navigate to Login Page
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");


            }
            else
                // navigate to Login Page
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

            IsBusy = false;
        }
        public async Task<bool> CheckUserLoginDetails()
        {
            UserDetailsStr = Preferences.Get(nameof(App.UserInfo), "");
            

            if (string.IsNullOrWhiteSpace(UserDetailsStr))
                return false;
            
            return true;
        }

        public async Task<bool> ValidateAndRefresh(string accessToken, string refreshToken, string userDetailsStr)
        {
            //Validate the token
            var validToken = await TokenValidater.ValidateToken(accessToken);
            if (validToken != true)
            {
                var tokenRequest = new TokenRequest
                {
                    Token = accessToken,
                    RefreshToken = refreshToken
                };


                //call refresh token method
                var response = await _loginService.RefreshToken(tokenRequest);
                if (response == null)
                {
                    await Shell.Current.DisplayAlert("Fail", "Refreshing token unsuccessful", "ok");
                    return false;
                }
                else
                {
                    //Update user preferences and securely store token
                    await SecureStorage.SetAsync(nameof(App.Token), response.Token);
                    await SecureStorage.SetAsync(nameof(App.RefreshToken), response.RefreshToken);

                    App.Token = response.Token;
                    App.RefreshToken = response.RefreshToken;
                    
                    await Shell.Current.DisplayAlert("Success", "No login required", "ok");
                    return true;
                }
            }
            else
            {
                var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);

                App.UserInfo = userInfo;
                App.Token = accessToken;
                App.RefreshToken = refreshToken;
                
                await Shell.Current.DisplayAlert("Success", "No login required", "ok");
                return true;
            }
        }
    }
}
