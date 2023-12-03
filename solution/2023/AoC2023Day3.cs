using System;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Reflection;
using System.Collections.Generic;

namespace AoC2023.solution
{
    public class AoCDay3
    {

        public AoCDay3(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );


            List<int> partNumbers = new List<int>();
            List<int> validPartNumbers = new List<int>();
            Dictionary<string, string> engineGears = new Dictionary<string, string>();
            Dictionary<string, int> validPartNumbersCoords = new Dictionary<string, int>();
            int engineSum = 0;

            int arrayLength = lines.Count();
            int arrayWidth = lines[0].Length;
            string[,] grid = new string[arrayLength, arrayWidth];
            int row = 0;
            int column = 0;


            string pattern = @"([0-9]{1,3})";
            Regex rg = new Regex(pattern);

            foreach (string line in lines)
            {
                MatchCollection foundNumbers = rg.Matches(line);
                for (int i = 0; i < foundNumbers.Count; i++)
                {
                    partNumbers.Add(Int32.Parse(foundNumbers[i].Groups[1].Value));
                    //foundEnginePartNumbers.Add("hi", Int32.Parse(foundNumbers[i].Groups[1].Value));
                }

                //string[] digits = line.Split(' ');
                foreach (char character in line)
                {
                    grid[row, column] = character.ToString();

                    //Console.WriteLine("\nAdding: " + row + " - " + column);
                    column++;
                }
                row++;
                column = 0;

            }
            row = 0;
            column = 0;
            foreach (string line in lines)
            {

                string[] digits = line.Split(' ');

                string currentNumber = "";
                int validEnginePart = 0;
                foreach (var character in line)
                {
                    int startedNumber = 0;
                    if (Char.IsDigit(character))
                    {
                        startedNumber = 1;
                        currentNumber = currentNumber + character;
                        string optionAbove = (row - 1 < 0) ? "." : grid[row - 1, column];
                        string optionBelow = (row + 1 >= arrayLength) ? "." : grid[row + 1, column];
                        string optionLeft = (column - 1 < 0) ? "." : grid[row, column - 1];
                        string optionRight = (column + 1 >= arrayWidth) ? "." : grid[row, column + 1];
                        string optionAboveLeft = (row - 1 < 0 || column - 1 < 0) ? "." : grid[row - 1, column - 1];
                        string optionAboveRight = (row - 1 < 0 || column + 1 >= arrayWidth) ? "." : grid[row - 1, column + 1];
                        string optionBelowLeft = (row + 1 >= arrayLength || column - 1 < 0) ? "." : grid[row + 1, column - 1];
                        string optionBelowRight = (row + 1 >= arrayLength || column + 1 >= arrayWidth) ? "." : grid[row + 1, column + 1];

                        if((optionAbove != "." && !Char.IsDigit(optionAbove, 0))
                           || (optionBelow != "." && !Char.IsDigit(optionBelow, 0))
                           || (optionLeft != "." && !Char.IsDigit(optionLeft, 0))
                           || (optionRight != "." && !Char.IsDigit(optionRight, 0))
                           || (optionAboveLeft != "." && !Char.IsDigit(optionAboveLeft, 0))
                           || (optionAboveRight != "." && !Char.IsDigit(optionAboveRight, 0))
                           || (optionBelowLeft != "." && !Char.IsDigit(optionBelowLeft, 0))
                           || (optionBelowRight != "." && !Char.IsDigit(optionBelowRight, 0)))
                        {
                            //Console.WriteLine("\n Current valid number:: " + currentNumber + " :: " + optionBelowRight + " - " + row + " - " + column);
                            validEnginePart = 1;
                            // too low 526341
                            // too high 592906
                        }

                    }
                    else if(validEnginePart == 1 && currentNumber.Length > 0)
                    {
                        if(currentNumber.Length < 2)
                        {
                            Console.WriteLine("\n Valid number to submit:: " + currentNumber);
                        }
                        
                        // We should only get here if we have a complete number that is also an invalid part
                        validPartNumbers.Add(Int32.Parse(currentNumber));
                        int newColumn = column - 1;
                        validPartNumbersCoords.Add(row+","+ newColumn, Int32.Parse(currentNumber));
                        
                        if (currentNumber.Length > 1)
                        {
                            newColumn = column - 2;
                            //Console.WriteLine("add 1 more digit - " + currentNumber + " :: " + row + "," + newColumn);
                            validPartNumbersCoords.Add(row + "," + newColumn, Int32.Parse(currentNumber));
                        }
                        if (currentNumber.Length > 2)
                        {
                            newColumn = column - 3;
                            //Console.WriteLine("add 2 more digit - " + currentNumber + " :: " + row + "," + newColumn);
                            validPartNumbersCoords.Add(row + "," + newColumn, Int32.Parse(currentNumber));
                        }

                            currentNumber = "";
                        validEnginePart = 0;
                    } else
                    {
                        startedNumber = 0;
                        currentNumber = "";
                    }
                    if(character.ToString() == "*")
                    {
                        // Engine gear
                        Console.WriteLine("Adding gear :: " + row + "," + column);
                        engineGears.Add(row+","+column,"*");
                    }
                    column++;
                    if(column >= arrayWidth && validEnginePart == 1 && currentNumber.Length > 0)
                    {
                        // I am so stupid.
                        // We should only get here if we have a complete number that is also an invalid part
                        validPartNumbers.Add(Int32.Parse(currentNumber));
                        int newColumn = column - 1;
                        validPartNumbersCoords.Add(row + "," + newColumn, Int32.Parse(currentNumber));
                        if (currentNumber.Length > 1)
                        {
                            newColumn = column - 2;
                            //Console.WriteLine("add 1 more digit - " + currentNumber + " :: " + row + "," + newColumn);
                            validPartNumbersCoords.Add(row + "," + newColumn, Int32.Parse(currentNumber));
                        }
                        if (currentNumber.Length > 2)
                        {
                            newColumn = column - 3;
                            //Console.WriteLine("add 2 more digit - " + currentNumber + " :: " + row + "," + newColumn);
                            validPartNumbersCoords.Add(row + "," + newColumn, Int32.Parse(currentNumber));
                        }

                        currentNumber = "";
                        validEnginePart = 0;
                    }
                }
                row++;
                column = 0;

            }









