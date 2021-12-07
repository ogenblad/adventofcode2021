using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _05
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var parsedLines = ParseLines(input);
            var minMaxPositions = GetPositionMinMax(parsedLines);
            var map = PlotLines(parsedLines, minMaxPositions[1], minMaxPositions[3], false);
            DrawPlot(map);
        }
        public static void DrawPlot(int[,] map)
        {
            Console.WriteLine("Plot:");
            int lineOverlaps = 0;

            for (int y = 0; y < map.GetLength(1); y++)
            {
                string row = "";
                
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    row += map[x, y] == 0 ? "." : map[x, y].ToString();
                    if (map[x, y] > 1) { lineOverlaps++; }
                }

                Console.WriteLine(row);
            }
            
            Console.WriteLine();
            Console.WriteLine($"Overlap count: {lineOverlaps}");
            Console.WriteLine();
        }
        public static int[,] PlotLines(List<Line> input, int xSize, int ySize, bool discardDiagonal = true)
        {
            int[,] map = new int[xSize+1, ySize+1];

            foreach (var line in input)
            {
                bool diagonalLine = (line.XStart == line.XEnd || line.YStart == line.YEnd) ? false : true;

                if ((diagonalLine && !discardDiagonal) || !diagonalLine)
                {
                    int xDiff = line.XStart == line.XEnd ? 0 : (line.XStart > line.XEnd ? line.XEnd - line.XStart : (line.XStart - line.XEnd) * -1); //line.XStart + (line.XEnd - line.XStart));
                    int yDiff = line.YStart == line.YEnd ? 0 : (line.YStart > line.YEnd ? line.YEnd - line.YStart : (line.YStart - line.YEnd) * -1); //line.YStart + (line.YEnd - line.YStart));

                    int xStepCount = xDiff < 0 ? xDiff * -1 : xDiff;
                    int yStepCount = yDiff < 0 ? yDiff * -1 : yDiff;

                    int currentXpos = 0;
                    int currentYpos = 0;

                    Console.WriteLine($"Processing line {line.XStart}, {line.YStart}, {line.XEnd}, {line.YEnd}");
                    Console.WriteLine($"XDiff: {xDiff}, YDiff: {yDiff}");
                    Console.WriteLine($"XStep: {xStepCount}, YStep: {yStepCount}");
                    Console.WriteLine();
                    
                    if (xStepCount >= yStepCount)
                    {
                        int counter = 0;                    
                        for (var i = 0; i <= xStepCount; i++)
                        {
                            if (xDiff < 0){
                                currentXpos = line.XStart - i;
                            }
                            else if (xDiff > 0)
                            {
                                currentXpos = line.XStart + i;
                            }
                            else
                            {
                                currentXpos = line.XStart;
                            }

                            if (yDiff < 0 && counter <= yStepCount)
                            {
                                currentYpos = line.YStart -counter;
                                counter++;
                            }
                            else if (yDiff > 0 && counter <= yStepCount)
                            {
                                currentYpos = line.YStart +counter;
                                counter++;
                            }
                            else
                            {
                                currentYpos = line.YStart;
                            }

                            map[currentXpos, currentYpos]++;
                        }
                    }
                    else if (yStepCount > xStepCount)
                    {
                        int counter = 0;                    
                        for (var i = 0; i <= yStepCount; i++)
                        {
                            if (yDiff < 0){
                                currentYpos = line.YStart - i;
                            }
                            else if (yDiff > 0)
                            {
                                currentYpos = line.YStart + i;
                            }
                            else
                            {
                                currentYpos = line.YStart;
                            }

                            if (xDiff < 0 && counter <= xStepCount)
                            {
                                currentXpos = line.XStart - counter;
                                counter++;
                            }
                            else if (xDiff > 0 && counter <= xStepCount)
                            {
                                currentXpos = line.XStart + counter;
                                counter++;
                            }
                            else
                            {
                                currentXpos = line.XStart;
                            }

                            map[currentXpos, currentYpos]++;
                        }
                    }
                }
            }

            return map;
        }

        public static List<Line> ParseLines(string[] input)
        {
            List<Line> parsedLines = new List<Line>();

            foreach (var line in input)
            {
                var positions = line.Split(" -> ");
                var start = positions[0].Split(",").Select(Int32.Parse).ToArray();
                var end = positions[1].Split(",").Select(Int32.Parse).ToArray();

                parsedLines.Add(new Line { XStart = start[0], XEnd = end[0], YStart = start[1], YEnd = end[1] } );
            }
            
            return parsedLines;
        }

        public static int[] GetPositionMinMax(List<Line> input)
        {
            var minXStart = (from Line in input select Line.XStart).Min();
            var maxXStart = (from Line in input select Line.XStart).Max();
            var minXEnd = (from Line in input select Line.XEnd).Min();
            var maxXEnd = (from Line in input select Line.XEnd).Max();
            var minYStart = (from Line in input select Line.YStart).Min();
            var maxYStart = (from Line in input select Line.YStart).Max();
            var minYEnd = (from Line in input select Line.YEnd).Min();
            var maxYEnd = (from Line in input select Line.YEnd).Max();

            var minX = minXStart <= minXEnd ? minXStart : minXStart;
            var maxX = maxXStart >= maxXEnd ? maxXStart : maxXEnd;
            var minY = minYStart <= minYEnd ? minYStart : minYStart;
            var maxY = maxYStart >= maxYEnd ? maxYStart : maxYEnd;

            return new int[] {minX, maxX, minY, maxY};
        }
    }
    public class Line
    {
        public int XStart { get; set; }
        public int XEnd { get; set; }
        public int YStart { get; set; }
        public int YEnd { get; set; }
    }
}
