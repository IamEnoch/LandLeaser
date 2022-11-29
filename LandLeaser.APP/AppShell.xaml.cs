using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using System;
using System.Threading.Tasks;

namespace LandLeaser.APP
{
    public partial class AppShell : Shell
    {
        private AppShellViewModel _appShellViewModel;
        private ProfileTabViewModel _profileTabViewModel;
        //private AppShell _appShell;

        public AppShell(AppShellViewModel appShellViewModel, ProfileTabViewModel profileTabViewModel)
        {
            InitializeComponent();
            _appShellViewModel = appShellViewModel;
            _profileTabViewModel = profileTabViewModel;

            BindingContext = _appShellViewModel;

            //Explicit route registration
            Routing.RegisterRoute(nameof(LogoutPage), typeof(LogoutPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(ProfileTabLogin), typeof(ProfileTabLogin));

            //_appShellViewModel.StateChanged += StateChanged;
        }
        
        /*void StateChanged(object sender, EventArgs e)
        {
            void Check(AppShell appShell)
            {
                _appShell = appShell;
            }

            App.Current.MainPage = _appShell;
        }*/
        protected override async void OnAppearing()
        {
            if(BindingContext is AppShellViewModel) 
            {
                await _appShellViewModel.CheckAsync();
            }
            base.OnAppearing();

        }
    }
}