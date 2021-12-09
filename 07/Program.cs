using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace _07
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var input = File.ReadAllText("input.txt");
            var positions = input.Split(",").Select(Int32.Parse).ToList();
            var result = CalculateFuelConsumption(positions);
            sw.Stop();

            Console.WriteLine($"Least amount of consumed fuel: {result.Item1}, target position: {result.Item2}");
            Console.WriteLine($"Execution time: {sw.Elapsed.TotalMilliseconds}");
        }

        public static Tuple<int, int> CalculateFuelConsumption(List<int> input)
        {
            List<Tuple<int, int>> initialPositionCount = new List<Tuple<int, int>>();
            var groupedPositions = input.GroupBy(p => p).ToList();

            foreach (var key in groupedPositions)
            {
                var count = key.Select(x => x).Count();
                initialPositionCount.Add(Tuple.Create(key.Key, count));
            }

            List<Tuple<int, int>> fuelResult = new List<Tuple<int, int>>();
            foreach (var position in initialPositionCount)
            {
                int consumedFuel = 0;
                foreach (var move in initialPositionCount)
                {
                    consumedFuel += move.Item1 != position.Item1 ? EnsurePositive((move.Item1 - position.Item1) * move.Item2) : 0;
                }

                fuelResult.Add(Tuple.Create(consumedFuel, position.Item1));
            }

            return fuelResult.OrderBy(f => f.Item1).FirstOrDefault();
        }

        public static int EnsurePositive(int input)
        {
            if (input < 0) { return input * -1; }
            return input;
        }
    }
}
