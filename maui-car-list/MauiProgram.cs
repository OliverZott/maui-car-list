using maui_car_list.Services;
using maui_car_list.ViewModels;
using maui_car_list.Views;
using Microsoft.Extensions.Logging;

namespace maui_car_list;

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



        // db setup ensures that CarService is created with dbPath and is available for DI throughout MAUI app
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "cars.db3");
        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<CarService>(s, dbPath));

        builder.Services.AddTransient<CarApiService>();  // because each time we open connection, we want new object

        builder.Services.AddSingleton<CarListViewModel>();
        builder.Services.AddTransient<CarDetailsViewModel>();  // transient because we want new instance of page every time!
        builder.Services.AddTransient<CarEditViewModel>();  // transient because we want new instance of page every time!

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<CarDetailsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
