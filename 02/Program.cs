using System;
using System.IO;

namespace _02
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "test.txt";
            
            foreach (var line in File.ReadLines(fileName))
            {
                Console.WriteLine(line.ToString());
            }
        }
    }
}
