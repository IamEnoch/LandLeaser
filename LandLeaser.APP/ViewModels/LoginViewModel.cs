using Android.OS;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LandLeaser.API.ViewModel;
using LandLeaser.APP;
using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using LandLeaserApp.Interfaces;
using LandLeaserApp.Services;
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
        private readonly UserManager _userManager;

        public LoginViewModel(ILoginService loginService, UserManager userManager)
        {
            Title = nameof(LoginPage);
            _loginService = loginService;
            _userManager = userManager;
        }

        [RelayCommand]
        public async Task DisplaySignUpPage()
        {
            await Shell.Current.GoToAsync("///SignUpPage");
        }

        [RelayCommand]
        async void Login()
        {
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
                    
                    await AppShell.Current.DisplayAlert("Login", "Login was Successfull!!!", "Ok");

                    //Set the neceessary credentials
                    //Remove existing preference details
                    if (Preferences.ContainsKey(nameof(App.UserInfo)))
                    {
                        Preferences.Remove(nameof(App.UserInfo));
                    }

                    //Desired infromation to be stored
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Login", "Login was unsuccessfull!!!", "Ok");
                }
            }
            else
            {
                await AppShell.Current.DisplayAlert("Error", "Enter all fields", "Ok");
            }
        }
    }
}
