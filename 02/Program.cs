using System;
using System.Collections.Generic;
using System.IO;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt");

            // Execute Assignment 1
            Assigment1(input);

            // Execute Assignment 2
            Assigment2(input);
            
        }

        public static void Assigment1(IEnumerable<string> input)
        {
            int horizontalPos = 0;
            int verticalPos = 0;
                        
            foreach (var line in input)
            {
                var currentInstruction = line.Split(" ");
                var direction = currentInstruction[0];
                var value = Int32.Parse(currentInstruction[1]);

                switch (direction)
                {
                    case "up":
                        verticalPos = verticalPos - value;
                        break;

                    case "down":
                        verticalPos = verticalPos + value;
                        break;

                    case "forward":
                        horizontalPos = horizontalPos + value;
                        break;
                }

                Console.WriteLine($"{direction} {value.ToString()}");
            }

            Console.WriteLine();
            Console.WriteLine($"Horizontal position: {horizontalPos}");
            Console.WriteLine($"Vertical position: {verticalPos}");
            Console.WriteLine();
            Console.WriteLine($"Puzzle Answer: {(horizontalPos * verticalPos).ToString()}");
        }

        public static void Assigment2(IEnumerable<string> input)
        {
            int horizontalPos = 0;
            int verticalPos = 0;
            int aim = 0;

            foreach (var line in input)
            {
                var currentInstruction = line.Split(" ");
                var direction = currentInstruction[0];
                var value = Int32.Parse(currentInstruction[1]);

                switch (direction)
                {
                    case "up":
                        aim = aim - value;
                        break;

                    case "down":
                        aim = aim + value;
                        break;

                    case "forward":
                        horizontalPos = horizontalPos + value;
                        verticalPos = verticalPos + (aim * value);
                        break;
                }

                Console.WriteLine($"{direction} {value.ToString()}");
            }

            Console.WriteLine();
            Console.WriteLine($"Horizontal position: {horizontalPos}");
            Console.WriteLine($"Vertical position: {verticalPos}");
            Console.WriteLine($"Aim: {aim}");
            Console.WriteLine();
            Console.WriteLine($"Puzzle Answer: {(horizontalPos * verticalPos).ToString()}");
        }
    }
}
