using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeatherAPI.DirectoryHelpers
{
    static class CitiesLoader
    {

        public static List<string> GetCities()
        {
            var CitiesList = new List<string>();

#if DEBUG
            var directory = new DirectoryInfo($"{Directory.GetCurrentDirectory()}/Data");
#else

#endif
            using (var sr = new StreamReader($"{directory.FullName}/SupportedCities.txt"))
            {
                while(!sr.EndOfStream)
                {
                    CitiesList.Add(sr.ReadLine());
                }
            }
            return CitiesList;
        }
    }
}
