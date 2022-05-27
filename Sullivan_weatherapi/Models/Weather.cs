using System;
using System.Collections.Generic;
using System.Text;
using WeatherAPI.Deserializer;

namespace WeatherAPI.Models
{
    public class Weather
    {
        public int LastUpdatedEpoch { get;  set; }
        public DateTime LastUpdated { get;  set; }
        public double TemperatureCelsius { get;  set; }
        public double TemperatureFahrenheit { get;  set; }
        public bool IsDay{ get;  set; }
        public double WindMPH { get;  set; }
        public double WindKPH { get;  set; }
        public int WindDegress { get;  set; }
        public string WindDirection { get;  set; }
        public double PressureMb { get;  set; }
        public double PressureIn{ get;  set; }
        public double PrecipMM { get;  set; }
        public double PrecipIn { get;  set; }
        public double Humidity { get;  set; }
        public double Cloud{ get;  set; }
        public double FellsLikeCelsius { get;  set; }
        public double FellsLikeFahrenheit { get;  set; }
        public double VisibilityKm { get;  set; }
        public double VisibilityMiles { get;  set; }
        public double UV { get;  set; }
        public double GustMPH { get;  set; }
        public double GustKPH { get;  set; }
        public Condition Condition { get;  set; }

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
    }

    public class Condition
    {
        public string Text { get;  set; }
        public string Icon { get;  set; } // We could Use binary parser
        public int Code { get;  set; }
        public Condition(WeatherAPI.Deserializer.Condition root)
        {
            Text = root.text;
            Icon = root.icon;
            Code = root.code;
        }
        public Condition()
        {

        }
    }
}
