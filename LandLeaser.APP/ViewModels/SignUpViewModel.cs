using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LandLeaser.APP.Views;
using LandLeaser.Shared.Models;
using LandLeaserApp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaser.APP.ViewModels
{
    public partial class SignUpViewModel : BaseViewModel
    {
        private readonly IRegisterService _registerService;
        private readonly IUserService _userService;
        public SignUpViewModel(IRegisterService registerService)
        {
            Title = nameof(SignUpPage);
            _registerService = registerService;
        }

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

                if (result != null)
                {
                    //Set the neceessary credentials
                    //Remove existing preference details
                    if (Preferences.ContainsKey(nameof(App.UserInfo)))
                    {
                        Preferences.Remove(nameof(App.UserInfo));
                    }

                    //Desired infromation to be stored
                    var userDetails = await _userService.GetUser(Email, result.Token);
                    var userInfoStr = JsonConvert.SerializeObject(userDetails);

                    //Add user preferences and securely store token
                    Preferences.Set(nameof(App.UserInfo), userInfoStr);
                    await SecureStorage.SetAsync(nameof(App.Token), result.Token);
                    await SecureStorage.SetAsync(nameof(App.RefreshToken), result.RefreshToken);

                    IsBusy = false; ;

                    await AppShell.Current.DisplayAlert("Register", "Registration was Successfull!!!", "Ok");
                    await Shell.Current.GoToAsync($"{nameof(PushPage)}");

                }
                else
                {
                    IsBusy = false;
                    await AppShell.Current.DisplayAlert("Register", "Registration was unsuccessfull!!!", "Ok");
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
