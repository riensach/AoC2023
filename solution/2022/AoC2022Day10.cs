using System;
using System.IO;
using System.Diagnostics;

namespace AoC2022.solution
{
    public class AoCDay10
    {
        private IDictionary<int, int> cycleValues = new Dictionary<int, int>();
        private int xReg = 1;
        private string[,] grid = new string[6, 40];
        List<string> spriteList = new List<string>();
        public AoCDay10(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            int signalStrengthSum = 0;
            int cycles = 0;
            
            cycleValues.Add(20, 0);
            cycleValues.Add(60, 0);
            cycleValues.Add(100, 0);
            cycleValues.Add(140, 0);
            cycleValues.Add(180, 0);
            cycleValues.Add(220, 0);

            createGrid(240);
            foreach (string line in lines)
            {
                string[] commands = line.Split(' ');
                string command = commands[0];

                checkCycle(cycles);
                if (command == "noop")
                {
                    cycles++;
                    checkCycle(cycles);
                    continue;
                } else if(command == "addx")
                {

                    int amount = Int32.Parse(commands[1]);
                    cycles++;
                    checkCycle(cycles);
                    cycles++;
                    xReg = xReg + amount;
                    checkCycle(cycles);
                }

            }
            foreach (var item in cycleValues)
            {
                signalStrengthSum += item.Value;
            }

            output = "Part A: " + signalStrengthSum;

            output += printGrid(40,240);



            


        }

        public void checkCycle(int cycles)
        {
            if (cycleValues.ContainsKey(cycles))
            {
                cycleValues[cycles] = xReg * cycles;
            }
            int tempCycleValue = cycles;
            if (cycles >= 200)
            {
                tempCycleValue = cycles - 200;
            } else if (cycles >= 160)
            {
                tempCycleValue = cycles - 160;
            } else if (cycles >= 120)
            {
                tempCycleValue = cycles - 120;
            }
            else if (cycles >= 80)
            {
                tempCycleValue = cycles - 80;
            }
            else if (cycles >= 40)
            {
                tempCycleValue = cycles - 40;
            }

            if(xReg == tempCycleValue || xReg == tempCycleValue - 1 || xReg == tempCycleValue + 1)
            {
                Console.WriteLine("Sprite drawn at cycle " + cycles + " with X value of " + xReg+"\n");
                spriteList[cycles] = "#";
            }
        }


        public string printGrid(int breakRow, int size)
        {
            string output = "\nGrid:\n";

            for (int x = 0; x < size; x++)
            {

                if (x % breakRow == 0)
                {
                    output += "\n";
                }
                output += "" + spriteList[x];

                //System.Console.WriteLine("\n");
                
            }

            return output;
        }

        public void createGrid(int size)
        {

            for (int x = 0; x < size; x++)
            {
                spriteList.Add(" ");
            }

        }

        public string output;

    }
}