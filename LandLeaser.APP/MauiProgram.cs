using LandLeaser.App.Interfaces;
using LandLeaser.App.Services;
using LandLeaser.App.ViewModels;
using LandLeaser.App.Views;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace LandLeaser.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("TitilliumWeb-Regular.ttf", "TitilliumWebRegular");
                    fonts.AddFont("TitilliumWeb-SemiBold.ttf", "TitilliumWebSemibold");
                });

            //Services
            builder.Services.AddSingleton<ILoginService, LoginService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IRegisterService, RegisterService>();
            builder.Services.AddSingleton<IListingService, ListingService>();
            builder.Services.AddSingleton<IRestService, RestService>();

            //Views
            builder.Services.AddTransient<AddImagesPage>();
            builder.Services.AddTransient<AddListingPage>();
            builder.Services.AddTransient<LogoutPage>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<SignUpPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<ProfileTabLoggedIn>();
            builder.Services.AddTransient<ProfileTabLogin>();

            //ViewModel
            builder.Services.AddTransient<AddImagesViewModel>();
            builder.Services.AddTransient<AddListingViewModel>();
            builder.Services.AddTransient<LogoutViewModel>();
            builder.Services.AddTransient<ProfileTabViewModel>();
            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<SignUpViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<BaseViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}