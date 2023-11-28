using System;
using System.IO;
using System.Diagnostics;

namespace AoC2022.solution
{
    public class AoCDay3
    {

        public AoCDay3(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

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




        }

        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        public string output;
    }
}