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
        public IDictionary<string, int> validCombos = new Dictionary<string, int>();
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
                validCombos.Add(line, possibleCombos);

                totalPossibleCombos = totalPossibleCombos + possibleCombos;

                Console.WriteLine("Map size: "+ mapSize+" and total springs: "+ totalSprings+ " and total groups: " + totalSpringGroups + " and total unknown areas: "+ totalSpringsUnknown);

            }


            int countedCombos = validCombos.Count;
            output = "Part A:" + totalPossibleCombos;


            
            
            int totalPossibleCombosSecond = 0;

            foreach (string line in lines)
            {
                string[] springs = line.Split(" ");

                int possibleCombos = 0;
                springs[0] = springs[0] + "?" + springs[0] + "?" + springs[0] + "?" + springs[0] + "?" + springs[0];
                springs[1] = springs[1] + "," + springs[1] + "," + springs[1] + "," + springs[1] + "," + springs[1];

                int mapSize = springs[0].Length;
                int totalSprings = springs[1].Split(",").ToList().Sum(int.Parse);
                List<int> springGroupsList = springs[1].Split(",").Select(Int32.Parse).ToList();
                int totalSpringsUnknown = springs[0].Count(f => f == '?');
                int totalSpringGroups = springs[1].Split(",").Count();

                possibleCombos = validCombinations(springs[0].ToCharArray(), springGroupsList);
                validCombos.Add(springs[0] + springs[1], possibleCombos);

                totalPossibleCombosSecond = totalPossibleCombosSecond + possibleCombos;
                //Console.WriteLine(springs[0] + " - " + springs[1]);
                //Console.WriteLine("Map size: "+ mapSize+" and total springs: "+ totalSprings+ " and total groups: " + totalSpringGroups + " and total unknown areas: "+ totalSpringsUnknown);

            }
            /*
             foreach (var valueHistory in validCombos)
             {
                 foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(valueHistory))
                 {
                     string name = descriptor.Name;
                     object value = descriptor.GetValue(valueHistory);
                     Console.WriteLine("{0}={1}", name, value);
                 }
             }*/

             output += "\nPart B:" + totalPossibleCombosSecond;



        }

        static HashSet<HashSet<int>> GetCombination(HashSet<int> list, int locationsToFill)
        {
            double count = Math.Pow(2, list.Count);
            HashSet<HashSet<int>> returnList = new HashSet<HashSet<int>>();
            for (int i = 0; i < count; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(list.Count, '0');
                int combos = 0;
                string options = "";
                HashSet<int> tempList = new HashSet<int>();
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        //Console.Write(list.ElementAt(j));
                        options += list.ElementAt(j)+",";
                        tempList.Add(list.ElementAt(j));
                        combos++;
                    }
                }
                if(combos== locationsToFill)
                {
                    //Console.WriteLine(options);
                    returnList.Add(tempList);
                }
                //Console.WriteLine("hello - " + locationsToFill);
            }
            return returnList;
        }
        public bool isValidList(char[] lineVales, HashSet<int> combinationIDs, List<int> validGroups)
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
            //Console.WriteLine("Got past first check");
            for (int i = 0; i < matchedWords.Count; i++)
            {
                
                if (matchedWords[i].Groups[1].Value.Length != validGroups.ElementAt(i)) return false; // Group not of the right size
            }
            //Console.WriteLine("Matches: " + matchedWords.Count() + " and valid groups count: " + validGroups.Count() + " in string: " + updatedString);

            //Console.WriteLine("Returning true");
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





            HashSet<int> possibleOptions = new HashSet<int>();
            HashSet<int> initialOption = new HashSet<int>();
            for (int i = 0; i < springInput.Length; i++)
            {
                if (springInput[i] == '?')
                {
                    possibleOptions.Add(i);
                    lineValues[i] = springInput[i];
                } else if (springInput[i] == '#')
                {
                    initialOption.Add(i);
                }

            }

            HashSet<HashSet<int>> combinationOptions = GetCombination(possibleOptions, remainingLocations);
            //combinationOptions.Add(initialOption);

            foreach (var combinationOption in combinationOptions)
            {
                bool validList = false;
                validList = isValidList(lineValues, combinationOption, sprintGroups);
                if(validList)
                {
                    validCombos++;
                }
            }




            //List<char> springInputIndividual = new List<char>(springInput);


           
            //int possibleCombinations = (totalSpringsUnknown * remainingLocations) / 2;





            //Console.WriteLine("Outstanding springs to find: " + remainingLocations);
            //Console.WriteLine("Possible combos: " + possibleCombinations);
            //Console.WriteLine("Valid combos: " + validCombos);



            return validCombos;
        }

        // 7170 wrong too low (and 7171)
        // 7179 too high

        public string output;
    }
}