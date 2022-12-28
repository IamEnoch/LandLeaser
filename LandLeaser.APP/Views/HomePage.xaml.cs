using LandLeaser.APP.ViewModels;
namespace LandLeaser.APP.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel _homePageViewModel;
    private readonly AppShellViewModel _appShellViewModel;
	public HomePage(HomePageViewModel homePageViewModel, AppShellViewModel appShellViewModel)
	{
		InitializeComponent();
		BindingContext = homePageViewModel;
        _homePageViewModel = homePageViewModel;
        _appShellViewModel = appShellViewModel;
        _homePageViewModel.IsLoggedInState();
    }

    protected override async void OnAppearing()
    {
        await _appShellViewModel.CheckAsync();
        _homePageViewModel.IsLoggedInState();
        await _homePageViewModel.GetItemsAsync();
        base.OnAppearing();
    }
}