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

namespace LandLeaserApp.ViewModels
{
    public  partial class LoadingPageViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        public TokenValidateHelper TokenValidater = new TokenValidateHelper();
        public LoadingPageViewModel(ILoginService loginService, IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;
            CheckUserLoginDetails();
        }
        private async void CheckUserLoginDetails()
        {
            string userDetailsStr = Preferences.Get(nameof(App.UserInfo), "");
            string accessToken = await SecureStorage.GetAsync(nameof(App.Token));
            string refreshToken = await SecureStorage.GetAsync(nameof(App.RefreshToken));

            if (string.IsNullOrWhiteSpace(userDetailsStr))
            {
                // navigate to Login Page
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");                
            }
            else
            {
                //Validate the token
                var validToken = TokenValidater.ValidateToken(accessToken);
                if(validToken != true)
                {
                    var tokenRequest = new TokenRequest
                    {
                        Token = accessToken,
                        RefreshToken = refreshToken
                    };

                    //call refresh token method
                    var response = await _loginService.RefreshToken(tokenRequest);
                    {
                        await Shell.Current.DisplayAlert("Fail", "Refreshing token unsuccessful", "ok");
                        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    }

                    //Update user preferences and securely store token
                    await SecureStorage.SetAsync(nameof(App.Token), response.Token);
                    await SecureStorage.SetAsync(nameof(App.RefreshToken), response.RefreshToken);

                    App.Token = response.Token;
                    App.RefreshToken = response.RefreshToken;
                }
                else
                {
                    var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);

                    App.UserInfo = userInfo;
                    App.Token = accessToken;
                    App.RefreshToken = refreshToken;

                    await Shell.Current.DisplayAlert("Success", "No login required", "ok");
                    await Shell.Current.GoToAsync($"//{nameof(PushPage)}");
                }
            }
        }
    }
}
