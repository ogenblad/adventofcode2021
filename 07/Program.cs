using System;
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
            //sw.Start();
            
            var input = File.ReadAllText("input.txt");
            var numbers = input.Split(",").Select(Int32.Parse).ToList();
        }
    }
}
