using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SampleMcpServer.Services.Models;
namespace SampleMcpServer.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    private readonly string _weatherApiBaseUrl;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(HttpClient httpClient, IConfiguration configuration, ILogger<WeatherService> logger)
    {
        _httpClient = httpClient;
        _apiKey = configuration["WEATHER_SERVICE_API_KEY"] ?? throw new InvalidOperationException("API key not configured");
        _logger = logger;
        _weatherApiBaseUrl= configuration["WEATHER_SERVICE_URL"] ?? throw new InvalidOperationException("Weather service URL not configured");
    }

    public async Task<WeatherData?> GetCurrentWeatherAsync(string city)
    {
        try
        {
            var url = $"{_weatherApiBaseUrl}/current.json?key={_apiKey}&q={Uri.EscapeDataString(city)}";
            var response = await _httpClient.GetStringAsync(url);
            var weatherResponse = JsonSerializer.Deserialize<WeatherApiResponse>(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (weatherResponse == null) return null;

            return new WeatherData
            {
                City = weatherResponse.location.name,
                Temperature = weatherResponse.current.temp_c,
                Description = weatherResponse.current.condition.text,
                Humidity = weatherResponse.current.humidity,
                Pressure = (int)weatherResponse.current.pressure_mb,
                WindSpeed = weatherResponse.current.wind_kph,
                Timestamp = DateTime.UtcNow
            };
           

            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get weather for city: {City}", city);
            return null;
        }
    }

    public class WeatherData
    {
        public string? City { get; set; }
        public double Temperature { get; set; }
        public string? Description { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double WindSpeed { get; set; }
        public DateTime Timestamp { get; set; }
    }

    
}
