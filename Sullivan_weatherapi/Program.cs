using System;
using System.IO;
using System.Threading.Tasks;
using WeatherAPI.API;
using static WeatherAPI.Core;

namespace WeatherAPI
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            string xmlDirectory = $"{Directory.GetCurrentDirectory()}/Data/WeatherData/LatestWeatherData.xml";
            string citiesFile = $"{Directory.GetCurrentDirectory()}/Data/SupportedCities.txt";
            int callIntervalToSpecificEndpoint = 0;
            int callIntervalToAllEndpoints = 0;
#endif
            Constants.SetRunInConstants(xmlDirectory, citiesFile, callIntervalToSpecificEndpoint, callIntervalToAllEndpoints);
            Console.WriteLine("Welcome to weather app.");
            Console.WriteLine("For munal mode press 1 for automatic mode press 2.");
            Console.WriteLine("To exit an app type Exit.");
            while (Constants.EndProgram)
            {
                int mode = 0;
                while(true)
                {
                    var input = Core.ReadCheckInput();
                    if (Constants.EndProgram)
                        break;
                    try 
                    {
                        int.TryParse(input, out mode);
                        switch (mode)
                        {
                            case 1:
                                Console.WriteLine("Entering manual mode.");
                                break;
                            case 2:
                                Console.WriteLine("Entering automatic mode.");
                                break;
                            default:
                                throw new Exception();
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("For munal mode press 1 for automatic mode press 2.");
                        Console.WriteLine("To exit an app type \"Exit\".");
                    }
                }
                if (Constants.EndProgram)
                    break;

                Core loop = Core.Instance();
                loop.StartLoop(mode == 1?ModeType.manual:ModeType.automatic);
                if (Constants.EndProgram)
                    break;
            }
        }
    }
}
