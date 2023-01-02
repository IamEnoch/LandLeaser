using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LandLeaser.App.Interfaces;
using LandLeaser.App.Views;
using LandLeaser.Shared.DTOs;

namespace LandLeaser.App.ViewModels
{
    public partial class AddListingViewModel : BaseViewModel
    {
        private readonly IListingService _listingService;

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

        [ObservableProperty] 
        private bool _isValid;

        public AddListingViewModel(IListingService listingService)
        {
            _listingService = listingService;
        }

        [RelayCommand]
        async Task GoToAddImagesAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddImagesPage));   
        }

        [RelayCommand]
        async Task SubmitListing()
        {
            //Check if all section have been filled
            if (!String.IsNullOrEmpty(LandLocation)
                && !String.IsNullOrEmpty(LandSize)
                && !String.IsNullOrEmpty(LandCost)
                && !String.IsNullOrEmpty(LeaseDuration)
                && !String.IsNullOrEmpty(Description)
                && !String.IsNullOrEmpty(PreferredCrop))
            {
                IsValid = true;

                var authToken = await SecureStorage.GetAsync(nameof(App.Token));
                var userId = await SecureStorage.GetAsync("userId");
                IList<ListingImageDto> item = new List<ListingImageDto>();

                var listing = new CreateListingDto()
                {
                    AppUserId = Guid.Parse(userId),
                    Cost = LandCost,
                    Description = Description,
                    Duration = LeaseDuration,
                    Location = LandLocation,
                    Images = item,
                    Size = LandSize
                };

                var response = await _listingService.PostListingAsync(authToken, listing);

                if (response != null)
                {
                    await Shell.Current.DisplayAlert("Success", $"Land at {response.Location} posted for leasing",
                        "Ok");

                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", $"Land at {response.Location} not posted for leasing",
                        "Ok");
                }
            }
        }
    }
}
