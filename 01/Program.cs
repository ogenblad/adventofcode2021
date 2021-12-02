using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace _01
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("measurements.txt"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Measurement>();

                Console.WriteLine("Start analyzing all measurements...");
                Analyze(records);
            }

            using (var reader = new StreamReader("measurements.txt"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Measurement>();

                Console.WriteLine();
                Console.WriteLine("Start analyzing summarized measurements...");
                var sumarizedMeasurements = SumarizeMeasurements(records);
                Analyze(sumarizedMeasurements);
                
            }
        }

        public static IEnumerable<Measurement> SumarizeMeasurements(IEnumerable<Measurement> measurements)
        {
            IList<Measurement> sumarizedMeasurements = new List<Measurement>();
            IList<int> values = new List<int>();

            foreach (var measurement in measurements)
            {
                if (values.Count < 3)
                {
                    values.Add(measurement.Depth);
                }
                else {
                    sumarizedMeasurements.Add(new Measurement{Depth = values.Sum()});
                    values.RemoveAt(0);
                    values.Add(measurement.Depth);
                }                
            }

            sumarizedMeasurements.Add(new Measurement{Depth = values.Sum()});
            return sumarizedMeasurements;
        }

        public static void Analyze(IEnumerable<Measurement> measurements)
        {
            int prevValue = -1;
            int counter = 0;

            foreach (var measurement in measurements)
            {
                if (prevValue == -1)
                {
                    prevValue = measurement.Depth;
                    Console.WriteLine($"{measurement.Depth} (N/A, no prev value)");
                }
                else
                {
                    if (measurement.Depth > prevValue)
                    {
                        prevValue = measurement.Depth;
                        counter++;
                        Console.WriteLine($"{measurement.Depth} (increased)");
                    }
                    else
                    {
                        prevValue = measurement.Depth;
                        Console.WriteLine($"{measurement.Depth} (decreased)");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Total increased: {counter}");
        }
    }

    public class Measurement {
        
        [Index(0)]
        public int Depth { get; set; }
    }
}
