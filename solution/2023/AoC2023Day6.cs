using System;
using System.IO;
using System.Diagnostics;

namespace AoC2023.solution
{
    public class AoCDay6
    {

        public AoCDay6(int selectedPart, string input)
        {

            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            Dictionary<Int64, Int64> raceRecords = new Dictionary<Int64, Int64>();
            // Test
            //raceRecords.Add(7,9);
            //raceRecords.Add(15,40);
            //raceRecords.Add(30,200);

            // Input

            //raceRecords.Add(46,347);
            //raceRecords.Add(82,1522);
            //raceRecords.Add(84,1406);
            //raceRecords.Add(79,1471);

            //raceRecords.Add(71530, 940200);

            raceRecords.Add(46828479, 347152214061471);


            List<int> raceWinsList = new List<int>();
            foreach (KeyValuePair<Int64, Int64> raceRecord in raceRecords)
            {
                int raceWins = 0;     
                for (int i = 0; i <= raceRecord.Key; i++)
                {
                    int totalCharge = i;
                    Int64 totalDistance = i * (raceRecord.Key - i);

                    if (totalDistance > raceRecord.Value)
                    {
                        //Console.WriteLine("winner");
                        raceWins++;
                    }
                }
                Console.WriteLine("Race " + raceRecord.Key + " race wins: " + raceWins + "\n");
                raceWinsList.Add(raceWins);
            }

            //129470400 wrong
            Int64 finalCalculation = 1;
            foreach (int raceWins in raceWinsList)
            {
                finalCalculation = finalCalculation * raceWins;
            }
            output = "Part A: " + finalCalculation;


        }

        public string output;
    }
}