using System;
using System.IO;
using System.Diagnostics;
using System.Data.Common;
using System.Collections.Generic;
using static ABI.System.Collections.Generic.IReadOnlyDictionary_Delegates;
using Windows.Media.Audio;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AoC2023.solution
{
    public class AoCDay12
    {
        public List<int> pathSteps = new List<int>();
        public IDictionary<string, int> gridValues = new Dictionary<string, int>();
        public int[,] grid;
        int arrayLength = 0;
        int arrayWidth = 0;
        int targetLocationX = 0;
        int targetLocationY = 0;
        public AoCDay12(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int totalPossibleCombos = 0;

            foreach (string line in lines)
            {
                string[] springs = line.Split(" ");

                int possibleCombos = 0;

                int mapSize = springs[0].Length;
                int totalSprings = springs[1].Split(",").ToList().Sum(int.Parse);
                List<int> springGroupsList = springs[1].Split(",").Select(Int32.Parse).ToList();
                int totalSpringsUnknown = springs[0].Count(f => f == '?');
                int totalSpringGroups = springs[1].Split(",").Count();


                possibleCombos = validCombinations(springs[0].ToCharArray(), springGroupsList);

                totalPossibleCombos = totalPossibleCombos + possibleCombos;


                //Console.WriteLine("Map size: "+ mapSize+" and total springs: "+ totalSprings+ " and total groups: " + totalSpringGroups + " and total unknown areas: "+ totalSpringsUnknown);

            }

            output = "Part A:" + totalPossibleCombos;

        }

        static List<List<int>> GetCombination(List<int> list, int locationsToFill)
        {
            double count = Math.Pow(2, list.Count);
            List<List<int>> returnList = new List<List<int>>();
            for (int i = 1; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(list.Count, '0');
                int combos = 0;
                string options = "";
                List<int> tempList = new List<int>();
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        //Console.Write(list[j]);
                        options += list[j]+",";
                        tempList.Add(list[j]);
                        combos++;
                    }
                }
                if(combos== locationsToFill)
                {
                    //Console.WriteLine(options);
                    returnList.Add(tempList);
                }
                //Console.WriteLine();
            }
            return returnList;
        }
        public bool isValidList(char[] lineVales, List<int> combinationIDs, List<int> validGroups)
        {
            char[] updatedLineValues = new char[lineVales.Length];
            lineVales.CopyTo(updatedLineValues, 0);
            foreach(int combinationID in combinationIDs)
            {
                updatedLineValues[combinationID] = '#';
            }


            //regex code: 
            string pattern = @"([#]+)";
            Regex rg = new Regex(pattern);
            string updatedString = new string(updatedLineValues);
            MatchCollection matchedWords = rg.Matches(updatedString);
            //Console.WriteLine("Matches: "+ matchedWords.Count()+" and valid groups count: "+ validGroups.Count() + " in string: " + updatedString);
            if (matchedWords.Count() != validGroups.Count()) return false; // Not the right number of groups

            for (int i = 0; i < matchedWords.Count; i++)
            {
                
                if (matchedWords[i].Groups[1].Value.Length != validGroups[i]) return false; // Group not of the right size
            }

            //Console.WriteLine("got here");
            return true;
        }
        public int validCombinations(char[] springInput, List<int> sprintGroups)
        {
            int validCombos = 0;

            int totalSpringsUnknown = springInput.Count(f => f == '?');
            int totalSpringsKnown = springInput.Count(f => f == '#');
            int totalSpringsNotLocated = springInput.Count(f => f == '.');
            int possibleLocations = sprintGroups.Sum();
            char[] lineValues = springInput;
            int remainingLocations = possibleLocations - totalSpringsKnown;


            List<int> possibleOptions = new List<int>();
            for (int i = 0; i < springInput.Length; i++)
            {
                if (springInput[i] != '?') continue;
                possibleOptions.Add(i);
                lineValues[i] = springInput[i];
            }

            List<List<int>> combinationOptions = GetCombination(possibleOptions, remainingLocations);

            foreach (var combinationOption in combinationOptions)
            {
                bool validList = false;
                validList = isValidList(lineValues, combinationOption, sprintGroups);
                if(validList)
                {
                    validCombos++;
                }
            }




            List<char> springInputIndividual = new List<char>(springInput);


           
            int possibleCombinations = (totalSpringsUnknown * remainingLocations) / 2;





            //Console.WriteLine("Outstanding springs to find: " + remainingLocations);
            //Console.WriteLine("Possible combos: " + possibleCombinations);
            //Console.WriteLine("Valid combos: " + validCombos);



            return validCombos;
        }

        

        public string output;
    }
}