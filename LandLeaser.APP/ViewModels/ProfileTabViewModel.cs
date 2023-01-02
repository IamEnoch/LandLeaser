using CommunityToolkit.Mvvm.Input;
using LandLeaser.App.Views;

namespace LandLeaser.App.ViewModels
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
