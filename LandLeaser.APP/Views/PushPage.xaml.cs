using LandLeaserApp.ViewModels;

namespace LandLeaser.APP.Views;

public partial class PushPage : ContentPage
{
	public PushPage(PushPageViewModel pushPageViewModel)
	{
		InitializeComponent();
		BindingContext = pushPageViewModel;
	}
}