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
        

        public App(AppShell appShell)
        {
            InitializeComponent();

            MainPage = appShell;
        }
    }
}