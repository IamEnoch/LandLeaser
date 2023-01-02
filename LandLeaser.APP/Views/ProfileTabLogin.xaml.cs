using LandLeaser.App.ViewModels;

namespace LandLeaser.App.Views;

public partial class ProfileTabLogin : ContentPage
{
	public ProfileTabLogin(AppShellViewModel appShellViewModel)
	{
		InitializeComponent();
        BindingContext = appShellViewModel;
    }
}