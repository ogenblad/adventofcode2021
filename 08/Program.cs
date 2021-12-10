using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace _08
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read input values and parse signal patterns by pipe to new array
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var input = File.ReadAllLines("input.txt");
            List<Tuple<string, string>> signalPatterns = new List<Tuple<string, string>>();
            foreach (var row in input)
            {
                var temp = row.Split(" | ");
                signalPatterns.Add(Tuple.Create(temp[0], temp[1]));
            }

            // calculate numbers
            var outputDigits = CalculateOutputValue(signalPatterns);
            sw.Stop();

            Console.WriteLine($"TASK1: Number of instances for digits 1,4,7,8: {outputDigits.Count()}");
            Console.WriteLine($"Execution time: {sw.Elapsed.TotalMilliseconds}ms");
        }

        public static List<int> CalculateOutputValue(List<Tuple<string, string>> input)
        {
            List<int> outputDigits = new List<int>();

            foreach (var signals in input)
            {
                // Create list of segment count for each signal pattern
                var segmentCount = signals.Item2.Split(" ").ToList().Select(s => s.Count()).ToList();

                // Parse digits with unique number of segments
                foreach (var segment in segmentCount)
                {
                    switch (segment)
                    {
                        case 2:
                            outputDigits.Add(1);
                            break;
                        case 3:
                            outputDigits.Add(7);
                            break;
                        case 4:
                            outputDigits.Add(4);
                            break;
                        case 7:
                            outputDigits.Add(8);
                            break;
                    }
                }
                
            }

            return outputDigits;
        }
    }
}
