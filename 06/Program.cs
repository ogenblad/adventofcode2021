using System;
using System.Collections.Generic;
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
            
            Console.WriteLine($"Assigment1: {CountFishes(numbers, 80)}");
            Console.WriteLine($"Assigment1: {CountFishes(numbers, 256)}");
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
