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
            var result = FindOptimalPosition(positions);
            sw.Stop();

            Console.WriteLine($"TASK1: Least amount of consumed fuel: {result[0].Item1}, target position: {result[0].Item2}");
            Console.WriteLine($"TASK1: Least amount of consumed fuel: {result[1].Item1}, target position: {result[1].Item2}");
            Console.WriteLine($"Execution time: {sw.Elapsed.TotalMilliseconds}");
        }

        public static List<Tuple<int, int>> FindOptimalPosition(List<int> input)
        {
            List<Tuple<int, int>> initialPositionCount = new List<Tuple<int, int>>();
            var groupedPositions = input.GroupBy(p => p).ToList();

            foreach (var key in groupedPositions)
            {
                var count = key.Select(x => x).Count();
                initialPositionCount.Add(Tuple.Create(key.Key, count));
            }

            List<Tuple<int, int>> fuelResult1 = new List<Tuple<int, int>>();
            List<Tuple<int, int>> fuelResult2 = new List<Tuple<int, int>>();
            foreach (var position in initialPositionCount)
            {
                int consumedFuel1 = 0;
                int consumedFuel2 = 0;
                foreach (var move in initialPositionCount)
                {
                    consumedFuel1 += CalculateFuelConsumption(move.Item1, position.Item1, move.Item2, false);
                    consumedFuel2 += CalculateFuelConsumption(move.Item1, position.Item1, move.Item2, true);
                }

                fuelResult1.Add(Tuple.Create(consumedFuel1, position.Item1));
                fuelResult2.Add(Tuple.Create(consumedFuel2, position.Item1));
            }

            var indexDiff = fuelResult2.OrderBy(f => f.Item1).Take(2).ToList();

            for (var i = indexDiff.FirstOrDefault().Item2+1; i < indexDiff.Last().Item2; i++)
            {
                int consumedFuel2 = 0;
                foreach (var move in initialPositionCount)
                {
                    consumedFuel2 += CalculateFuelConsumption(move.Item1, i, move.Item2, true);
                }

                fuelResult2.Add(Tuple.Create(consumedFuel2, i));
            }

            var result = new List<Tuple<int, int>>();
            result.Add(fuelResult1.OrderBy(f => f.Item1).FirstOrDefault());
            result.Add(fuelResult2.OrderBy(f => f.Item1).FirstOrDefault());
            
            return result;
        }

        public static int EnsurePositive(int input)
        {
            if (input < 0) { return input * -1; }
            return input;
        }

        public static int CalculateFuelConsumption(int currentLocation, int targetLocation, int count, bool expensive)
        {
            if (!expensive)
            {
                return currentLocation != targetLocation ? EnsurePositive((currentLocation - targetLocation) * count) : 0;
            }
            
            return currentLocation != targetLocation ? (Enumerable.Range(1,EnsurePositive(currentLocation - targetLocation)).Sum()) * count : 0;;
        }
    }
}
