using CommunityToolkit.Mvvm.ComponentModel;

namespace LandLeaser.APP.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotLoggedIn))]
        bool isLoggedIn;

        bool IsNotBusy => !IsBusy;
        bool IsNotLoggedIn => !IsLoggedIn;
    }
}
