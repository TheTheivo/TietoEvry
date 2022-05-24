using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAPI.API;
using WeatherAPI.DirectoryHelpers;
using WeatherAPI.Models;

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
        public enum ModeType {
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
                input = Core.ReadCheckInput();
                if (Constants.EndProgram)
                    return;
                try
                {
                    int.TryParse(input, out selection);
                    if (selection != 1 && selection != 2)
                        throw new Exception();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please select give choices by typing the number.");
                    Console.WriteLine("To exit an app type \"Exit\".");
                }

                if (selection == 1)
                {
                    data = WeatherIODataHelper.GetLatestWeatherDataFromXml();
                    
                    var selectedMethod = 0;
                    input = Core.ReadCheckInput();
                    if (Constants.EndProgram)
                        return;
                    try
                    {
                        int.TryParse(input, out selectedMethod);
                        if (selectedMethod != 1 && selectedMethod != 2 && selectedMethod != 3)
                            throw new Exception();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please select give choices by typing the number.");
                        Console.WriteLine("To exit an app type \"Exit\".");
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
                    input = Core.ReadCheckInput();
                    if (Constants.EndProgram)
                        return;
                    try
                    {
                        if (!cities.Contains(input))
                            throw new Exception();
                        selectedCity = input;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please select give choices by typing the City");
                        Console.WriteLine("To exit an app type \"Exit\".");
                    }
                    if (Constants.EndProgram)
                        return;

                    Console.WriteLine("To get Realtime weather type 1");
                    Console.WriteLine("To get Astronomy type 2");
                    Console.WriteLine("To get TimeZone type 3");
                    Console.WriteLine("To get Forecast type 4");
                    Console.WriteLine("To exit an app type Exit.");

                    var selectedMethod = 0;
                    input = Core.ReadCheckInput();
                    if (Constants.EndProgram)
                        return;
                    try
                    {
                        int.TryParse(input, out selectedMethod);
                        if (selectedMethod != 1 || selectedMethod != 2 || selectedMethod != 3)
                            throw new Exception();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please select given choices by typing the number.");
                        Console.WriteLine("To exit an app type \"Exit\".");
                    }

                    switch (selectedMethod)
                    {
                        case (1):
                            data = Task.Run(async () => await WeatherApi.GetRealTimeWeather(selectedCity)).Result;
                            break;
                        case (2):
                            data = Task.Run(async () => await WeatherApi.GetAstronomy(selectedCity)).Result;
                            break;
                        case (3):
                            data = Task.Run(async () => await WeatherApi.GetTimeZone(selectedCity)).Result;
                            break;
                    }
                    WeatherIODataHelper.WriteWeatherDataToXML(data);
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

            while(mode == ModeType.automatic)
            {
                Console.WriteLine("Type how frueqently will be data pulled, between 5 seconds and 60 secds");;
                Console.WriteLine("To exit an app type Exit.");

                var selectedMethod = 0;
                input = Core.ReadCheckInput();
                if (Constants.EndProgram)
                    return;
                try
                {
                    int.TryParse(input, out selectedMethod);
                    if (selectedMethod < 5 && selectedMethod >60)
                        throw new Exception();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please select give choices by typing the number.");
                    Console.WriteLine("To exit an app type \"Exit\".");
                }
                interval = interval * 1000;

                Console.WriteLine("Type how long will be data pulled in seconds"); ;
                Console.WriteLine("To exit an app type Exit.");

                input = Core.ReadCheckInput();
                if (Constants.EndProgram)
                    return;
                try
                {
                    int.TryParse(input, out selectedMethod);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please select give choices by typing the number.");
                    Console.WriteLine("To exit an app type \"Exit\".");
                }
                elapsedTime = selectedMethod*1000;

                Console.WriteLine("Choose one of the cities by typing it:");
                var cities = CitiesLoader.GetCities();
                foreach (var city in cities)
                {
                    Console.WriteLine(city);
                }
                input = Core.ReadCheckInput();
                if (Constants.EndProgram)
                    return;
                try
                {
                    if (!cities.Contains(input))
                        throw new Exception();
                    selectedCity = input;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please select give choices by typing the City");
                    Console.WriteLine("To exit an app type \"Exit\".");
                }
                if (Constants.EndProgram)
                    return;

                Console.WriteLine("To get Realtime weather type 1");
                Console.WriteLine("To get Astronomy type 2");
                Console.WriteLine("To get TimeZone type 3");
                Console.WriteLine("To get All type 4");
                Console.WriteLine("To exit an app type Exit.");
                
                try
                {
                    input = Core.ReadCheckInput();
                    if (Constants.EndProgram)
                        return;
                    int.TryParse(input, out selectedMethod);
                    if (selectedMethod != 1 || selectedMethod != 2 || selectedMethod != 3 || selectedMethod != 4)
                        throw new Exception();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please select give choices by typing the number.");
                    Console.WriteLine("To exit an app type \"Exit\".");
                }

                //Standartne bych zde udelal job a plne asynchorne, ale pokud chapu zadani, tahle cast by mela byt synchroni a jen se intervalem a dobou volani urci kolikrat se ma metoda zavolat
                stopwatch.Start();
                while(stopwatch.ElapsedMilliseconds <= elapsedTime)
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
                }

                int sunriseFirst = 0;
                int sunriseLast = 0;
                Console.WriteLine("Type hours in 24 format for sunrise search, begining");
                Console.WriteLine("To exit an app type Exit.");
                
                try
                {
                    input = Core.ReadCheckInput();
                    if (Constants.EndProgram)
                        return;
                    int.TryParse(input, out sunriseFirst);
                    if (selectedMethod < 1 && selectedMethod > 24)
                        throw new Exception();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Type hours in 24 format for sunrise search, end");
                    Console.WriteLine("To exit an app type \"Exit\".");
                }
                Console.WriteLine("Type hours in 24 format for sunrise search");
                Console.WriteLine("To exit an app type Exit.");
                
                try
                {
                    input = Core.ReadCheckInput();
                    if (Constants.EndProgram)
                        return;
                    int.TryParse(input, out sunriseLast);
                    if (selectedMethod < 1 && selectedMethod > 24)
                        throw new Exception();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Please select give choices by typing the number.");
                    Console.WriteLine("To exit an app type \"Exit\".");
                }

                var filteredData = data.Where(x => x.Astronomy.Sunrise.Hour > sunriseFirst && x.Astronomy.Sunset.Hour < sunriseLast).ToList();
                Console.WriteLine($"Number of entried: {filteredData.Count}");
                
            }
        }

        private void GetForecastCallType(string city, int? days = null)
        {
            if (DateTime.Now.Minute % 2 == 0)
            {
                var result = Task.Run(async () => await WeatherApi.GetForecast(city));
            }
            else
            {
                if (days == null)
                    days = 1;
                var result = Task.Run(async () => await WeatherApi.GetForecast(city, days));
            }
        }
        private static void CheckForExitInput(string input)
        {
            if (input.ToLower() == "Exit")
                Constants.EndProgram = true;
        }

        public static string ReadCheckInput()
        {
            var line = Console.ReadLine();
            CheckForExitInput(line);
            return line;
        }
    }
}
