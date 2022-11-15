using CommunityToolkit.Mvvm.Input;
using LandLeaser.APP;
using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using LandLeaser.Shared.DTOs;
using LandLeaserApp.Interfaces;
using LandLeaserApp.Services;
using LandLeaserApp.Views;

namespace LandLeaserApp.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<GetListingDto>? Listing { get; } = new();
        private readonly IListingService _listingService;
        private readonly IRestService _restService;

        public HomePageViewModel(IListingService listingService, IRestService restService)
        {
            Title = nameof(HomePage);
            _listingService = listingService;
            _restService = restService;
            GetItems();
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

                //get listings from the api

                //var response = await check.GetListingsAsync(await SecureStorage.GetAsync(nameof(App.Token)));
                /*var response =
                    await _restService.GetItemsAsync<GetListingDto>(
                        await SecureStorage.GetAsync(nameof(App.Token)), "api/Listings");*/
                var response = await _listingService.GetListingsAsync(await SecureStorage.GetAsync(nameof(App.Token)));
                if (response.Item1 != null)
                {
                    foreach (var listing in response.Item1)
                    {
                        Listing.Add(listing);
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("ERROR", "Terrible error in the api call result", "OK");
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
