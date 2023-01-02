using CommunityToolkit.Mvvm.Input;

namespace LandLeaser.App.ViewModels
{
    public partial class AddImagesViewModel : BaseViewModel
    {
        [RelayCommand]
        public async Task Prev()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
