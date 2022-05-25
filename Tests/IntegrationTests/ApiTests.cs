using NUnit.Framework;

namespace Tests.IntegrationTests
{
    class ApiTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetForecastTest()
        {
            WeatherAPI.API.WeatherApi.GetForecast("London", 2).Wait();
        }

        public void GetRealTimeWeatherTest()
        {
            WeatherAPI.API.WeatherApi.GetRealTimeWeather("London").Wait(); ;
            
        }

        public void GetAstronomyTest()
        {
            WeatherAPI.API.WeatherApi.GetAstronomy("London").Wait();
        }

        public void GetTimeZoneTest()
        {
            WeatherAPI.API.WeatherApi.GetForecast("London").Wait();
        }
    }
}
