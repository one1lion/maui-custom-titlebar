using MauiCustomTitleBar.Shared.Data;

namespace MauiCustomTitleBar.Shared.ServiceInterfaces;

public interface IWeatherForecastService
{
    Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
}