using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LandLeaser.APP.ViewModels
{
    public partial class AddListingViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _landLocation;

        [ObservableProperty]
        private string _landSize;

        [ObservableProperty]
        private string _sizeMetric;

        [ObservableProperty]
        private string _landCost;

        [ObservableProperty]
        private string _currency;

        [ObservableProperty]
        private string _leaseDuration;

        [ObservableProperty]
        private string _durationMetric;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private string _preferredCrop;

        
    }
}
