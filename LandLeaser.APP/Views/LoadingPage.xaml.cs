using Android.Content;
using LandLeaserApp.ViewModels;

namespace LandLeaser.APP.Views;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel loadingPageViewModel)
	{
		InitializeComponent();
		BindingContext = loadingPageViewModel;
	}
}