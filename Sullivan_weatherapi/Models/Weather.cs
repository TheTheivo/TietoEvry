using System;
using System.Collections.Generic;
using System.Text;
using WeatherAPI.Deserializer;

namespace WeatherAPI.Models
{
    public class Weather
    {
        public int LastUpdatedEpoch { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public double TemperatureCelsius { get; private set; }
        public double TemperatureFahrenheit { get; private set; }
        public bool IsDay{ get; private set; }
        public double WindMPH { get; private set; }
        public double WindKPH { get; private set; }
        public int WindDegress { get; private set; }
        public string WindDirection { get; private set; }
        public double PressureMb { get; private set; }
        public double PressureIn{ get; private set; }
        public double PrecipMM { get; private set; }
        public double PrecipIn { get; private set; }
        public double Humidity { get; private set; }
        public double Cloud{ get; private set; }
        public double FellsLikeCelsius { get; private set; }
        public double FellsLikeFahrenheit { get; private set; }
        public double VisibilityKm { get; private set; }
        public double VisibilityMiles { get; private set; }
        public double UV { get; private set; }
        public double GustMPH { get; private set; }
        public double GustKPH { get; private set; }
        public Condition Condition { get; private set; }

        public Weather(RealTimeWeatherDeserializer root)
        {
            LastUpdatedEpoch = root.last_updated_epoch;
            LastUpdated = DateTime.Parse(root.last_updated);
            TemperatureCelsius = root.temp_c;
            TemperatureFahrenheit = root.temp_f;
            IsDay = root.is_day == 0 ? false : true;
            WindMPH = root.wind_kph;
            WindKPH = root.wind_mph;
            WindDegress = root.wind_degree;
            WindDirection = root.wind_dir;
            PressureMb = root.pressure_mb;
            PressureIn = root.precip_in;
            PrecipMM = root.precip_mm;
            PrecipIn = root.precip_in;
            Humidity = root.humidity;
            Cloud = root.cloud;
            FellsLikeCelsius = root.feelslike_c;
            FellsLikeFahrenheit = root.feelslike_f;
            VisibilityKm = root.vis_km;
            VisibilityMiles = root.vis_miles;
            UV = root.uv;
            GustMPH = root.gust_kph;
            GustKPH = root.gust_kph;
            Condition = new Condition(root.condition);
        }

        public Weather()
        {

        }
        protected void UpdateWeather()
        {

        }
    }

    public class Condition
    {
        public string Text { get; private set; }
        public string Icon { get; private set; } // We could Use binary parser
        public int Code { get; private set; }
        public Condition(WeatherAPI.Deserializer.Condition root)
        {
            Text = root.text;
            Icon = root.icon;
            Code = root.code;
        }
        protected void UpdateCondition()
        {

        }
    }
}
