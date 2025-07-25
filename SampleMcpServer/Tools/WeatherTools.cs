using System.ComponentModel;
using ModelContextProtocol.Server;
using SampleMcpServer.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SampleMcpServer.Tools;




public class WeatherTools
{
    private readonly WeatherService _weatherService;
    private readonly ILogger<WeatherTools> _logger;
    public WeatherTools(WeatherService weatherService, ILogger<WeatherTools> logger)
    {
        _weatherService = weatherService;
        _logger = logger;
    }

    [McpServerTool]
    [Description("Describes random weather in the provided city.")]
    public async Task<string> GetCityWeather(
        [Description("Name of the city to return weather for")] string city)
    {
        var weather =  await _weatherService.GetCurrentWeatherAsync(city);
        
        if (weather == null)
        {
            return $"Unable to get weather information for {city}. Please check the city name.";
        }

        return $"Current weather in {weather.City}: {weather.Temperature:F1}°C, {weather.Description}. " +
               $"Humidity: {weather.Humidity}%, Pressure: {weather.Pressure:F0} hPa, Wind: {weather.WindSpeed:F1} m/s";
    }
}