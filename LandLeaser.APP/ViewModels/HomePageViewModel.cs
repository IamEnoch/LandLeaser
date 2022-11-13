using CommunityToolkit.Mvvm.Input;
using LandLeaser.APP;
using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using LandLeaser.Shared.DTOs;
using LandLeaserApp.Interfaces;
using LandLeaserApp.Views;

namespace LandLeaserApp.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<GetListingDto>? Listing { get; } = new();
        private readonly IListingService _listingService;

        public HomePageViewModel(IListingService listingService)
        {
            Title = nameof(HomePage);
            _listingService = listingService;
        }
        

        async Task GetItems()
        {
            if(IsBusy)
                return;

            try
            {
                if (Listing?.Count > 0)
                {
                    Listing.Clear();
                }

                IsBusy = true;

                //ge listings from the api

                //var response = await _listingService.GetListingsAsync();

                /*if (response != null) ;
                {
                    foreach (var listing in await _listingService.GetListingsAsync())
                    {
                        Listing.Add(listing);
                    }
                }*/
                foreach (var listing in await _listingService.GetListingsAsync())
                {
                    Listing.Add(listing);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", $"An error has occured: {ex.Message}", "Ok");
            }
            finally
            {
                IsBusy = false;
            }



        }
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
