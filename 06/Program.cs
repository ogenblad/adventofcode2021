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
            var input = File.ReadAllText("input.txt");
            var numbers = input.Split(",").Select(Int32.Parse).ToList();
            
            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();
            
            sw1.Start();
            long a1 = CountFishes(numbers, 80);
            sw1.Stop();

            sw2.Start();
            long a2 = CountFishes(numbers, 256);
            sw2.Stop();


            Console.WriteLine($"Assigment1: {a1}, {sw1.ElapsedMilliseconds}ms");
            Console.WriteLine($"Assigment1: {a2}, {sw2.ElapsedMilliseconds}ms");
            Console.WriteLine($"Total time: {sw1.ElapsedMilliseconds + sw2.ElapsedMilliseconds}ms");
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
