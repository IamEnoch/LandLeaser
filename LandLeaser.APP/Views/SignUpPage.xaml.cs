using LandLeaser.APP.ViewModels;

namespace LandLeaser.APP.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpViewModel signUpViewModel)
	{
		InitializeComponent();
		BindingContext = signUpViewModel;
		
	}
}