using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _04
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("test.txt");
            var numbers = input[0].Split(",").Select(Int32.Parse).ToList();
            BuildGrids(input);
        }

        public static void BuildGrids(string[] input)
        {
            int currentRow = 0;
            Grid tempGrid  = new Grid { Rows = new List<List<Tile>>() };

            for ( var i=2; i < input.Length; i++)
            {
                if (currentRow == 0)
                {
                    tempGrid = new Grid();
                }

                input[i] = input[i].Replace("  ", " ");
                var currentRowNumbers = input[i].Split(" ").Select(Int32.Parse).ToArray();
                tempGrid.Rows.Add(new List<Tile>());
                foreach (var number in currentRowNumbers)
                {
                    tempGrid.Rows.LastOrDefault().Add(new Tile { Value = number, IsMarked = false});
                }

                currentRow++;

                if (currentRow > 4)
                {
                    currentRow = 0;
                    i++;
                }

            }
        }
    }

    public class Tile
    {
        public int Value { get; set; }
        public bool IsMarked { get; set; }
    }

    public class Grid
    {
        public IList<List<Tile>> Rows { get; set; }
    }
}
