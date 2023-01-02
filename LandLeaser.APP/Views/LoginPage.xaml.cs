using LandLeaser.App.ViewModels;

namespace LandLeaser.App.Views;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _loginViewModel;
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		BindingContext = loginViewModel;
        _loginViewModel = loginViewModel;

    }
}