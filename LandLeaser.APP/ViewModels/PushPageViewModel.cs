using CommunityToolkit.Mvvm.Input;
using LandLeaser.APP;
using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaserApp.ViewModels
{
    public partial class PushPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Sign out command
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        async Task SignOut()
        {
            IsBusy = true;
            //Remove preferences
            Preferences.Remove(nameof(App.UserInfo));
            SecureStorage.RemoveAll();

            IsBusy = false;

            await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
        }
    }
}
