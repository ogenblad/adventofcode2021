using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace _06
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var input = File.ReadAllText("input.txt");
            var numbers = input.Split(",").Select(Int32.Parse).ToList();
            
            long a1 = CountFishes(numbers, 80);
            long a2 = CountFishes(numbers, 256);
            
            sw.Stop();
            Console.WriteLine($"Assigment1: {a1}");
            Console.WriteLine($"Assigment2: {a2}");
            Console.WriteLine($"Total time: {sw.Elapsed.TotalMilliseconds}ms");
        }

        public static long CountFishes(List<int> initial, int numberOfDays)
        {
            List<long> state = new List<long> {0,0,0,0,0,0,0,0,0};
            
            foreach (var currentInitialState in initial)
            {
                state[currentInitialState]++;
            }

            for (var currentDay = 0; currentDay < numberOfDays; currentDay++)
            {
                state[7] += state[0];
                state.Insert(9, state[0]);
                state.RemoveAt(0);
            }

            long sum = state.Sum();
            return sum;
        }
    }
}
