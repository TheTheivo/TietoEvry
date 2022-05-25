using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherAPI.UI
{
    public static class InputHandler
    {
        public static string ReadLine()
        {
            var line = Console.ReadLine();
            CheckForExitInput(line);
            return line;
        }

        private static void CheckForExitInput(string input)
        {
            if (input.ToLower() == "Exit")
            {
                Constants.EndNotProgram = true;
                Environment.Exit(0);
            }
        }
    }
}
