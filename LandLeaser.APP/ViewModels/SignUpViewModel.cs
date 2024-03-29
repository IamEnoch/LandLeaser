﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LandLeaser.App.Interfaces;
using LandLeaser.App.Views;
using LandLeaser.Shared.Models;
using Newtonsoft.Json;

namespace LandLeaser.App.ViewModels
{
    public partial class SignUpViewModel : BaseViewModel
    {
        [ObservableProperty]
        string _firstName;

        [ObservableProperty]
        string _lastName;

        [ObservableProperty]
        string _email;

        [ObservableProperty]
        string _phoneNumber;

        [ObservableProperty]
        string _countryCode;

        [ObservableProperty]
        string _password;

        private readonly IRegisterService _registerService;
        private readonly IUserService _userService; 
        private readonly ILoginService _loginService;
        public SignUpViewModel(IRegisterService registerService, IUserService userService, ILoginService loginService)
        {
            Title = nameof(SignUpPage);
            _registerService = registerService;
            _userService = userService;
            _loginService = loginService;
        }

        [RelayCommand]
        public async Task DisplayLoginPage()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}", true);
        }

        [RelayCommand]
        async Task Register()
        {
            IsBusy = true;
            //Check whether string is null or empty for both fields
            if (!String.IsNullOrEmpty(_email) && !String.IsNullOrEmpty(_password) && !String.IsNullOrEmpty(_phoneNumber)
                && !String.IsNullOrEmpty(_firstName) && !String.IsNullOrEmpty(_lastName) && !String.IsNullOrEmpty(_countryCode))
            {
                //Call the register method from the register service
                var result = await _registerService.CreateUser(new Register
                {
                    FirstName = _firstName,
                    LastName = _lastName,
                    EmailAddress = _email,
                    PhoneNumeber = _countryCode + _phoneNumber,
                    Password = _password
                });

                if (result == true)
                {
                    var loginResult = await _loginService.Authenticate(new LoginRequest
                    {
                        EmailAddress = _email,
                        Password = _password,
                    });

                    if (loginResult != null)
                    {
                        //Set the neceessary credentials
                        //Remove existing preference details
                        if (Preferences.ContainsKey(nameof(App.UserInfo)))
                        {
                            Preferences.Remove(nameof(App.UserInfo));
                        }

                        //Desired infromation to be stored
                        var userDetails = await _userService.GetUser(_email, loginResult.LoginResult.Token);
                        var userInfoStr = JsonConvert.SerializeObject(userDetails);

                        //Add user preferences and securely store token
                        Preferences.Set(nameof(App.UserInfo), userInfoStr);
                        await SecureStorage.SetAsync(nameof(App.Token), loginResult.LoginResult.Token);
                        await SecureStorage.SetAsync(nameof(App.RefreshToken), loginResult.LoginResult.RefreshToken);

                        IsBusy = false; ;

                        await Shell.Current.DisplayAlert("Register", "Registration was Successfull!!!", "Ok");
                        await Shell.Current.GoToAsync($"///{nameof(HomePage)}");
                    }
                    //Login after registration unsuccessful
                    else
                    {
                        IsBusy = false;
                        await Shell.Current.DisplayAlert("Register", "Registration was successfull login unsuccessfull!!!", "Ok");
                    }
                }

                //Api call = User not created
                else
                {
                    IsBusy = false;
                    await Shell.Current.DisplayAlert("Register", "Registration was unsuccessfull!!!", "Ok");
                }

            }
            else
            {
                IsBusy = false;
                await Shell.Current.DisplayAlert("Error", "Enter all fields", "Ok");
            }

        }


    }
}
