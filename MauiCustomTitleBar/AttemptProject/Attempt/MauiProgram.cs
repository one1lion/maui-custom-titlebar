using MauiCustomTitleBar.Shared.ServiceInterfaces;
using MauiCustomTitleBar.Shared.Services;
using MauiCustomTitleBar.Shared.State;

namespace MauiCustomTitleBar.Attempt;

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
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
        builder.Services.AddSingleton<ColorState>();
        return builder.Build();
    }
}