using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using System;
using System.Threading.Tasks;

namespace LandLeaser.APP
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel appShellViewModel)
        {
            InitializeComponent();
            BindingContext = appShellViewModel;

            //Explicit route registration
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
            Routing.RegisterRoute(nameof(ProfileTabLogin), typeof(ProfileTabLogin));


        }
        protected override void OnAppearing()
        {
            if(BindingContext is AppShellViewModel appShellViewModel) 
            {
                appShellViewModel.CheckAsync();
            }
            base.OnAppearing();

        }
    }
}