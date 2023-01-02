using CommunityToolkit.Mvvm.Input;
using LandLeaser.App.Helpers;
using LandLeaser.App.Interfaces;
using LandLeaser.App.Views;
using LandLeaser.Shared.Models;
using Newtonsoft.Json;

namespace LandLeaser.App.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        //private readonly AppShell _appShell;
        public string UserDetailsStr { get; set; }

        public TokenValidateHelper TokenValidater = new TokenValidateHelper();
        public AppShellViewModel(ILoginService loginService, IUserService userService)
        {
            _loginService = loginService;
            _userService = userService;

        }

        /// <summary>
        /// Navigate to login page
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task GoToLoginAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");

        }

        /// <summary>
        /// Navigate to logout page
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task GoToLogoutAsync()
        {
            var my = await Shell.Current.DisplayAlert("Logout", "Are you sure?", "Yes", "No");
            if (my)
            {
                Preferences.Remove(nameof(App.UserInfo));
                SecureStorage.RemoveAll();

                await Shell.Current.GoToAsync($"{nameof(LogoutPage)}");

            }

        }

        /// <summary>
        /// Method to check and validate stored credentials. Called in the AppShell page View model
        /// </summary>
        /// <returns></returns>
        public async Task CheckAsync()
        {
            IsBusy = true;

            var result = await CheckUserLoginDetailsAsync();

            if (result)
            {
                string accessToken = await SecureStorage.GetAsync(nameof(App.Token));
                string refreshToken = await SecureStorage.GetAsync(nameof(App.RefreshToken));
                var response = await ValidateAndRefreshAsync(accessToken, refreshToken, UserDetailsStr);

                if (response)
                {
                    // Display profileLoggedInPage
                    IsLoggedIn = true;
                    App.IsLoggedIn = true;
                }
                else
                {
                    // Display profileLogInPage
                    IsLoggedIn = false;
                    App.IsLoggedIn = false;

                }
                IsBusy = false;
            }
            else
            {
                // Display profileLogInPage
                IsLoggedIn = false;
                App.IsLoggedIn = false;
            }
            IsBusy = false;
        }

        /// <summary>
        /// Gets the correct tab bar state
        /// </summary>
        /// <returns></returns>
        public Task TabState() => Task.FromResult(Task.FromResult(Title = App.IsLoggedIn ? "Profile" : "Login"));


        /// <summary>
        /// Check user login details
        /// </summary>
        /// <returns>Bool: Yes if stores preferences are found and No of not</returns>
        public async Task<bool> CheckUserLoginDetailsAsync()
        {
            UserDetailsStr = Preferences.Get(nameof(App.UserInfo), "");


            if (string.IsNullOrWhiteSpace(UserDetailsStr))
                return await Task.FromResult(false);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Validates token and refreshes if necessary
        /// </summary>
        /// <param name="accessToken">Access token</param>
        /// <param name="refreshToken">Refresh token</param>
        /// <param name="userDetailsStr">User details</param>
        /// <returns></returns>
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
                    return false;
                }
                else
                {
                    //Update user preferences and securely store token
                    await SecureStorage.SetAsync(nameof(App.Token), response.Token);
                    await SecureStorage.SetAsync(nameof(App.RefreshToken), response.RefreshToken);

                    App.Token = response.Token;
                    App.RefreshToken = response.RefreshToken;
                    
                    return true;
                }
            }
            else
            {
                var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);

                App.UserInfo = userInfo;
                App.Token = accessToken;
                App.RefreshToken = refreshToken;
                
                return true;
            }
        }
    }
}

