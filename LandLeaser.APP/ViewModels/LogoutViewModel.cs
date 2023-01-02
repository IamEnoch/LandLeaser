using CommunityToolkit.Mvvm.Input;

namespace LandLeaser.App.ViewModels
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
