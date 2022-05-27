using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherAPI.API;
using WeatherAPI.DirectoryHelpers;
using WeatherAPI.Models;
using WeatherAPI.UI;

namespace WeatherAPI
{
    class Core
    {
        private Core() { }
        private static readonly Core instance = new Core();
        public static Core Instance()
        {
            return instance;
        }
        public enum ModeType
        {
            manual,
            automatic
        }

        private ModeType mode;
        public void StartLoop(ModeType mode)
        {
            this.mode = mode;

            switch (this.mode)
            {
                case ModeType.automatic:
                    AutomaticLop();
                    break;
                case ModeType.manual:
                    ManualLoop();
                    break;
            }
        }

        private void ManualLoop()
        {
            Location data = new Location();
            string input = "";
            string selectedCity = "";

            Console.WriteLine("To load data type 1");
            Console.WriteLine("To get data type 2");

            var selection = 0;
            bool isInInputLoop = true;

            while (isInInputLoop)
            {
                input = UI.InputHandler.ReadLine();
                try
                {
                    int.TryParse(input, out selection);
                    if (selection != 1 && selection != 2)
                        throw new Exception();
                    isInInputLoop = false;
                }
                catch (Exception e)
                {
                    UI.OutputHandler.PresentErrorInput();
                }
            }

            if (selection == 1)
            {
                Console.WriteLine("Choose one of the files by typing it:");
                WeatherIODataHelper.ListAllXmlData();
                input = InputHandler.ReadLine();
                try
                {
                    data = WeatherIODataHelper.GetWeatherDataFromXml(input);
                }catch(Exception e)
                {
                    return;
                }

            }

            if (selection == 2)
            {
                Console.WriteLine("Choose one of the cities by typing it:");
                var cities = CitiesLoader.GetCities();
                foreach (var city in cities)
                {
                    Console.WriteLine(city);
                }

                isInInputLoop = true;
                while (isInInputLoop)
                {
                    input = UI.InputHandler.ReadLine();
                    try
                    {
                        if (!cities.Contains(input))
                            throw new Exception();
                        selectedCity = input;
                        isInInputLoop = false;
                    }
                    catch (Exception e)
                    {
                        UI.OutputHandler.PresentCustomInputInput();
                    }
                }

                Console.WriteLine("To get Realtime weather type 1");
                Console.WriteLine("To get Astronomy type 2");
                Console.WriteLine("To get TimeZone type 3");
                Console.WriteLine("To get Forecast type 4");
                Console.WriteLine("To exit an app type Exit.");

                var selectedMethod = 0;
                Task<Location> taskData = null;

                isInInputLoop = true;
                while (isInInputLoop)
                {
                    input = UI.InputHandler.ReadLine();
                    try
                    {
                        int.TryParse(input, out selectedMethod);
                        if (selectedMethod != 1 && selectedMethod != 2 && selectedMethod != 3 && selectedMethod != 4)
                            throw new Exception();
                        isInInputLoop = false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please select given choices by typing the number.");
                        Console.WriteLine("To exit an app type \"Exit\".");
                    }
                }

                try 
                {
                    switch (selectedMethod)
                    {
                        case (1):
                            taskData = WeatherApi.GetRealTimeWeather(selectedCity);
                            break;
                        case (2):
                            taskData = WeatherApi.GetAstronomy(selectedCity);
                            break;
                        case (3):
                            taskData = WeatherApi.GetTimeZone(selectedCity);
                            break;
                        case (4):
                            Console.WriteLine("Select how many days for forecast.");
                            Console.WriteLine("To exit an app type \"Exit\".");
                            isInInputLoop = true;
                            uint days = 1;
                            while (isInInputLoop)
                            {
                                input = UI.InputHandler.ReadLine();

                                try
                                {
                                    uint.TryParse(input, out days);
                                    isInInputLoop = false;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Select how many days for forecast.");
                                    Console.WriteLine("To exit an app type \"Exit\".");
                                }
                            }
                            taskData = WeatherApi.GetForecast(selectedCity);
                            break;
                    }
                    data = taskData.GetAwaiter().GetResult();
                    WeatherIODataHelper.WriteWeatherDataToXML(data);
                }catch(Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
                
            }
        }

        private void AutomaticLop()
        {
            Stopwatch stopwatch = new Stopwatch();
            int elapsedTime = 0;
            string input = "";
            int interval = 0;
            string selectedCity = "";
            var data = new List<Location>();

            UI.OutputHandler.PresentAutomaticPullFreqency();

            bool isInInputLoop = true;
            var selectedMethod = 0;

            while (isInInputLoop)
            {
                input = UI.InputHandler.ReadLine();

                try
                {
                    int.TryParse(input, out selectedMethod);
                    if (selectedMethod < 5 || selectedMethod > 60)
                        throw new Exception();
                    isInInputLoop = false;
                }
                catch (Exception e)
                {
                    UI.OutputHandler.PresentErrorInput();
                }
            }

            interval = interval * 1000;

            Console.WriteLine("Type how long will be data pulled in seconds"); ;
            Console.WriteLine("To exit an app type Exit.");

            isInInputLoop = true;
            while (isInInputLoop)
            {
                input = UI.InputHandler.ReadLine();
                try
                {
                    int.TryParse(input, out selectedMethod);
                    isInInputLoop = false;
                }
                catch (Exception e)
                {
                    UI.OutputHandler.PresentErrorInput();
                }
            }

            elapsedTime = selectedMethod * 1000;

            Console.WriteLine("Choose one of the cities by typing it:");
            var cities = CitiesLoader.GetCities();
            foreach (var city in cities)
            {
                Console.WriteLine(city);
            }

            isInInputLoop = true;
            while (isInInputLoop)
            {
                input = UI.InputHandler.ReadLine();

                try
                {
                    if (!cities.Contains(input))
                        throw new Exception();
                    selectedCity = input;
                    isInInputLoop = false;
                }
                catch (Exception e)
                {
                    UI.OutputHandler.PresentCustomInputInput();
                }
            }


            Console.WriteLine("To get Realtime weather type 1");
            Console.WriteLine("To get Astronomy type 2");
            Console.WriteLine("To get TimeZone type 3");
            Console.WriteLine("To get All type 4");
            Console.WriteLine("To exit an app type Exit.");

            isInInputLoop = true;
            while (isInInputLoop)
            {
                try
                {
                    input = UI.InputHandler.ReadLine();
                    int.TryParse(input, out selectedMethod);
                    if (selectedMethod != 1 && selectedMethod != 2 && selectedMethod != 3 && selectedMethod != 4)
                        throw new Exception();
                    isInInputLoop = false;
                }
                catch (Exception e)
                {
                    UI.OutputHandler.PresentErrorInput();
                }
            }


            //Standartne bych zde udelal job a plne asynchorne, ale pokud chapu zadani, tahle cast by mela byt synchroni a jen se intervalem a dobou volani urci kolikrat se ma metoda zavolat
            
            int timeoutCounter = 0;
            stopwatch.Start();
            while (stopwatch.ElapsedMilliseconds <= elapsedTime)
            {
                try
                {
                    switch (selectedMethod)
                    {
                        case (1):
                            data.Add(Task.Run(async () => await WeatherApi.GetRealTimeWeather(selectedCity)).Result);
                            break;
                        case (2):
                            data.Add(Task.Run(async () => await WeatherApi.GetAstronomy(selectedCity)).Result);
                            break;
                        case (3):
                            data.Add(Task.Run(async () => await WeatherApi.GetTimeZone(selectedCity)).Result);
                            break;
                        case (4):
                            data.Add(Task.Run(async () => await WeatherApi.GetAll(selectedCity)).Result);
                            break;
                    }
                    Thread.Sleep(interval); // Mozny TODO: prepravocat na peridoicke volani, Timer pravdepodobne nejlepe
                    timeoutCounter = 0;
                }
                catch(Exception e)
                {
                    timeoutCounter++;
                    if (timeoutCounter == 5)
                    {
                        Console.WriteLine($"Automatic mode action was interrupted due to timeout. Please try again later.");
                        break;
                    }
                        
                }
            }
            try
            {
                WeatherIODataHelper.WriteWeatherDataToXML(data, "atomatic");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            if (timeoutCounter == 5)
                return;
            int sunriseFirst = 0;
            int sunriseLast = 0;
            UI.OutputHandler.PresentHourInput();

            isInInputLoop = true;

            while (isInInputLoop)
            {
                input = UI.InputHandler.ReadLine();
                try
                {
                    int.TryParse(input, out sunriseFirst);
                    if (selectedMethod < 1 || selectedMethod > 24)
                        throw new Exception();
                    isInInputLoop = false;
                }
                catch (Exception e)
                {
                    UI.OutputHandler.PresentHourInput();
                }
            }

            UI.OutputHandler.PresentHourInput();

            isInInputLoop = true;
            while (isInInputLoop)
            {
                input = UI.InputHandler.ReadLine();
                try
                {
                    int.TryParse(input, out sunriseLast);
                    if (selectedMethod < 1 || selectedMethod > 24)
                        throw new Exception();
                    isInInputLoop = false;
                }
                catch (Exception e)
                {
                    UI.OutputHandler.PresentHourInput();
                }
            }


            var filteredData = data.Where(x => x.Astronomy.Sunrise.Hour > sunriseFirst && x.Astronomy.Sunset.Hour < sunriseLast).ToList();
            Console.WriteLine($"Number of entried: {filteredData.Count}");


        }
    }
}
