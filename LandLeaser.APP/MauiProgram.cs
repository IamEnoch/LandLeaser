﻿using LandLeaser.APP.ViewModels;
using LandLeaser.APP.Views;
using LandLeaserApp.ViewModels;

namespace LandLeaser.APP
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("TitilliumWeb-Regular.ttf", "TitilliumWebRegular");
                    fonts.AddFont("TitilliumWeb-SemiBold.ttf", "TitilliumWebSemibold");
                });
            //Services
            //Views
            builder.Services.AddSingleton<SignUpPage>();

            //ViewModel
            builder.Services.AddSingleton<SignUpViewModel>();
            builder.Services.AddSingleton<AppShellViewModel>();

            return builder.Build();
        }
    }
}