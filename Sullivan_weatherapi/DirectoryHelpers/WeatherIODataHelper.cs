using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WeatherAPI.Models;

namespace WeatherAPI.DirectoryHelpers
{
    public class WeatherIODataHelper
    {
        private static List<FileInfo> files = new List<FileInfo>();
        public static void WriteWeatherDataToXML(Location data)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Location));
            
            FileStream fs = File.Create($"{Constants.XmlDataFile}__manual__{data.LocalTime}");
            writer.Serialize(fs, data);
            fs.Close();
            
        }

        public static void WriteWeatherDataToXML(List<Location> datas, string nameCall)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Forecast));

            foreach(var data in datas)
            {
                FileStream fs = File.Create($"{Constants.XmlDataFile}__{nameCall}__{data.LocalTime}");
                writer.Serialize(fs, data);
                fs.Close();
            }
        }

        public static ForecastRoot GetLatestWeatherDataFromXmlForecast()
        {
            if (files.Count == 0)
                throw new Exception("There are no data to read");
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Location));

            var directory = new DirectoryInfo($"{Constants.XmlDataFile}");

            //Check for file

            var fileData = new ForecastRoot();

            using (Stream reader = new FileStream($"{Constants.XmlDataFile},", FileMode.Open))
            {
                fileData = (ForecastRoot)writer.Deserialize(reader);
            }
            return fileData;
        }

        public static Location GetLatestWeatherDataFromXml()
        {
            System.Xml.Serialization.XmlSerializer writer = null;
            var fileData = new Location();
            try
            {
                if (files.Count == 0)
                    throw new Exception("There are no data to read");
                writer = new System.Xml.Serialization.XmlSerializer(typeof(Location));
                var directory = new DirectoryInfo($"{Constants.XmlDataFile}");
                using (Stream reader = new FileStream($"{Constants.XmlDataFile},", FileMode.Open))
                {
                    fileData = (Location)writer.Deserialize(reader);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return fileData;
        }
    }
}
