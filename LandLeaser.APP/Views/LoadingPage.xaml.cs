using LandLeaserApp.ViewModels;

namespace LandLeaserApp.Views;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel loadingPageViewModel)
	{
		InitializeComponent();
        BindingContext = loadingPageViewModel;
	}
}