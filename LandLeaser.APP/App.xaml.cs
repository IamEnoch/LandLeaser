using LandLeaser.APP.ViewModels;
using LandLeaser.Shared.Models;

namespace LandLeaser.APP
{
    public partial class App : Application
    {
        public static UserBasicInfo UserInfo { get; set; }
        public static bool IsLoggedIn { get; set; }
        public static string Token { get; set; }
        public static  string RefreshToken { get; set; }
        private readonly AppShellViewModel _appShellViewModel;
        private readonly ProfileTabViewModel _profileTabViewModel;
        public App(AppShellViewModel appShellViewModel, ProfileTabViewModel profileTabViewModel)
        {
            InitializeComponent();   
            _appShellViewModel = appShellViewModel;
            _profileTabViewModel = profileTabViewModel;

            MainPage = new AppShell(appShellViewModel, profileTabViewModel);
        }

        
    }
}