            foreach (int partNumber in validPartNumbers)
            {
                if (partNumber < 10)
                {
                    Console.WriteLine("\nsumming " + partNumber);
                }
                    engineSum = engineSum + partNumber;
                
                
            }

            output = "Part A: " + engineSum;
            int engineSumSecond = 0;
            int trackRow = 0;
            int trackColumn = 0;

            foreach (KeyValuePair<string, string> engineGear in engineGears)
            {

                List<int> possibleGearNumbers = new List<int>();
                //engineSum = engineSum + partNumber;
                //Console.WriteLine("\nGears " + engineGear.Key + " - " + engineGear.Value);
                string[] coords = engineGear.Key.Split(',');
                row = Int32.Parse(coords[0]);
                column = Int32.Parse(coords[1]);
                string optionAbove = (row - 1 < 0) ? "." : grid[row - 1, column];
                string optionBelow = (row + 1 >= arrayLength) ? "." : grid[row + 1, column];
                string optionLeft = (column - 1 < 0) ? "." : grid[row, column - 1];
                string optionRight = (column + 1 >= arrayWidth) ? "." : grid[row, column + 1];
                string optionAboveLeft = (row - 1 < 0 || column - 1 < 0) ? "." : grid[row - 1, column - 1];
                string optionAboveRight = (row - 1 < 0 || column + 1 >= arrayWidth) ? "." : grid[row - 1, column + 1];
                string optionBelowLeft = (row + 1 >= arrayLength || column - 1 < 0) ? "." : grid[row + 1, column - 1];
                string optionBelowRight = (row + 1 >= arrayLength || column + 1 >= arrayWidth) ? "." : grid[row + 1, column + 1];
                if(Char.IsDigit(optionAbove, 0))
                {
                    trackRow = row - 1;
                    trackColumn = column;
                    possibleGearNumbers.Add(validPartNumbersCoords[trackRow+","+trackColumn]);
                }
                if (Char.IsDigit(optionBelow, 0))
                {
                    trackRow = row + 1;
                    trackColumn = column;
                    possibleGearNumbers.Add(validPartNumbersCoords[trackRow + "," + trackColumn]);
                }
                if (Char.IsDigit(optionLeft, 0))
                {
                    trackRow = row;
                    trackColumn = column - 1;
                    possibleGearNumbers.Add(validPartNumbersCoords[trackRow + "," + trackColumn]);
                }
                if (Char.IsDigit(optionRight, 0))
                {
                    trackRow = row;
                    trackColumn = column + 1;
                    possibleGearNumbers.Add(validPartNumbersCoords[trackRow + "," + trackColumn]);
                }
                if (Char.IsDigit(optionAboveLeft, 0))
                {
                    trackRow = row - 1;
                    trackColumn = column - 1;
                    possibleGearNumbers.Add(validPartNumbersCoords[trackRow + "," + trackColumn]);
                }
                if (Char.IsDigit(optionAboveRight, 0))
                {
                    trackRow = row - 1;
                    trackColumn = column + 1;
                    possibleGearNumbers.Add(validPartNumbersCoords[trackRow + "," + trackColumn]);
                }
                if (Char.IsDigit(optionBelowLeft, 0))
                {
                    trackRow = row + 1;
                    trackColumn = column - 1;
                    possibleGearNumbers.Add(validPartNumbersCoords[trackRow + "," + trackColumn]);
                }
                if (Char.IsDigit(optionBelowRight, 0))
                {
                    trackRow = row + 1;
                    trackColumn = column + 1;
                    possibleGearNumbers.Add(validPartNumbersCoords[trackRow + "," + trackColumn]);
                }
                List<int> noDupes = possibleGearNumbers.Distinct().ToList();
                //noDupes.ForEach(i => Console.Write("{0}\t", i));
                if(noDupes.Count() == 2)
                {
                    engineSumSecond = engineSumSecond + (noDupes.First() * noDupes.Last());
                }

            }

