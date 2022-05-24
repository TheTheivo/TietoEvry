using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherAPI
{
    public static class Constants
    {
        public static string XmlDataFile { get ; private set;}
        public static string CitiesFile { get; private set; }
        public static int? CallIntervalTospecificEndpoint { get; private set; }
        public static int? CallIntervalToAllEndpoints { get; private set; }
        public static bool EndProgram { get; set; }

        public static void SetRunInConstants(string xmlDirectory,string citiesFile,int callIntervalToSpecificEndpoint,int callIntervalToAllEndpoints)
        {
            EndProgram = false;
            if (string.IsNullOrEmpty(XmlDataFile))
                XmlDataFile = xmlDirectory;
            if (string.IsNullOrEmpty(CitiesFile))
                CitiesFile = citiesFile;
            if (CallIntervalTospecificEndpoint == null)
            {
                CallIntervalTospecificEndpoint = callIntervalToSpecificEndpoint;
                if (CallIntervalTospecificEndpoint < 5000)
                    CallIntervalTospecificEndpoint = 5000;
                if(CallIntervalTospecificEndpoint > 60000)
                    CallIntervalTospecificEndpoint = 60000;
            }
                
            if (CallIntervalToAllEndpoints == null)
            {
                CallIntervalToAllEndpoints = callIntervalToAllEndpoints;
                if (CallIntervalToAllEndpoints < 5000)
                    CallIntervalToAllEndpoints = 5000;
                if (CallIntervalToAllEndpoints > 60000)
                    CallIntervalToAllEndpoints = 60000;
            }
                
        }
    }
}
