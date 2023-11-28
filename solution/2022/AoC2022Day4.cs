using System;
using System.IO;
using System.Diagnostics;

namespace AoC2022.solution
{
    public class AoCDay4
    {

        public AoCDay4(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int fullyContained = 0;
            int overlapped = 0;

            foreach (string line in lines)
            {
                string[] eitherSide = line.Split(',');
                string[] firstItems = eitherSide[0].Split('-');
                string[] secondItems = eitherSide[1].Split('-');

                List<int> firstItemsList = new List<int>();
                List<int> secondItemsList = new List<int>();

                for (int i = Int32.Parse(firstItems[0]); i <= Int32.Parse(firstItems[1]); i++)
                {
                    firstItemsList.Add(i);
                }

                for (int i = Int32.Parse(secondItems[0]); i <= Int32.Parse(secondItems[1]); i++)
                {
                    secondItemsList.Add(i);
                }

                if((firstItemsList.First() >= secondItemsList.First() && firstItemsList.Last() <= secondItemsList.Last()) || (secondItemsList.First() >= firstItemsList.First() && secondItemsList.Last() <= firstItemsList.Last()))
                {
                    fullyContained++;
                }

                if ((firstItemsList.First() >= secondItemsList.First() && firstItemsList.First() <= secondItemsList.Last()) || (secondItemsList.First() >= firstItemsList.First() && secondItemsList.First() <= firstItemsList.Last()))
                {
                    overlapped++;
                }

            }

            output = "Part A: " + fullyContained;
            output += "\nPart B: " + overlapped;
        }

        public string output;
    }
}