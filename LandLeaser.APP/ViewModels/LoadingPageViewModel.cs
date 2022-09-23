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

namespace LandLeaserApp.ViewModels
{
    public  partial class LoadingPageViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
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
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                // navigate to Login Page
            }
            else
            {
                //Instatialize a JwtSecurityTokenHandler
                var handler = new JwtSecurityTokenHandler();

                var token = handler.ReadJwtToken(accessToken);

                if(token.ValidTo < DateTime.UtcNow)
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

                    //Add user preferences and securely store token
                    await SecureStorage.SetAsync(nameof(App.Token), response.Token);
                    await SecureStorage.SetAsync(nameof(App.RefreshToken), response.RefreshToken);

                    App.Token = accessToken;
                    App.RefreshToken = refreshToken;
                }
                else
                {
                    var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);

                    App.UserInfo = userInfo;
                    App.Token = accessToken;
                    App.RefreshToken = refreshToken;
                }                

            }
        }
    }
}
