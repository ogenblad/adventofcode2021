using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace _01
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var records = File.ReadLines("measurements.txt");

            Console.WriteLine("Start analyzing all measurements...");
            Analyze(records);

            Console.WriteLine();
            Console.WriteLine("Start analyzing summarized measurements...");
            var sumarizedMeasurements = SumarizeMeasurements(records);
            Analyze(sumarizedMeasurements);
        }

        public static IEnumerable<string> SumarizeMeasurements(IEnumerable<string> measurements)
        {
            IList<string> sumarizedMeasurements = new List<string>();
            IList<int> values = new List<int>();

            foreach (var measurement in measurements)
            {
                int currentValue = Int32.Parse(measurement);
                
                if (values.Count < 3)
                {
                    values.Add(currentValue);
                }
                else {
                    sumarizedMeasurements.Add(values.Sum().ToString());
                    values.RemoveAt(0);
                    values.Add(currentValue);
                }                
            }

            sumarizedMeasurements.Add(values.Sum().ToString());
            return sumarizedMeasurements;
        }

        public static void Analyze(IEnumerable<string> measurements)
        {
            int prevValue = -1;
            int counter = 0;

            foreach (var measurement in measurements)
            {
                int currentMeasurement = Int32.Parse(measurement);
                
                if (prevValue == -1)
                {
                    prevValue = currentMeasurement;
                    Console.WriteLine($"{currentMeasurement} (N/A, no prev value)");
                }
                else
                {
                    if (currentMeasurement > prevValue)
                    {
                        prevValue = currentMeasurement;
                        counter++;
                        Console.WriteLine($"{currentMeasurement} (increased)");
                    }
                    else
                    {
                        prevValue = currentMeasurement;
                        Console.WriteLine($"{currentMeasurement} (decreased)");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Total increased: {counter}");
        }
    }
}
