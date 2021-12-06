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
            var input = File.ReadAllLines("input.txt");
            var numbers = input[0].Split(",").Select(Int32.Parse).ToList();
            var tiles = ParseTiles(input);
            PlayBingo(tiles, numbers);
        }
        
        public static void PlayBingo(List<Tile> tiles, List<int> numbers)
        {
            int gridCount = tiles.GroupBy(t => t.Grid)
                .Select(g => g.First())
                .Count();
            int bingoCount = 0;
            List<int> gridsWithBingo = new List<int>();

            foreach (var number in numbers)
            {
                var matchingTiles = tiles.Where(t => t.Value == number).ToList();
                
                foreach (var matchingTile in matchingTiles)
                {
                    matchingTile.IsMarked = true;
                    var markedInCurrentRow = (from Tile in tiles
                    where Tile.Grid == matchingTile.Grid && Tile.GridRow == matchingTile.GridRow && Tile.IsMarked == true
                    select Tile.IsMarked).Count();

                    if (markedInCurrentRow == 5)
                    {
                        bingoCount++;
                        var gridAllreadHasBingo = gridsWithBingo.Exists(g => g == matchingTile.Grid);

                        if (bingoCount == 1)
                        {
                            var puzzleAnswer = (from Tile in tiles
                            where Tile.Grid == matchingTile.Grid && Tile.IsMarked == false
                            select Tile.Value).Sum() * matchingTile.Value;
                            
                            Console.WriteLine();
                            Console.WriteLine("BINGO!");
                            Console.WriteLine($"Winning row: {matchingTile.GridRow} on grid: {matchingTile.Grid}");
                            Console.WriteLine();
                            Console.WriteLine($"Puzzle answer: {puzzleAnswer}");
                        }
                        else if (gridsWithBingo.Count() == gridCount -1 && gridAllreadHasBingo == false)
                        {
                            var puzzleAnswer = (from Tile in tiles
                            where Tile.Grid == matchingTile.Grid && Tile.IsMarked == false
                            select Tile.Value).Sum() * matchingTile.Value;

                            Console.WriteLine();
                            Console.WriteLine("LOOSER!");
                            Console.WriteLine($"Last bingo row: {matchingTile.GridRow} on grid: {matchingTile.Grid}");
                            Console.WriteLine();
                            Console.WriteLine($"Puzzle answer: {puzzleAnswer}");
                        }

                        if (!gridAllreadHasBingo) {
                            gridsWithBingo.Add(matchingTile.Grid);
                        }
                    }
                }
            }

            if (bingoCount < 1)
            {
                Console.WriteLine();
                Console.WriteLine("No winner here...");
            }
        }
        
        public static List<Tile> ParseTiles(string[] input)
        {
            int currentRow = 0;
            int currentGrid = 0;
            List<Tile> parsedTiles = new List<Tile>();

            for ( var i=2; i < input.Length; i++)
            {
                input[i] = input[i].Replace("  ", " ").Trim();
                var currentRowNumbers = input[i].Split(" ").Select(Int32.Parse).ToArray();

                foreach (var number in currentRowNumbers)
                {
                    parsedTiles.Add(new Tile {Value = number, Grid = currentGrid, GridRow = currentRow, IsMarked = false});
                }

                currentRow++;

                if (currentRow > 4)
                {
                    currentRow = 0;
                    currentGrid++;
                    i++;
                }
            }
            return parsedTiles;
        }
    }

    public class Tile
    {
        public int Value { get; set; }
        public int Grid { get; set;}
        public int GridRow { get; set; }
        public bool IsMarked { get; set; }
    }
}
