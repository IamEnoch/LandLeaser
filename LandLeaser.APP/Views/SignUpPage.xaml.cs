using LandLeaser.APP.ViewModels;

namespace LandLeaserApp.Views;

public partial class SignUpPage : ContentPage
{
	public SignUpPage(SignUpViewModel signUpViewModel)
	{
		InitializeComponent();
		BindingContext = signUpViewModel;
	}
}