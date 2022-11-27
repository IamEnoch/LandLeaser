using LandLeaser.APP.ViewModels;

namespace LandLeaser.APP.Views;

public partial class ProfileTabLoggedIn : ContentPage
{
	public ProfileTabLoggedIn(ProfileTabViewModel profileTabViewModel)
	{
		InitializeComponent();
        BindingContext = profileTabViewModel;
    }
}