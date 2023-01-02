using LandLeaser.App.ViewModels;

namespace LandLeaser.App.Views;

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
