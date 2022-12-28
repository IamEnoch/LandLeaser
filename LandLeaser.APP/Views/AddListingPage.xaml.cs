using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandLeaser.APP.ViewModels;

namespace LandLeaser.APP.Views;

public partial class AddListingPage : ContentPage
{
    public AddListingPage(AddListingViewModel addListingViewModel)
    {
        BindingContext = addListingViewModel;
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync("..");
        return base.OnBackButtonPressed();
    }
}