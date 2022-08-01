using MauiCustomTitleBar.Shared.ServiceInterfaces;
using MauiCustomTitleBar.Shared.Services;
using Microsoft.Maui.LifecycleEvents;
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
            })
            .ConfigureLifecycleEvents(lifecycle =>
            {
#if WINDOWS
                lifecycle.AddWindows(windows => windows.OnWindowCreated(window =>
                {
                    //window.ExtendsContentIntoTitleBar = true;
                    // This doesn't seem to make a difference
                }));
#endif
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddSingleton<ColorState>();

        builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();

        return builder.Build();
    }
}