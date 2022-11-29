using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandLeaser.APP.ViewModels;

namespace LandLeaser.APP.Views;

public partial class LogoutPage : ContentPage
{
    public LogoutPage(LogoutViewModel logoutViewModel)
    {
        InitializeComponent();
        BindingContext = logoutViewModel;
    }
}