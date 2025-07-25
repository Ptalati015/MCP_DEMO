using System;
using System.Net.Http;

namespace SampleMcpServer.Services.Models
{
    public class WeatherApiResponse
    {
        public Location? location { get; set; }
        public Current? current { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
        public string region { get; set; }
        public string country { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string tz_id { get; set; }
        public string localtime { get; set; }
    }

    public class Current
    {
        public double temp_c { get; set; }
        public int humidity { get; set; }
        public double pressure_mb { get; set; }
        public double wind_kph { get; set; }
        public Condition condition { get; set; }
    }

    public class Condition
    {
        public string text { get; set; }
        public string icon { get; set; }
    }
}