using LandLeaser.App.ViewModels;

namespace LandLeaser.App.Views;

public partial class AddImagesPage : ContentPage
{
	public AddImagesPage(AddImagesViewModel addImagesViewModel)
	{
		InitializeComponent();
		BindingContext = addImagesViewModel;
	}
}