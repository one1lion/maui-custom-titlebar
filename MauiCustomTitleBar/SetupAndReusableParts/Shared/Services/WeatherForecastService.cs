using MauiCustomTitleBar.Shared.Data;
using MauiCustomTitleBar.Shared.ServiceInterfaces;

namespace MauiCustomTitleBar.Shared.Services;

// Note: I would normally have a Service implementations in an appropriate library.  Perhaps
//       one that uses a HTTP Client to do an API call, or one that communicates directly with
//       a database, etc.  For simplicity, all of the applications that use this library
//       use the same service implementation
public class WeatherForecastService : IWeatherForecastService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray());
    }
}