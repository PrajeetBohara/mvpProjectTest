using Microsoft.Extensions.Logging;
using Dashboard.Services;

namespace Dashboard
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
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

    // Register services
    builder.Services.AddSingleton<WeatherService>();
    builder.Services.AddSingleton<EWeekGalleryService>();
    builder.Services.AddSingleton<FacultyService>();
    builder.Services.AddSingleton<ResearchImageService>();

            return builder.Build();
        }
    }
}
