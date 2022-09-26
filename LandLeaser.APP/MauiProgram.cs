﻿using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using LandLeaserApp.Interfaces;
using LandLeaserApp.Services;
using LandLeaserApp.ViewModels;
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

            //Views
            builder.Services.AddSingleton<LoadingPage>();
            builder.Services.AddSingleton<SignUpPage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<PushPage>();

            //ViewModel
            builder.Services.AddSingleton<LoadingPageViewModel>();
            builder.Services.AddSingleton<SignUpViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<AppShellViewModel>();
            builder.Services.AddSingleton<BaseViewModel>();

            return builder.Build();
        }
    }
}