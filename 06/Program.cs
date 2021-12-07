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
            var fishyResult = PopulateFishes(numbers, 80);

            Console.WriteLine($"Answer: {fishyResult.Fishes.Count()}");
            Console.WriteLine("Log:");
            fishyResult.Log.ForEach(i => Console.WriteLine(i));
        }

        public static FishResult PopulateFishes(List<int> numbers, int numberOfDays)
        {
            List<Fish> fishes = new List<Fish>();
            List<string> log = new List<string>();
            int counter = 0;

            log.Add($"Initial state: {string.Join(",", numbers)}");
            
            foreach (var number in numbers)
            {
                fishes.Add(new Fish { Timer = number});
            }

            for (var d = 0; d < numberOfDays; d++)
            {
                List<Fish> newBornFishes = new List<Fish>();
                
                foreach ( var fish in fishes)
                {
                    var createNewFish = fish.CountDay();
                    
                    if (createNewFish)
                    {
                        newBornFishes.Add(new Fish());
                    }
                }

                if (counter > 0)
                {
                    string currentState = string.Join(",", fishes.Select(f => f.Timer).ToList());
                    log.Add($"After {counter} days: {currentState}");
                }
                
                fishes = fishes.Concat(newBornFishes).ToList();
                counter++;
            }

            return new FishResult { Fishes = fishes, Log = log };
        }
    }

    public class Fish
    {
        public int Timer { get; set; }
        public int Children { get; set; }

        public Fish (int timer = 8, int children = 0)
        {
            this.Timer = timer;
            this.Children = children;
        }

        private void GiveBirth()
        {
            this.Timer = 6;
            this.Children++;
        }

        public int ChildrenCount()
        {
            return this.Children;
        }

        public bool CountDay()
        {
            this.Timer--;

            if (this.Timer < 0)
            {
                this.GiveBirth();
                return true;
            }

            return false;
        }
    }

    public class FishResult
    {
        public List<Fish> Fishes { get; set; }
        public List<string> Log { get; set; }
    }
}
