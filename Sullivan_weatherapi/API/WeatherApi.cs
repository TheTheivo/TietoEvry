using WeatherAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using WeatherAPI.Deserializer;
using WeatherAPI.Deserializer.WeatherAPI.Deserializer;

namespace WeatherAPI.API
{
    public static class WeatherApi
    {
        static HttpClient client = new HttpClient();

        public static async Task<Location> GetRealTimeWeather(string city)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://weatherapi-com.p.rapidapi.com/current.json?q={city}"),
                Headers =
        {
            { "X-RapidAPI-Host", Constants.XRapidAPIHost },
            { "X-RapidAPI-Key", Constants.XRapidAPIKey },
        },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var TimeZoneJson = JsonSerializer.Deserialize<RealTimeRoot>(body);
                var timezone = new Location(TimeZoneJson);

                return timezone;
            }

        }

        public static async Task<Models.Location> GetAstronomy(string city)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://weatherapi-com.p.rapidapi.com/astronomy.json?q={city}"),
                Headers =
    {
        { "X-RapidAPI-Host", Constants.XRapidAPIHost },
        { "X-RapidAPI-Key", Constants.XRapidAPIKey },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var TimeZoneJson = JsonSerializer.Deserialize<AstroRoot>(body);
                var timezone = new Location(TimeZoneJson);

                return timezone;
            }
        }

        public static async Task<Location> GetTimeZone(string city)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://weatherapi-com.p.rapidapi.com/timezone.json?q={city}"),
                    Headers =
    {
        { "X-RapidAPI-Host", Constants.XRapidAPIHost },
        { "X-RapidAPI-Key", Constants.XRapidAPIKey },
    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var TimeZoneJson = JsonSerializer.Deserialize<LocationRoot>(body);
                    var timezone = new Location(TimeZoneJson);

                    return timezone;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not call {nameof(GetTimeZone)}Error:{e.Message}");
            }
            return new Location();
        }

        public static async Task<ForecastRoot> GetForecast(string city, int? days =null)
        {
            Uri uri = new Uri("https://weatherapi-com.p.rapidapi.com/forecast.json?q={city}") ;
            if(days == null)
            {
                uri = new Uri($"https://weatherapi-com.p.rapidapi.com/forecast.json?q={city}");
            }
            else
            {
                uri = new Uri($"https://weatherapi-com.p.rapidapi.com/forecast.json?q={city}&days={days}");
            }
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri,
                Headers =
    {
        { "X-RapidAPI-Host", Constants.XRapidAPIHost },
        { "X-RapidAPI-Key", Constants.XRapidAPIKey },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var forecastRoot = JsonSerializer.Deserialize<ForecastRoot>(body);
                return forecastRoot;
            }
        }

        public static async Task<Location> GetAll(string city)
        {
            try
            {
                var location = await GetTimeZone(city);
                var astronomy = await GetAstronomy(city);
                var realTimeWeather = await GetRealTimeWeather(city);
                return new Location(location, realTimeWeather.Weather, astronomy.Astronomy);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not call {nameof(GetTimeZone)}Error:{e.Message}");
            }
            return new Location();
        }

    }
}
