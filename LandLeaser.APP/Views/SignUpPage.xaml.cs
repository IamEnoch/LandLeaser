using LandLeaser.App.ViewModels;

namespace LandLeaser.App.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpViewModel signUpViewModel)
	{
		InitializeComponent();
		BindingContext = signUpViewModel;
		
	}
}