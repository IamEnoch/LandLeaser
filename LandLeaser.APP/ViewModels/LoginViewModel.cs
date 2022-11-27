﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        public LoginViewModel(ILoginService loginService, IUserService userService )
        {
            Title = nameof(LoginPage);
            _loginService = loginService;
            _userService = userService;
        }

        [RelayCommand]
        public async Task DisplaySignUpPage()
        {
            await Shell.Current.GoToAsync($"///{nameof(SignUpPage)}");
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

                    await AppShell.Current.DisplayAlert("Login", "Login was Successful!!!", "Ok");
                    await Shell.Current.GoToAsync($"{nameof(HomePage)}");

                }
                else
                {
                    IsBusy = false;
                    await AppShell.Current.DisplayAlert("Login", "Login was unsuccessful!!!", "Ok");
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
