using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherAPI.Deserializer
{
    namespace WeatherAPI.Deserializer
    {
        using System;
        using System.Collections.Generic;

        using System.Globalization;
        using Newtonsoft.Json;
        using Newtonsoft.Json.Converters;

        public class LocationDeserializer
        {
            public string name { get; set; }
            public string region { get; set; }
            public string country { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
            public string tz_id { get; set; }
            public int localtime_epoch { get; set; }
            public string localtime { get; set; }
        }
        public class LocationRoot
        {
            public LocationDeserializer location { get; set; }
        }
    }

}
