using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandLeaser.APP.ViewModels
{
    public partial class LogoutViewModel : BaseViewModel
    {
        private readonly AppShell _appShell;
        public LogoutViewModel(AppShell appShell)
        {
            _appShell = appShell;
        }

        [RelayCommand]
        public void Logout()
        {
            Preferences.Remove(nameof(App.UserInfo));
            SecureStorage.RemoveAll();

            App.Current.MainPage = _appShell;
            
        }
    }
}
