using LandLeaser.App.ViewModels;

namespace LandLeaser.App.Views;

public partial class ProfileTabLoggedIn : ContentPage
{
	public ProfileTabLoggedIn(AppShellViewModel appShellViewModel)
	{
		InitializeComponent();
        BindingContext = appShellViewModel;
    }
}