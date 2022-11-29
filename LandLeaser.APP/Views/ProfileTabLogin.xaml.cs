using LandLeaser.APP.ViewModels;

namespace LandLeaser.APP.Views;

public partial class ProfileTabLogin : ContentPage
{
	public ProfileTabLogin(AppShellViewModel appShellViewModel)
	{
		InitializeComponent();
        BindingContext = appShellViewModel;
    }
}