using System;
using System.IO;
using System.Diagnostics;
using System.Security;

namespace AoC2015.solution
{
    public class AoCDay1
    {
        public AoCDay1(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int floor = 0;
            int iterator = 1;
            int lowestIterator = 50000000;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    //Console.WriteLine(character);
                    if (character.ToString() == "(")
                    {
                        floor++;
                    }
                    else
                    {
                        floor--;
                    }
                    if(floor == -1 && iterator < lowestIterator)
                    {
                        lowestIterator = iterator;
                    }
                    iterator++;
                }
            }

            output = "Part A: " + floor;
            output += "\nPart B: " + lowestIterator;            
        }
        public string output;

    }
}