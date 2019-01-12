namespace p022
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Data
    {
        private readonly Dictionary<char, int> letterScores;
        private int lineNumber = 1; // <---- After all we are supose to start at line 1... :|
        public ulong result = 0;

        public Data()
        {
            letterScores = new Dictionary<char, int>()
            {
                {'A', 1},
                {'B', 2},
                {'C', 3},
                {'D', 4},
                {'E', 5},
                {'F', 6},
                {'G', 7},
                {'H', 8},
                {'I', 9},
                {'J', 10},
                {'K', 11},
                {'L', 12},
                {'M', 13},
                {'N', 14},
                {'O', 15},
                {'P', 16},
                {'Q', 17},
                {'R', 18},
                {'S', 19},
                {'T', 20},
                {'U', 21},
                {'V', 22},
                {'W', 23},
                {'X', 24},
                {'Y', 25},
                {'Z', 26},
            };
        }

        public void ParseLine(string word)
        {
            var value = 0;
            foreach (var character in word)
            {
                if (character == '"' || character == ',')
                {
                    continue;
                }

                value += GetValue(character);
            }

            value *= this.lineNumber;
            result += (ulong) value;
            PrettyPrint(word, value);
        }

        public void IncrementLine()
        {
            lineNumber++;
        }

        private int GetValue(char letter)
        {
            return this.letterScores[letter];
        }

        private void PrettyPrint(string word, int value)
        {
            if (this.lineNumber > 40) return;
            Console.WriteLine($"[line:{this.lineNumber} | word:{word} | value:{value} | accumulated:{result}]");
        }
    }

    public static class Program
    {
        static void Main(string[] args)
        {
            var words = new List<string>();
            var data = new Data();
            var filename = "p022_names.txt";
            var path = Directory.GetCurrentDirectory() + "\\" + filename;
            using (var fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var bs = new BufferedStream(fs))
            using (var sr = new StreamReader(bs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    words.Add(line.Trim('\'', ','));
                }
            }

            words.Sort();
            foreach (var word in words)
            {
                data.ParseLine(word);
                data.IncrementLine();
            }
            
            Console.WriteLine($"Result: {data.result}");
            Console.ReadKey();
        }
    }
}