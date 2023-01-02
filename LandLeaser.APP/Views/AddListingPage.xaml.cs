using LandLeaser.App.ViewModels;

namespace LandLeaser.App.Views;

public partial class AddListingPage : ContentPage
{
    public AddListingPage(AddListingViewModel addListingViewModel)
    {
        BindingContext = addListingViewModel;
        InitializeComponent();
    }

}