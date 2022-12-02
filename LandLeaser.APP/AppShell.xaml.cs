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
            Routing.RegisterRoute(nameof(LogoutPage), typeof(LogoutPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(ProfileTabLogin), typeof(ProfileTabLogin));
            
        }
        protected override async void OnAppearing()
        {

            if(BindingContext is AppShellViewModel)
            {
                _appShellViewModel.CheckAsync();
                await _appShellViewModel.TabStateAsync();
            }
            base.OnAppearing();

        }
    }
}