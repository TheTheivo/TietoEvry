using System;
using System.Collections.Generic;
using System.Text;
using WeatherAPI.Deserializer;

namespace WeatherAPI.Models
{
    public class Astronomy
    {
        public DateTime Sunrise { get; private set; }
        public DateTime Sunset { get; private set; }
        public DateTime Moonrise { get; private set; }
        public DateTime Moonset { get; private set; }
        public string MoonPhase{ get; private set; }
        public double MoonIllumination { get; private set; }
        public Astronomy(AstronomyDeserializer root)
        {
            Sunrise = DateTime.Parse(root.astro.sunrise);
            Sunset = DateTime.Parse(root.astro.sunset);
            Moonrise = DateTime.Parse(root.astro.moonrise);
            Moonset = DateTime.Parse(root.astro.moonset);
            MoonPhase = root.astro.moon_phase;
            double moonIllumination;
            double.TryParse(root.astro.moon_illumination,out moonIllumination);
            MoonIllumination = moonIllumination;
        }
        public Astronomy()
        {

        }

        protected void UpdateAstronomy()
        {

        }
    }
}
