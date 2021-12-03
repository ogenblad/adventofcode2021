using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _03
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt");
            
            // Part 1:
            var parsedBits = ParseBits(input);
            BuildFinalBits(parsedBits);

            // Part 2:
            var oxygen = FilterRating(input, "oxygen");
            var co2 = FilterRating(input, "co2");

            Console.WriteLine();
            Console.WriteLine($"Oxygen rating: {oxygen}");
            Console.WriteLine($"Oxygen rating: {co2}");
            Console.WriteLine();
            Console.WriteLine($"Part 2, Answer: {oxygen * co2}");
        }

        public static int FilterRating(IEnumerable<string> input, string ratingKey)
        {
            int index = 0;
            int indexLimit = input.FirstOrDefault().Length;
            
            while (index < indexLimit)
            {
                if (input.Count() == 1) { break; }

                Console.WriteLine($"Working on index {index}...");
                var parsedBits = ParseBits(input);
                var hasMostOccurrences = GetMostOccurrences(parsedBits[index]);


                if (ratingKey == "oxygen")
                {
                    input = input.Where(i => i[index].ToString().Contains(hasMostOccurrences.ToString())).ToList();
                    Console.WriteLine($"Selection now contans {input.Count()} rows...");
                }
                else {
                    var filterValue = hasMostOccurrences == 1 ? 0 : 1;
                    
                    input = input.Where(i => i[index].ToString().Contains(filterValue.ToString())).ToList();
                    Console.WriteLine($"Selection now contans {input.Count()} rows...");
                }

                index++;
            }

            return Convert.ToInt32(input.FirstOrDefault(), 2);
        }
        
        public static void BuildFinalBits(IList<List<int>> parsedBits)
        {
            string gamma = "";
            string epsilon = "";

            foreach (var list in parsedBits)
            {
                var result = GetMostOccurrences(list);

                gamma += result.ToString();
                epsilon += result > 0 ? "0" : "1";
            }

            var gammaDecimal = Convert.ToInt32(gamma, 2);
            var epsilonDecimal = Convert.ToInt32(epsilon, 2);

            Console.WriteLine($"Gamma: {gammaDecimal}");
            Console.WriteLine($"Epsilon: {epsilonDecimal}");
            Console.WriteLine();
            Console.WriteLine($"Part 1, Answer: {gammaDecimal * epsilonDecimal}");
        }

        public static int GetMostOccurrences(IList<int> input)
        {
            int zeroCount = input.Count(input => input == 0);
            int oneCount = input.Count(input => input == 1);

            if (zeroCount > oneCount)
            {
                return 0;
            }
            else {
                return 1;
            }
        }
        
        public static IList<List<int>> ParseBits(IEnumerable<string> input)
        {
            var parsedBits = new List<List<int>>()
            {
                new List<int>(),
                new List<int>(),
                new List<int>(),
                new List<int>(),
                new List<int>(),
                new List<int>(),
                new List<int>(),
                new List<int>(),
                new List<int>(),
                new List<int>(),
                new List<int>(),
                new List<int>()
            };

            foreach (var line in input)
            {
                var bits = line.ToCharArray();
                parsedBits[0].Add(Int32.Parse(bits[0].ToString()));
                parsedBits[1].Add(Int32.Parse(bits[1].ToString()));
                parsedBits[2].Add(Int32.Parse(bits[2].ToString()));
                parsedBits[3].Add(Int32.Parse(bits[3].ToString()));
                parsedBits[4].Add(Int32.Parse(bits[4].ToString()));
                parsedBits[5].Add(Int32.Parse(bits[5].ToString()));
                parsedBits[6].Add(Int32.Parse(bits[6].ToString()));
                parsedBits[7].Add(Int32.Parse(bits[7].ToString()));
                parsedBits[8].Add(Int32.Parse(bits[8].ToString()));
                parsedBits[9].Add(Int32.Parse(bits[9].ToString()));
                parsedBits[10].Add(Int32.Parse(bits[10].ToString()));
                parsedBits[11].Add(Int32.Parse(bits[11].ToString()));
            }

            return parsedBits;
        }
    }
}
