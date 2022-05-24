using System;
using System.Collections.Generic;
using System.Text;
using WeatherAPI.Deserializer.WeatherAPI.Deserializer;

namespace WeatherAPI.Deserializer
{
    public class AstronomyDeserializer
    {
        public Astro astro { get; set; }
    }

    public class Astro
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string moonrise { get; set; }
        public string moonset { get; set; }
        public string moon_phase { get; set; }
        public string moon_illumination { get; set; }
    }

    public class AstroRoot
    {
        public LocationDeserializer location { get; set; }
        public AstronomyDeserializer astronomy { get; set; }
    }
}
