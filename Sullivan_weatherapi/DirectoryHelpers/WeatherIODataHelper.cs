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
        public static List<FileInfo> files = new List<FileInfo>();
        public static void WriteWeatherDataToXML(Location data)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Location));
            
            FileStream fs = File.Create($"{Constants.XmlDataDir}weatherdata__manual__{data.LocalTime.Ticks}.xml");
            writer.Serialize(fs, data);
            files.Add(new FileInfo(fs.Name));
            fs.Close();
            
        }

        public static void WriteWeatherDataToXML(List<Location> datas, string nameCall)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Location));

            foreach(var data in datas)
            {
                
                FileStream fs = File.Create($"{Constants.XmlDataDir}weatherdata__{nameCall}__{data.LocalTime.Ticks}.xml");
                writer.Serialize(fs, data);
                files.Add(new FileInfo(fs.Name));
                fs.Close();
            }
        }


        public static void ListAllXmlData()
        {

            foreach(var file in files)
            {
                Console.WriteLine(file.Name);
            }
        }
        public static Location GetWeatherDataFromXml(string filename)
        {
            System.Xml.Serialization.XmlSerializer writer = null;
            var fileData = new Location();
            try
            {
                if (files.Count == 0)
                    throw new Exception("There are no data to read");

                var checkFile = files.Find(x => x.Name == filename);
                if (checkFile == null)
                    throw new Exception("File not found");

                writer = new System.Xml.Serialization.XmlSerializer(typeof(Location));
                var directory = new DirectoryInfo($"{Constants.XmlDataDir}");
                using (Stream reader = new FileStream($"{Constants.XmlDataDir},", FileMode.Open))
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
