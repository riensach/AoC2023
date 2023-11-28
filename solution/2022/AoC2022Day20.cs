using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace AoC2022.solution
{
    public class AoCDay20
    {
        public List<(int Index, long Value)> files = new List<(int Index, long Value)>();
        public List<(int Index, long Value)> filesCopy = new List<(int Index, long Value)>();

        public AoCDay20(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int multiplier = 1;
            int totalIterations = 1;
            long resultValue = solve(lines, multiplier, totalIterations);
            output += "Part A: " + resultValue;
            
            multiplier = 811589153;
            totalIterations = 10;
            resultValue = solve(lines, multiplier, totalIterations);
            output += "\nPart B: " + resultValue;            

        }

        public long solve(string[] lines, int multiplier, int totalIterations)
        {
            files = lines.Select((i, index) => (index, long.Parse(i) * multiplier)).ToList();
            filesCopy = new List<(int Index, long Value)>(files);
            int length = filesCopy.Count() - 1;
            for (int i = 0; i < totalIterations; i++)
            {
                foreach(var (index, value) in files)
                {
                    int initialPosition = filesCopy.IndexOf((index, value));
                    int newPosition = (int)((initialPosition + value) % length);
                    if (newPosition  < 0)
                    {
                        newPosition = newPosition + length;
                    }
                    filesCopy.RemoveAt(initialPosition);
                    filesCopy.Insert(newPosition, (index, value));
                }
            }            

            int findFirstItem = filesCopy.FindIndex(i => i.Value == 0);
            int firstFile = (findFirstItem + 1000) % filesCopy.Count();
            int secondFile = (findFirstItem + 2000) % filesCopy.Count();
            int thirdFile = (findFirstItem + 3000) % filesCopy.Count();
            long grooveCoordSum = filesCopy[firstFile].Value + filesCopy[secondFile].Value + filesCopy[thirdFile].Value;

            return grooveCoordSum;
        }

        public string output;
    }
}