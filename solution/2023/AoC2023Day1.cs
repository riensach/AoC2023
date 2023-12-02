using System;
using System.IO;
using System.Diagnostics;
using System.Security;
using System.Windows.Markup;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Windows.Devices.Power;

namespace AoC2023.solution
{
    public class AoCDay1
    {

        public AoCDay1(int selectedPart, string input)
        {

            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            List<int> calibrationValues = new List<int>();
            List<int> calibrationValuesSecond = new List<int>();
            
            foreach (string line in lines)
            {
                List<string> numbers = new List<string>();
                foreach (var character in line)
                {
                    int n;
                    bool isNumeric = int.TryParse(character.ToString(), out n);
                    if (isNumeric)
                    {
                        numbers.Add(character.ToString());
                    }
                    
                }
                string value = numbers[0] + "" + numbers.Last();
                calibrationValues.Add(Int32.Parse(value));                

            }

            int totalValue = 0;
            foreach (int value in calibrationValues)
            {
                totalValue += value;
            }

            output = calculationOutput(calibrationValues, "A");            

            Dictionary<string, long> numberTable =
                new Dictionary<string, long>
                {{"one",1},{"two",2},{"three",3},{"four",4},
                {"five",5},{"six",6},{"seven",7},{"eight",8},{"nine",9}};

            string pattern = @"(?=(one|two|three|four|five|six|seven|eight|nine))";
            Regex rg = new Regex(pattern);

            foreach (string line in lines)
            {

                MatchCollection matchedWords = rg.Matches(line);
                string newText = line;
                int indexMod = 0;
                int replaceCount = 0;
                for (int i = 0; i < matchedWords.Count; i++)
                {
                    string oldText = newText;
                    newText = ReplaceFirst(newText, matchedWords[i].Groups[1].Value, numberTable[matchedWords[i].Groups[1].Value].ToString());
                    
                    if (oldText == newText)
                    {
                        newText = newText.Insert(matchedWords[i].Groups[1].Index - indexMod + 1, numberTable[matchedWords[i].Groups[1].Value].ToString());
                    }
                    indexMod = indexMod + matchedWords[i].Groups[1].Value.Length - 1;
                    replaceCount++;
                }

                List<string> numbers = new List<string>();

                foreach (var character in newText)
                {
                    int n;
                    bool isNumeric = int.TryParse(character.ToString(), out n);
                    if (isNumeric)
                    {
                        numbers.Add(character.ToString());
                    }

                }
                string value = numbers[0] + "" + numbers.Last();
                calibrationValuesSecond.Add(Int32.Parse(value));

            }

            int totalValueSecond = 0;
            foreach (int value in calibrationValuesSecond)
            {
                totalValueSecond += value;
            }
            
            output += calculationOutput(calibrationValuesSecond, "B");

        }

        public string output;

        public string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        private string calculationOutput(List<int> calibrationValues, string partLabel) {
            int totalValue = 0;
            foreach (int value in calibrationValues)
            {
                totalValue += value;
            }
            output = "\nPart "+ partLabel + " value: " + totalValue;
            return output;
        }

    }
}