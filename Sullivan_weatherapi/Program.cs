using System;
using System.IO;
using System.Linq;
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
            var directory = new DirectoryInfo($"{Directory.GetCurrentDirectory()}");
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            string xmlDirectory = $"{directory}/Data/WeatherData/LatestWeatherData.xml";
            string citiesFile = $"{directory}/Data/SupportedCities.txt";
            int callIntervalToSpecificEndpoint = 0;
            int callIntervalToAllEndpoints = 0;
#endif
            Constants.SetRunInConstants(xmlDirectory, citiesFile, callIntervalToSpecificEndpoint, callIntervalToAllEndpoints);
            
            while (Constants.EndNotProgram)
            {
                UI.OutputHandler.PresentModeSelection();
                int mode = 0;
                bool modeSelection = true;
                while(modeSelection)
                {
                    var input = UI.InputHandler.ReadLine();
                    try 
                    {
                        int.TryParse(input, out mode);
                        switch (mode)
                        {
                            case 1:
                                Console.WriteLine("Entering manual mode.");
                                modeSelection = false;
                                break;
                            case 2:
                                Console.WriteLine("Entering automatic mode.");
                                modeSelection = false;
                                break;
                            default:
                                throw new Exception();
                        }
                    }
                    catch(Exception e)
                    {
                        UI.OutputHandler.PresentModeSelection();
                    }
                }

                Core loop = Core.Instance();
                loop.StartLoop(mode == 1?ModeType.manual:ModeType.automatic);
            }
        }
    }
}
