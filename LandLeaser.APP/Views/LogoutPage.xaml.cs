using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LandLeaser.APP.ViewModels;

namespace LandLeaser.APP.Views;

public partial class LogoutPage : ContentPage
{
    private readonly AppShell _appShell;
    public LogoutPage(LogoutViewModel logoutViewModel, AppShell appShell)
    {
        InitializeComponent();
        BindingContext = logoutViewModel;
        _appShell = appShell;
    }

    protected override void OnAppearing()
    {
        Preferences.Remove(nameof(App.UserInfo));
        SecureStorage.RemoveAll();

        App.Current.MainPage = _appShell;
        base.OnAppearing();
    }
}
