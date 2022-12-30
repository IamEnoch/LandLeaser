using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using System;
using System.Threading.Tasks;

namespace LandLeaser.APP
{
    public partial class AppShell : Shell
    {
        public readonly AppShellViewModel _appShellViewModel;

        public AppShell(AppShellViewModel appShellViewModel)
        {
            InitializeComponent();
            _appShellViewModel = appShellViewModel;

            BindingContext = _appShellViewModel;

            //Explicit route registration
            Routing.RegisterRoute(nameof(AddListingPage), typeof(AddListingPage));
            Routing.RegisterRoute(nameof(LogoutPage), typeof(LogoutPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(AddListingPage), typeof(AddListingPage));
            
        }
        protected override async void OnAppearing()
        {
            await _appShellViewModel.CheckAsync();
            await _appShellViewModel.TabState();
            /*f(BindingContext is AppShellViewModel)
            { 
                await _appShellViewModel.CheckAsync();
                _appShellViewModel.TabState();
            }*/
            base.OnAppearing();

        }
    }
}