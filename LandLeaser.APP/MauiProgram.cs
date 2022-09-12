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

            return builder.Build();
        }
    }
}