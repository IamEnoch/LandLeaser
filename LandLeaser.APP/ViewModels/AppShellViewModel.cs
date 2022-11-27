using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LandLeaser.APP.Helpers;
using LandLeaser.APP.Interfaces;
using LandLeaser.Shared.Models;
using Newtonsoft.Json;

namespace LandLeaser.APP.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        public string Name { get; set; }
        public string UserDetailsStr { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotVisible))]
        bool _isVisible;

        public bool IsNotVisible => !IsVisible;

        public TokenValidateHelper TokenValidater = new TokenValidateHelper();
        public AppShellViewModel(ILoginService loginService, IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;
        }

        /// <summary>
        /// Method to check and validate stored credentials. Called in the AppShell page View model
        /// </summary>
        /// <returns></returns>
        public async Task CheckAsync()
        {
            IsBusy = true;

            var result = CheckUserLoginDetails();

            if (result == true)
            {
                string accessToken = await SecureStorage.GetAsync(nameof(App.Token));
                string refreshToken = await SecureStorage.GetAsync(nameof(App.RefreshToken));
                var response = await ValidateAndRefreshAsync(accessToken, refreshToken, UserDetailsStr);

                if (response == true)
                {
                    // Display profileLoggedInPage
                    IsVisible = true;
                    Name = "Profile";
                }
                else
                {
                    // Display profileLogInPage
                    IsVisible = false;
                    Name = "Login";
                }

                IsBusy = false;
            }
            else
            {
                // Display profileLogInPage
                IsVisible = false;
                Title = "Login";
            }
        }
        public bool CheckUserLoginDetails()
        {
            UserDetailsStr = Preferences.Get(nameof(App.UserInfo), "");


            if (string.IsNullOrWhiteSpace(UserDetailsStr))
                return false;

            return true;
        }

        public async Task<bool> ValidateAndRefreshAsync(string accessToken, string refreshToken, string userDetailsStr)
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

