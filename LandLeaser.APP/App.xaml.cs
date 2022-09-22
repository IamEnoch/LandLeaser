using LandLeaserApp.Models;

namespace LandLeaser.APP
{
    public partial class App : Application
    {
        public UserBasicInfo UserInfo { get; set; }
        public string Token { get; set; }
        public App()
        {
            InitializeComponent();     

            MainPage = new AppShell();
        }
    }
}