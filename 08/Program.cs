using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace _08
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read input values and parse signal patterns by pipe to new array
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var input = File.ReadAllLines("input.txt");
            List<Tuple<string, string>> signalPatterns = new List<Tuple<string, string>>();
            foreach (var row in input)
            {
                var temp = row.Split(" | ");
                signalPatterns.Add(Tuple.Create(temp[0], temp[1]));
            }

            // calculate numbers
            var outputDigitsA1 = CalculateOutputValue(signalPatterns);
            var outputDigitsA2 = DecodeSignalPatterns(signalPatterns);
            sw.Stop();

            Console.WriteLine($"TASK1: Number of instances for digits 1,4,7,8: {outputDigitsA1.Count()}");
            Console.WriteLine($"TASK2: Output digits sum: {outputDigitsA2}");
            Console.WriteLine($"Execution time: {sw.Elapsed.TotalMilliseconds}ms");
        }

        public static List<int> CalculateOutputValue(List<Tuple<string, string>> input)
        {
            List<int> outputDigits = new List<int>();

            foreach (var signals in input)
            {
                // Create list of each signal patterns
                var signalPatterns = signals.Item2.Split(" ").ToList();

                // Parse digits with unique number of segments
                foreach (var signalPattern in signalPatterns)
                {
                    switch (signalPattern.Count())
                    {
                        case 2:
                            outputDigits.Add(1);
                            break;
                        case 3:
                            outputDigits.Add(7);
                            break;
                        case 4:
                            outputDigits.Add(4);
                            break;
                        case 7:
                            outputDigits.Add(8);
                            break;
                    }
                }
                
            }

            return outputDigits;
        }

        public static int DecodeSignalPatterns(List<Tuple<string, string>> input)
        {
            List<Tuple<string, string>> decryptedSignals = new List<Tuple<string, string>>();
            List<Tuple<int, string>> decodedDigits = new List<Tuple<int, string>>();
            List<int> outputValues = new List<int>();

            foreach (var signals in input)
            {
                // Create list of each signal patterns
                var signalPatterns = signals.Item1.Split(" ").ToList();
                var digitPatterns = signals.Item2.Split(" ").ToList();
                
                // Parse digits with unique number of segments
                foreach (var signalPattern in signalPatterns)
                {
                    switch (signalPattern.Count())
                    {
                        case 2:
                            decodedDigits.Add(Tuple.Create(1, signalPattern));
                            break;
                        case 3:
                            decodedDigits.Add(Tuple.Create(7, signalPattern));
                            break;
                        case 4:
                            decodedDigits.Add(Tuple.Create(4, signalPattern));
                            break;
                        case 7:
                            decodedDigits.Add(Tuple.Create(8, signalPattern));
                            break;
                    }
                }

                foreach (var matchedPattern in decodedDigits)
                {
                   signalPatterns.Remove(matchedPattern.Item2);
                }

                // Start decrypt signals and decode digits that does not have a unique pattern by count
                // decrypt signal a
                decryptedSignals.Add(Tuple.Create("a", CalculateSignalPatternDiff(GetDecodedDigitPattern(decodedDigits, 7), GetDecodedDigitPattern(decodedDigits, 1)).FirstOrDefault().ToString()));

                // decode digit 6 amd decrypt signal c & f
                var selection = signalPatterns.Where(s => s.Length == 6).Select(s => String.Concat(s.OrderBy(c => c))).ToList();
                var diff = CalculateSignalPatternDiff(GetDecodedDigitPattern(decodedDigits, 8), GetDecodedDigitPattern(decodedDigits, 1)).ToArray();
                var chars = GetDecodedDigitPattern(decodedDigits, 1).ToCharArray();
                var testForSixA = String.Concat(new string(diff.Append(chars[0]).ToArray()).OrderBy(c => c));
                var testForSixB = String.Concat(new string(diff.Append(chars[1]).ToArray()).OrderBy(c => c));

                if (selection.Where(s => s == testForSixA).Select(s => s).DefaultIfEmpty().FirstOrDefault() != null)
                {
                    decryptedSignals.Add(Tuple.Create("f", chars[0].ToString()));
                    decryptedSignals.Add(Tuple.Create("c", chars[1].ToString()));
                    string six = signalPatterns.Where(s => String.Concat(s.OrderBy(c => c)) == testForSixA).Select(s => s).FirstOrDefault();
                    decodedDigits.Add(Tuple.Create(6, six));
                    signalPatterns.Remove(six);
                    selection.Remove(testForSixA);
                }
                else
                {
                    decryptedSignals.Add(Tuple.Create("c", chars[0].ToString()));
                    decryptedSignals.Add(Tuple.Create("f", chars[1].ToString()));
                    string six = signalPatterns.Where(s => String.Concat(s.OrderBy(c => c)) == testForSixB).Select(s => s).FirstOrDefault();
                    decodedDigits.Add(Tuple.Create(6, six));
                    signalPatterns.Remove(six);
                    selection.Remove(testForSixB);
                }

                testForSixA = null;
                testForSixB = null;

                // 9 and 0 left in selection
                diff = CalculateSignalPatternDiff(GetDecodedDigitPattern(decodedDigits, 8), selection.FirstOrDefault()).ToArray();
                if (GetDecodedDigitPattern(decodedDigits, 4).Contains(new string(diff)))
                {
                    // Found 0
                    var zero = signalPatterns.Where(s => String.Concat(s.OrderBy(c => c)) == selection.FirstOrDefault()).Select(s => s).FirstOrDefault();
                    var nine = signalPatterns.Where(s => String.Concat(s.OrderBy(c => c)) == selection.LastOrDefault()).Select(s => s).FirstOrDefault();
                    decodedDigits.Add(Tuple.Create(0, zero));
                    decryptedSignals.Add(Tuple.Create("d", new String(diff)));
                    decodedDigits.Add(Tuple.Create(9, nine));
                    decryptedSignals.Add(Tuple.Create("e", new String(CalculateSignalPatternDiff(GetDecodedDigitPattern(decodedDigits, 8), selection.LastOrDefault()).ToArray())));
                    signalPatterns.Remove(zero);
                    signalPatterns.Remove(nine);
                }
                else
                {
                    // Found 9
                    var nine = signalPatterns.Where(s => String.Concat(s.OrderBy(c => c)) == selection.FirstOrDefault()).Select(s => s).FirstOrDefault();
                    var zero = signalPatterns.Where(s => String.Concat(s.OrderBy(c => c)) == selection.LastOrDefault()).Select(s => s).FirstOrDefault();
                    decodedDigits.Add(Tuple.Create(9, nine));
                    decryptedSignals.Add(Tuple.Create("e", new String(diff)));
                    decodedDigits.Add(Tuple.Create(0, zero));
                    decryptedSignals.Add(Tuple.Create("d", new String(CalculateSignalPatternDiff(GetDecodedDigitPattern(decodedDigits, 8), selection.LastOrDefault()).ToArray())));
                    signalPatterns.Remove(nine);
                    signalPatterns.Remove(zero);
                }

                selection = null;

                // Calculate 5
                var signalsInFive = GetDecodedDigitPattern(decodedDigits, 8)
                    .Replace(decryptedSignals.Where(s => s.Item1 == "c").Select(s => s.Item2).FirstOrDefault(), "")
                    .Replace(decryptedSignals.Where(s => s.Item1 == "e").Select(s => s.Item2).FirstOrDefault(), "");
                var five = signalPatterns.Where(s => String.Concat(s.OrderBy(c => c)) == String.Concat(signalsInFive.OrderBy(c => c))).Select(s => s).FirstOrDefault();
                decodedDigits.Add(Tuple.Create(5, five));
                signalPatterns.Remove(five);

                signalsInFive = null;
                five = null;

                // Calculate 3 and 2
                var three = signalPatterns.Where(s => s.Contains(decryptedSignals.Where(d => d.Item1 == "f").Select(d => d.Item2).FirstOrDefault())).Select(s => s).FirstOrDefault();
                decodedDigits.Add(Tuple.Create(3, three));
                signalPatterns.Remove(three);
                decodedDigits.Add(Tuple.Create(2, signalPatterns.FirstOrDefault()));

                // Get output value
                string outputValue = "";
                foreach (var digitPattern in digitPatterns)
                {
                    outputValue += decodedDigits.Where(d => String.Concat(d.Item2.OrderBy(c => c)) == String.Concat(digitPattern.OrderBy(c => c))).Select(d => d.Item1).FirstOrDefault().ToString();
                }

                outputValues.Add(Int32.Parse(outputValue));
                decodedDigits.Clear();
                decryptedSignals.Clear();
            }

            return outputValues.Sum();
        }

        public static IEnumerable<char> CalculateSignalPatternDiff(string input, string remove)
        {
            var inputList = input.ToCharArray();
            var removeList = remove.ToCharArray();

            return inputList.Except(removeList);
        }

        public static string GetDecodedDigitPattern(List<Tuple<int, string>> decodedDigits, int digit)
        {
            var result = decodedDigits.Where(d => d.Item1 == digit).Select(d => d.Item2).FirstOrDefault();
            return result.Length > 0 ? result : "";
        }
    }
}
