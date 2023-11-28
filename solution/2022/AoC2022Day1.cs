using System;
using System.IO;
using System.Diagnostics;
using System.Security;

namespace AoC2022.solution
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

            int calories = 0;
            foreach (string line in lines)
            {
                if (line == null || line == "")
                {
                    elves.Add(calories);
                    calories = 0;
                    continue;
                }
                calories += Int32.Parse(line);

            }
            elves.Sort();
            elves.Reverse();

            output = partA(elves);
            output += partB(elves);
            
        }

        public string output;

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