            output += "\nPart B: " + engineSumSecond;


            /*

            int rucksackSize;
            IEnumerable<string> rucksacks;
            List<string> rucksackItemsFirst = new List<string>();
            List<string> rucksackItemsSecond = new List<string>();
            //Dictionary<int, string> rucksackItemsFirst = new Dictionary<int, string>();
            //Dictionary<int, string> rucksackItemsSecond = new Dictionary<int, string>();
            int rucksackID = 0;
            //int ruckspacePositionID = 0;
            int prioritiesSum = 0;
            foreach (string line in lines)
            {
                rucksackSize = line.Length;
                rucksacks = Split(line, rucksackSize / 2);
                rucksackItemsFirst.Clear();
                rucksackItemsSecond.Clear();
                rucksackID = 0;
                //ruckspacePositionID = 0;
                foreach (string rucksack in rucksacks)
                {
                    foreach (var character in rucksack)
                    {
                        // do something with each character.
                        if(rucksackID == 0)
                        {
                            rucksackItemsFirst.Add(character.ToString());
                        } else
                        {
                            rucksackItemsSecond.Add(character.ToString());
                        }
                        //ruckspacePositionID++;

                    }
                    rucksackID++;
                    //ruckspacePositionID = 0;
                }





                var commonDictionaryItems = rucksackItemsFirst.Intersect(rucksackItemsSecond);

                foreach (var common in commonDictionaryItems)
                {
                    string letter = common.ToString();
                    int index;
                    if (Char.IsUpper(letter[0]))
                    {
                        index = char.ToUpper(letter[0]) - 64 + 26;//index == 1
                    } else
                    {
                        index = char.ToUpper(letter[0]) - 64;//index == 1
                    }
                    prioritiesSum = prioritiesSum + index;
                }



            }


            output = "Part A: "+ prioritiesSum;
            prioritiesSum = 0;


            int currentLine = 0;

            List<string> rucksackItemsFirstBag = new List<string>();
            List<string> rucksackItemsSecondBag = new List<string>();
            List<string> rucksackItemsThirdBag = new List<string>();

            int totalLines = lines.Length;
            while (currentLine < totalLines)
            {
                foreach (var character in lines[currentLine])
                {
                    rucksackItemsFirstBag.Add(character.ToString());
                }
                foreach (var character in lines[currentLine + 1])
                {
                    rucksackItemsSecondBag.Add(character.ToString());
                }
                foreach (var character in lines[currentLine + 2])
                {
                    rucksackItemsThirdBag.Add(character.ToString());
                }


                HashSet<string> hashSet = new HashSet<string>(rucksackItemsFirstBag);
                hashSet.IntersectWith(rucksackItemsSecondBag);
                hashSet.IntersectWith(rucksackItemsThirdBag);
                List<string> intersection = hashSet.ToList();

                foreach (var common in intersection)
                {
                    string letter = common.ToString();
                    int index;
                    if (Char.IsUpper(letter[0]))
                    {
                        index = char.ToUpper(letter[0]) - 64 + 26;//index == 1
                    }
                    else
                    {
                        index = char.ToUpper(letter[0]) - 64;//index == 1
                    }
                    prioritiesSum = prioritiesSum + index;
                }

                currentLine = currentLine + 3;

                rucksackItemsFirstBag.Clear();
                rucksackItemsSecondBag.Clear();
                rucksackItemsThirdBag.Clear();
            }
             

            output += "\nPart B: " + prioritiesSum;


            */

        }

        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        public string output;
    }
}