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
        public void Test1()
        {
            WeatherAPI.API.WeatherApi.GetForecast("London", 2).Wait();
            var tt = 1;
        }
    }
}
