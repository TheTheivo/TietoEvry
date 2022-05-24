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
        public string Name { get; private set; }
        public string Region { get; private set; }
        public string Country { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string TzID { get; private set; }
        public int LocalTimeEpoch { get; private set; }
        public DateTime LocalTime { get; private set; }
        public Weather Weather { get; private set; }
        public Astronomy Astronomy { get; private set; }

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
            Weather = new Weather(root.Weather);
            Astronomy = new Astronomy();
        }
    }
}
