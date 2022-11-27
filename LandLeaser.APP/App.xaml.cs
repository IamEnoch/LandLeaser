using LandLeaser.APP.ViewModels;
using LandLeaser.Shared.Models;

namespace LandLeaser.APP
{
    public partial class App : Application
    {
        public static UserBasicInfo UserInfo { get; set; }
        public static string Token { get; set; }
        public static  string RefreshToken { get; set; }
        private readonly AppShellViewModel _appShellViewModel;
        public App(AppShellViewModel appShellViewModel)
        {
            InitializeComponent();   
            _appShellViewModel = appShellViewModel;

            MainPage = new AppShell(_appShellViewModel);
        }
    }
}