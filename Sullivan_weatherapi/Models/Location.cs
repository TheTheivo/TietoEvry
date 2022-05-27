using System;
using System.Collections.Generic;
using System.Text;
using WeatherAPI.Deserializer;
using WeatherAPI.Deserializer.WeatherAPI.Deserializer;
using WeatherAPI.Models;

namespace WeatherAPI.Models
{
    public class Location
    {
        public string Name { get;  set; }
        public string Region { get;  set; }
        public string Country { get;  set; }
        public double Latitude { get;  set; }
        public double Longitude { get;  set; }
        public string TzID { get;  set; }
        public int LocalTimeEpoch { get;  set; }
        public DateTime LocalTime { get;  set; }
        public Weather Weather { get;  set; }
        public Astronomy Astronomy { get;  set; }

        public Forecast Forecast { get;  set; }
        public Location(Location location, Weather weather = null, Astronomy astronomy = null)
        {
            Name = location.Name;
            Region = location.Region;
            Country = location.Country;
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            TzID = location.TzID;
            LocalTimeEpoch = location.LocalTimeEpoch;
            LocalTime = location.LocalTime;
            Weather = weather;
            Astronomy = astronomy;
        }

        public Location(ForecastRoot root)
        {
            Name = root.location.location.name;
            Region = root.location.location.region;
            Country = root.location.location.country;
            Latitude = root.location.location.lat;
            Longitude = root.location.location.lon;
            TzID = root.location.location.tz_id;
            LocalTimeEpoch = root.location.location.localtime_epoch;
            Forecast = root.forecast;
        }
        public Location (LocationRoot root)
        {
            Name = root.location.name;
            Region = root.location.region;
            Country = root.location.country;
            Latitude = root.location.lat;
            Longitude = root.location.lon;
            TzID = root.location.tz_id;
            LocalTimeEpoch = root.location.localtime_epoch;
            LocalTime = DateTime.Parse(root.location.localtime);
            Weather = new Weather();
            Astronomy = new Astronomy();
        }
        public Location() { }
        public Location(AstroRoot root)
        {
            Name = root.location.name;
            Region = root.location.region;
            Country = root.location.country;
            Latitude = root.location.lat;
            Longitude = root.location.lon;
            TzID = root.location.tz_id;
            LocalTimeEpoch = root.location.localtime_epoch;
            LocalTime = DateTime.Parse(root.location.localtime);
            Astronomy = new Astronomy(root.astronomy);
        }
        public Location(RealTimeRoot root)
        {
            Name = root.location.name;
            Region = root.location.region;
            Country = root.location.country;
            Latitude = root.location.lat;
            Longitude = root.location.lon;
            TzID = root.location.tz_id;
            LocalTimeEpoch = root.location.localtime_epoch;
            LocalTime = DateTime.Parse(root.location.localtime);
            Weather = new Weather(root.current);
            Astronomy = new Astronomy();
        }
    }
}
