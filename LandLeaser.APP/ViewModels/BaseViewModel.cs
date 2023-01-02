using CommunityToolkit.Mvvm.ComponentModel;

namespace LandLeaser.App.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool _isBusy;

        [ObservableProperty]
        string _title;
        
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotLoggedIn))]
        bool _isLoggedIn;

        public bool IsNotBusy => !IsBusy;
        public bool IsNotLoggedIn => !IsLoggedIn;
    }
}
