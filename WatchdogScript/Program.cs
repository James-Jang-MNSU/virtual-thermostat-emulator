using System;
using System.IO;
using System.Threading;

namespace WatchdogScript
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- THERMOSTAT WATCHDOG CONNECTED ---");
            Console.WriteLine("Monitoring for critical failures (> 100 C)");

            // IMPORTANT: PASTE THE FULL PATH TO YOUR status.txt HERE
            // TODO: Update this path to point to your ThermostatEmulator's debug folder
            // Example: @"C:\Users\YourName\Source\Repos\ThermostatEmulator\bin\Debug\net8.0-windows\status.txt"
            string filePath = @"C:\REPLACE_WITH_YOUR_PATH\status.txt";
            
            while (true)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        // 1. Read the "Sensor"
                        string content = File.ReadAllText(filePath);

                        if (double.TryParse(content, out double currentTemp))
                        {
                            Console.Write($"\rCurrent Temp: {currentTemp:F2}°C   "); // \r overwrites the line

                            // 2. Check for Danger
                            if (currentTemp > 100.0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n!!! CRITICAL ALARM !!! TEMPERATURE EXCEEDED 100°C !!!");
                                Console.Beep(); // Makes a sound!
                                Console.ResetColor();
                            }
                        }
                    }
                }
                catch (IOException)
                {
                    // Ignore errors if the file is currently being written to by the other app
                }

                // Wait 0.5 seconds before checking again
                Thread.Sleep(500);
            }
        }
    }
}
