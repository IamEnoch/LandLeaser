using LandLeaser.APP.Views;
using LandLeaserApp.ViewModels;

namespace LandLeaser.APP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();

            //Explicit route registration
            
            Routing.RegisterRoute(nameof(PushPage), typeof(PushPage));
        }
    }
}