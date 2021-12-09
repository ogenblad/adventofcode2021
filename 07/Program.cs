using System;
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
            Console.WriteLine($"TASK2: Least amount of consumed fuel: {result[1].Item1}, target position: {result[1].Item2}");
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

            List<Tuple<int, int>> fuelResultCheap = new List<Tuple<int, int>>();
            List<Tuple<int, int>> fuelResultExpensive = new List<Tuple<int, int>>();
            foreach (var position in initialPositionCount)
            {
                fuelResultCheap.Add(CalculateFuelConsumtionList(initialPositionCount, position.Item1, false));
                fuelResultExpensive.Add(CalculateFuelConsumtionList(initialPositionCount, position.Item1, true));
            }

            var additionalTargets = fuelResultExpensive.OrderBy(f => f.Item1).Take(2).ToList();

            for (var additionalTarget = additionalTargets.FirstOrDefault().Item2+1; additionalTarget < additionalTargets.Last().Item2; additionalTarget++)
            {
                fuelResultExpensive.Add(CalculateFuelConsumtionList(initialPositionCount, additionalTarget, true));
            }

            var result = new List<Tuple<int, int>>();
            result.Add(fuelResultCheap.OrderBy(f => f.Item1).FirstOrDefault());
            result.Add(fuelResultExpensive.OrderBy(f => f.Item1).FirstOrDefault());
            
            return result;
        }

        public static int EnsurePositive(int input)
        {
            if (input < 0) { return input * -1; }
            return input;
        }

        public static Tuple<int, int> CalculateFuelConsumtionList(List<Tuple<int, int>> input, int targetPosition, bool expensive)
        {
            int consumedFuel = 0;
            foreach (var move in input)
            {
                consumedFuel += CalculateFuelConsumption(move.Item1, targetPosition, move.Item2, expensive);
            }

            return Tuple.Create(consumedFuel, targetPosition);
        }

        public static int CalculateFuelConsumption(int currentPosition, int targetPosition, int count, bool expensive)
        {
            if (!expensive)
            {
                return currentPosition != targetPosition ? EnsurePositive((currentPosition - targetPosition) * count) : 0;
            }
            
            return currentPosition != targetPosition ? (Enumerable.Range(1,EnsurePositive(currentPosition - targetPosition)).Sum()) * count : 0;;
        }
    }
}
