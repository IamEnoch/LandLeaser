using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LandLeaser.Shared.Models;
using LandLeaser.APP;
using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using LandLeaserApp.Interfaces;
using LandLeaserApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp.ViewModels
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

        public LoginViewModel(ILoginService loginService, IUserService userService )
        {
            Title = nameof(LoginPage);
            _loginService = loginService;
            _userService = userService;
        }

        [RelayCommand]
        public async Task DisplaySignUpPage()
        {
            await Shell.Current.GoToAsync($"{nameof(SignUpPage)}");
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

                    //Desired infromation to be stored
                    var userDetails = await _userService.GetUser(Email, response.Token);
                    var userInfoStr = JsonConvert.SerializeObject(userDetails);

                    //Add user preferences and securely store token
                    Preferences.Set(nameof(App.UserInfo), userInfoStr);
                    await SecureStorage.SetAsync(nameof(App.Token), response.Token);
                    await SecureStorage.SetAsync(nameof(App.RefreshToken), response.RefreshToken);

                    IsBusy = false;

                    await AppShell.Current.DisplayAlert("Login", "Login was Successfull!!!", "Ok");
                    await Shell.Current.GoToAsync($"{nameof(PushPage)}");

                }
                else
                {
                    IsBusy = false;
                    await AppShell.Current.DisplayAlert("Login", "Login was unsuccessfull!!!", "Ok");
                }
                
            }
            else
            {
                IsBusy = false;
                await AppShell.Current.DisplayAlert("Error", "Enter all fields", "Ok");
            }
            
        }
    }
}
