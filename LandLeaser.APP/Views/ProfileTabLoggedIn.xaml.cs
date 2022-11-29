using LandLeaser.APP.ViewModels;

namespace LandLeaser.APP.Views;

public partial class ProfileTabLoggedIn : ContentPage
{
	public ProfileTabLoggedIn(AppShellViewModel appShellViewModel)
	{
		InitializeComponent();
        BindingContext = appShellViewModel;
    }
}