using System;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace AoC2022.solution
{
    public class AoCDay5
    {

        public AoCDay5(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            List<string>[] arrayList = new List<string>[9];
            arrayList[0] = new List<string> { "R", "G", "J", "B", "T", "V", "Z" };
            arrayList[1] = new List<string> { "J", "R", "V", "L" };
            arrayList[2] = new List<string> { "S", "Q", "F" };
            arrayList[3] = new List<string> { "Z", "H", "N", "L", "F", "V", "Q", "G" };
            arrayList[4] = new List<string> { "R", "Q", "T", "J", "C", "S", "M", "W" };
            arrayList[5] = new List<string> { "S", "W", "T", "C", "H", "F" };
            arrayList[6] = new List<string> { "D", "Z", "C", "V", "F", "N", "J" };
            arrayList[7] = new List<string> { "L", "G", "Z", "D", "W", "R", "F", "Q" };
            arrayList[8] = new List<string> { "J", "B", "W", "V", "P" };



            foreach (string line in lines)
            {
                string[] moves = line.Split(' ');
                int moveAmount = Int32.Parse(moves[1]);
                int moveFrom = Int32.Parse(moves[3]);
                int moveTo = Int32.Parse(moves[5]);

                for (int i = 0; i < moveAmount; i++)
                {
                    string itemToMove = arrayList[moveFrom - 1].Last();
                    arrayList[moveTo - 1].Add(itemToMove);
                    arrayList[moveFrom - 1].RemoveAt(arrayList[moveFrom - 1].Count - 1);
                }

            }

            string endValue = arrayList[0].Last();
            endValue += arrayList[1].Last();
            endValue += arrayList[2].Last();
            endValue += arrayList[3].Last();
            endValue += arrayList[4].Last();
            endValue += arrayList[5].Last();
            endValue += arrayList[6].Last();
            endValue += arrayList[7].Last();
            endValue += arrayList[8].Last();
            output = "Part A: " +endValue;

            List<string>[] arrayListB = new List<string>[9];
            arrayListB[0] = new List<string> { "R", "G", "J", "B", "T", "V", "Z" };
            arrayListB[1] = new List<string> { "J", "R", "V", "L" };
            arrayListB[2] = new List<string> { "S", "Q", "F" };
            arrayListB[3] = new List<string> { "Z", "H", "N", "L", "F", "V", "Q", "G" };
            arrayListB[4] = new List<string> { "R", "Q", "T", "J", "C", "S", "M", "W" };
            arrayListB[5] = new List<string> { "S", "W", "T", "C", "H", "F" };
            arrayListB[6] = new List<string> { "D", "Z", "C", "V", "F", "N", "J" };
            arrayListB[7] = new List<string> { "L", "G", "Z", "D", "W", "R", "F", "Q" };
            arrayListB[8] = new List<string> { "J", "B", "W", "V", "P" };

            foreach (string line in lines)
            {
                string[] moves = line.Split(' ');
                int moveAmount = Int32.Parse(moves[1]);
                int moveFrom = Int32.Parse(moves[3]);
                int moveTo = Int32.Parse(moves[5]);

                for (int i = 0; i < moveAmount; i++)
                {
                    string itemToMove = arrayListB[moveFrom - 1].Last();
                    if(i > 0)
                    {
                        arrayListB[moveTo - 1].Insert(arrayListB[moveTo - 1].Count - i, itemToMove);
                    } else
                    {
                        arrayListB[moveTo - 1].Add(itemToMove);
                    }                    
                    arrayListB[moveFrom - 1].RemoveAt(arrayListB[moveFrom - 1].Count - 1);
                }


            }

            string endValueB = arrayListB[0].Last();
            endValueB += arrayListB[1].Last();
            endValueB += arrayListB[2].Last();
            endValueB += arrayListB[3].Last();
            endValueB += arrayListB[4].Last();
            endValueB += arrayListB[5].Last();
            endValueB += arrayListB[6].Last();
            endValueB += arrayListB[7].Last();
            endValueB += arrayListB[8].Last();
            output += "\nPart B: " + endValueB;

        }

        public string output;
    }
}