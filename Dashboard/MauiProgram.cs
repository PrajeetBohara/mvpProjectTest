using Microsoft.Extensions.Logging;
using Dashboard.Services;
using Dashboard.Pages;

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
    builder.Services.AddSingleton<FloorPlanService>();
    builder.Services.AddSingleton<HomePageImageService>();
    builder.Services.AddSingleton<StudentClubService>();
    builder.Services.AddSingleton<AcademicProgramService>();
    
    // Register pages that require dependency injection
    builder.Services.AddTransient<DepartmentMapPage>();
    builder.Services.AddTransient<HomePage>();
    builder.Services.AddTransient<StudentClubsPage>();
    builder.Services.AddTransient<StudentClubDetailPage>();
    builder.Services.AddTransient<AcademicCataloguePage>();
    builder.Services.AddTransient<DepartmentConcentrationsPage>();
    builder.Services.AddTransient<AcademicProgramDetailPage>();

            return builder.Build();
        }
    }
}
