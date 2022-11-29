using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LandLeaser.APP.Helpers;
using LandLeaser.APP.Interfaces;
using LandLeaser.APP.Views;
using LandLeaser.Shared.Models;
using Newtonsoft.Json;

namespace LandLeaser.APP.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        string _email;

        [ObservableProperty]
        string _password;

        //Dependency injection of the login service
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
        private readonly AppShellViewModel _appShellViewModel;
        private readonly ProfileTabViewModel _profileTabViewModel;
        private AppShell _appShell;
        public string UserDetailsStr { get; set; }
        public TokenValidateHelper TokenValidater = new TokenValidateHelper();

        public event EventHandler StateChanged;

        public LoginViewModel(ILoginService loginService, IUserService userService, AppShellViewModel appShellViewModel, ProfileTabViewModel profileTabViewModel, AppShell appShell)
        {
            Title = nameof(LoginPage);
            _loginService = loginService;
            _userService = userService;
            _appShellViewModel = appShellViewModel;
            _profileTabViewModel = profileTabViewModel;
            _appShell = appShell;
        }

        [RelayCommand]
        public async Task DisplaySignUpPage()
        {
            await Shell.Current.GoToAsync($"///{nameof(SignUpPage)}");
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
                    App.IsLoggedIn = true;
                    await Shell.Current.GoToAsync($"///{nameof(AppShell)}");
                }
                else
                {
                    // Display profileLogInPage
                    App.IsLoggedIn = false;
                }

                IsBusy = false;
            }
            else
            {
                // Display profileLogInPage
                App.IsLoggedIn = false;
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

        [RelayCommand]
        async Task Login()
        {
            IsBusy = true;
            //Check whether string is null or empty for both fields
            if(!String.IsNullOrEmpty(_email) && !String.IsNullOrEmpty(_password))
            {
                var loginCredentials = new LoginRequest
                {
                    EmailAddress = Email,
                    Password = this.Password
                };

                //Authenticate the login credentials
                var response = await _loginService.Authenticate(loginCredentials);

                if(response != null)
                {                   
                    //Set the neceessary credentials
                    //Remove existing preference details
                    if (Preferences.ContainsKey(nameof(App.UserInfo)))
                    {
                        Preferences.Remove(nameof(App.UserInfo));
                    }

                    //Desired information to be stored
                    var userDetails = await _userService.GetUser(Email, response.Token);
                    var userInfoStr = JsonConvert.SerializeObject(userDetails);

                    //Add user preferences and securely store token
                    Preferences.Set(nameof(App.UserInfo), userInfoStr);
                    await SecureStorage.SetAsync(nameof(App.Token), response.Token);
                    await SecureStorage.SetAsync(nameof(App.RefreshToken), response.RefreshToken);

                    IsBusy = false;

                    await Shell.Current.DisplayAlert("Login", "Login was Successful!!!", "Ok");
                    Application.Current.MainPage = _appShell;

                }
                else
                {
                    IsBusy = false;
                    await Shell.Current.DisplayAlert("Login", "Login was unsuccessful!!!", "Ok");
                }
                
            }
            else
            {
                IsBusy = false;
                await Shell.Current.DisplayAlert("Error", "Enter all fields", "Ok");
            }
            StateChanged?.Invoke(this, EventArgs.Empty);

        }
    }
}
