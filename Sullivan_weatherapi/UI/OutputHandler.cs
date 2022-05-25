using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherAPI.UI
{
    public static class OutputHandler
    {
        public static void PresentModeSelection()
        {
            Console.WriteLine("Welcome to weather app.");
            Console.WriteLine("For munal mode press 1 for automatic mode press 2.");
            Console.WriteLine("To exit an app type Exit.");
        }

        public static void PresentAutomaticPullFreqency()
        {
            Console.WriteLine("Type how frueqently will be data pulled, between 5 seconds and 60 secds"); ;
            Console.WriteLine("To exit an app type Exit.");
        }

        public static void PresentErrorInput()
        {
            Console.WriteLine("Please select one of the given choices by typing the number.");
            Console.WriteLine("To exit an app type \"Exit\".");
        }

        public static void PresentCustomInputInput()
        {
            Console.WriteLine("Please select one of the given choices by typing the choice.");
            Console.WriteLine("To exit an app type \"Exit\".");
        }

        public static void PresentHourInput()
        {
            Console.WriteLine("Type hours in 24 format for sunrise search, end");
            Console.WriteLine("To exit an app type \"Exit\".");
        }
    }
}
