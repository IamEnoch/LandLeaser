using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using LandLeaserApp.Helpers;
using LandLeaserApp.Interfaces;
using LandLeaserApp.Services;
using LandLeaserApp.ViewModels;
using LandLeaserApp.Views;
using Microsoft.Extensions.DependencyInjection;

namespace LandLeaser.APP
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
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
            builder.Services.AddSingleton<LoadingPage>();
            builder.Services.AddTransient<SignUpPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomePage>();

            //ViewModel
            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddSingleton<LoadingPageViewModel>();
            builder.Services.AddSingleton<SignUpViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddSingleton<BaseViewModel>();
            

            return builder.Build();
        }
    }
}