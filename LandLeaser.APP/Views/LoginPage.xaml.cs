using LandLeaser.APP.ViewModels;

namespace LandLeaser.APP.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _loginViewModel;
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		BindingContext = loginViewModel;
        _loginViewModel = loginViewModel;


	}
    protected override async void OnAppearing()
    {
        if (BindingContext is LoginViewModel)
        {
            await _loginViewModel.CheckAsync();
        }
        base.OnAppearing();

    }
}