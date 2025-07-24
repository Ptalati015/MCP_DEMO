using System.Text.Json;
using Microsoft.Extensions.Configuration;
namespace SampleMcpServer.Services;
using Microsoft.Extensions.Logging;
public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(HttpClient httpClient, IConfiguration configuration, ILogger<WeatherService> logger)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenWeatherMap:ApiKey"] ?? throw new InvalidOperationException("OpenWeatherMap API key not configured");
        _logger = logger;
    }

  

    private static string GetRandomWeatherDescription()
    {
        var descriptions = new[] { "clear sky", "few clouds", "scattered clouds", "broken clouds", "shower rain", "rain", "thunderstorm", "snow", "mist" };
        return descriptions[Random.Shared.Next(descriptions.Length)];
    }
}

public record WeatherData
{
    public string City { get; init; } = string.Empty;
    public double Temperature { get; init; }
    public string Description { get; init; } = string.Empty;
    public int Humidity { get; init; }
    public double Pressure { get; init; }
    public double WindSpeed { get; init; }
    public DateTime Timestamp { get; init; }
}

// OpenWeatherMap API response models
public record OpenWeatherMapResponse
{
    public string Name { get; init; } = string.Empty;
    public MainData Main { get; init; } = new();
    public WeatherInfo[] Weather { get; init; } = Array.Empty<WeatherInfo>();
    public WindData? Wind { get; init; }
}

public record MainData
{
    public double Temp { get; init; }
    public int Humidity { get; init; }
    public double Pressure { get; init; }
}

public record WeatherInfo
{
    public string Description { get; init; } = string.Empty;
}

public record WindData
{
    public double Speed { get; init; }
}