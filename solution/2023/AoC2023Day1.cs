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

            List<int> elves = new List<int>();

            
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
                string value = "";
                value = numbers[0] + "" + numbers.Last();
                elves.Add(Int32.Parse(value));
                

            }
            int totalValue = 0;
            foreach (int elf in elves)
            {
                totalValue += elf;
            }


            output = "Part A value: " + totalValue;
            
            List<string> words = new List<string>();
            words.Add("nine");
            words.Add("eight");
            words.Add("seven");
            words.Add("six");
            words.Add("five");
            words.Add("four");
            words.Add("three");
            words.Add("two");
            words.Add("one");
            words.Add("zero");

            Dictionary<string, long> numberTable =
    new Dictionary<string, long>
        {{"one",1},{"two",2},{"three",3},{"four",4},
        {"five",5},{"six",6},{"seven",7},{"eight",8},{"nine",9}};

        string pattern = @"(?=(one|two|three|four|five|six|seven|eight|nine))";
            Regex rg = new Regex(pattern);




            foreach (string line in lines)
            {

                MatchCollection matchedWords = rg.Matches(line);
                //MatchCollection matchedWords = rg.Match;
                string newText = line;
                int indexMod = 0;
                int replaceCount = 0;
                for (int i = 0; i < matchedWords.Count; i++)
                {
                    //Console.Write(newText + " ::a \n");
                    string oldText = newText;
                    newText = ReplaceFirst(newText, matchedWords[i].Groups[1].Value, numberTable[matchedWords[i].Groups[1].Value].ToString());
                    //newText = newText.Replace("" + matchedWords[i].Groups[1].Value, "" + numberTable[matchedWords[i].Groups[1].Value]);
                    
                    if (oldText == newText)
                    {
                        //Console.Write(matchedWords[i].Groups[1].Index + " ::dfdsdsf \n");
                        newText = newText.Insert(matchedWords[i].Groups[1].Index - indexMod + 1, numberTable[matchedWords[i].Groups[1].Value].ToString());
                        //Console.Write(numberTable[matchedWords[i].Groups[1].Value].ToString() + " ::dfdsdsf \n");
                    }
                    indexMod = indexMod + matchedWords[i].Groups[1].Value.Length - 1;
                    replaceCount++;
                    //Console.Write(newText + " ::b \n");
                    //Console.Write(indexMod + " ::b \n");
                    //Console.Write(replaceCount + " ::b \n");
                }

                List<string> numbers = new List<string>();

                //Console.Write(newText + " - ");

                foreach (var character in newText)
                {
                    int n;
                    bool isNumeric = int.TryParse(character.ToString(), out n);
                    if (isNumeric)
                    {
                        numbers.Add(character.ToString());
                    }


                }
                string value = "";
                value = numbers[0] + "" + numbers.Last();
                //Console.Write(value + "\n");
                elves.Add(Int32.Parse(value));


            }
            int totalValueSecond = 0;
            foreach (int elf in elves)
            {
                totalValueSecond += elf;
            }

            output += "\nPart B value: " + totalValueSecond;

            //output = partA(elves);
            //output += partB(elves);

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

        private string partA(List<int> calories) {
            int value = 0;     
            value = calories[0];
            output = "Part A value: " + value;
            return output;
        }

        private string partB(List<int> calories)
        {
            int value = 0;
            value = calories[0] + calories[1] + calories[2];
            output = "\nPart B value: " + value;
            return output;
        }

    }
}