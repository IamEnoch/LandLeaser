using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using LandLeaser.APP.Interfaces;
using LandLeaser.APP.Views;
using LandLeaser.Shared.DTOs;

namespace LandLeaser.APP.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<GetListingDto>? Listing { get; } = new();
        private readonly IListingService _listingService;


        public HomePageViewModel(IListingService listingService)
        {
            Title = nameof(HomePage);
            _listingService = listingService;
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
                if (response != null)
                {
                    foreach (var listing in response)
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
