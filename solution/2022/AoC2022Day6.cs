using System;
using System.IO;
using System.Diagnostics;

namespace AoC2022.solution
{
    public class AoCDay6
    {

        public AoCDay6(int selectedPart, string input)
        {
            List<string> charList = new List<string>();
            //input = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
            foreach (var character in input)
            {
                charList.Add(character.ToString());
            }
            int marker = 0;
            int markerB = 0;
            for (int i = 0; i <= charList.Count(); i++)
            {
                List<string> tempList = new List<string> { charList[i], charList[i+1], charList[i+2], charList[i+3] };
                bool isUnique = tempList.Distinct().Count() == tempList.Count();
                if(isUnique)
                {
                    marker = i+4;
                    break;
                }
            }

            for (int i = 0; i <= charList.Count(); i++)
            {
                List<string> tempList = new List<string> { charList[i], charList[i + 1], charList[i + 2], charList[i + 3], charList[i + 4], charList[i + 5], charList[i + 6], charList[i + 7], charList[i + 8], charList[i + 9], charList[i + 10], charList[i + 11], charList[i + 12], charList[i + 13] };
                bool isUnique = tempList.Distinct().Count() == tempList.Count();
                if (isUnique)
                {
                    markerB = i + 14;
                    break;
                }
            }




            output = "Part A: " + marker;
            output += "\nPart B: " + markerB;


        }

        public string output;
    }
}