using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LandLeaser.APP.Views;

namespace LandLeaser.APP.ViewModels
{
    public partial class ProfileTabViewModel : BaseViewModel
    {
        [RelayCommand]
        public async Task GoToLoginAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
        
        [RelayCommand]
        public async Task LogoutAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(ProfileTabLogin)}");
        }
    }
}
