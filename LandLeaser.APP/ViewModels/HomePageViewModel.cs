﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using LandLeaser.App.Interfaces;
using LandLeaser.App.Views;
using LandLeaser.Shared.DTOs;

namespace LandLeaser.App.ViewModels
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

        public void IsLoggedInState() => IsLoggedIn = App.IsLoggedIn;

        /// <summary>
        /// Get a list of listings by making an api call
        /// </summary>
        /// <returns></returns>
        public async Task GetItemsAsync()
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

            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        [RelayCommand]
        public async Task AddListingAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddListingPage)}");
        }
    }
}
