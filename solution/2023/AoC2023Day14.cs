using System;
using System.IO;
using System.Diagnostics;
using System.Data.Common;

namespace AoC2023.solution
{
    public class sandUnit
    {
        public int xCoord = 0;
        public int yCoord = 500;
        public bool rested = false;
        public sandUnit() { }
    }
    public class AoCDay14
    {
        public string[,] grid;
        int arrayLength = 0;
        int arrayWidth = 0;
        public AoCDay14(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            arrayLength = lines.Count();
            arrayWidth = lines[0].Length;

            grid = new string[arrayLength, arrayWidth];
            grid = createGrid(grid, arrayLength, arrayWidth);
            int row = 0;
            int column = 0;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    grid[row, column] = character.ToString();
                    column++;
                }
                row++;
                column = 0;
            }
            bool stoppedMoving = false;
            while(stoppedMoving == false)
            {
                stoppedMoving = true;
                // NORTH
                for (int x = 1; x < arrayLength; x++)
                {
                    for (int y = 0; y < arrayWidth; y++)
                    {
                        if (grid[x,y] == "O" && grid[x - 1, y] == ".")
                        {
                            grid[x - 1, y] = "O";
                            grid[x, y] = ".";
                            stoppedMoving = false;
                        }
                    }
                }
                
            }
            int totalLoad = 0;
            for (int x = 0; x < arrayLength; x++)
            {
                for (int y = 0; y < arrayWidth; y++)
                {
                    if (grid[x, y] == "O")
                    {
                        totalLoad = totalLoad + arrayWidth - x;
                    }
                }
            }



            output = "Part A:"+ totalLoad;
            string outputGrid = printGrid(arrayLength, arrayWidth);
            output += outputGrid;


            grid = new string[arrayLength, arrayWidth];
            grid = createGrid(grid, arrayLength, arrayWidth);
            row = 0;
            column = 0;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    grid[row, column] = character.ToString();
                    column++;
                }
                row++;
                column = 0;
            }
            outputGrid = printGrid(arrayLength, arrayWidth);
            output += outputGrid;
            stoppedMoving = false;

            Dictionary<int, int> previousScores = new Dictionary<int, int>();




            int iterationToEnd = 1000000000;
            for (int i = 0; i < 1000000000; i++)
            {
                if(i % 10000000 == 0)
                {
                    Console.WriteLine("Completed " + i + " cycles.");
                }
                stoppedMoving = false;
                while (stoppedMoving == false)
                {
                    stoppedMoving = true;
                    // NORTH
                    for (int x = 1; x < arrayLength; x++)
                    {
                        for (int y = 0; y < arrayWidth; y++)
                        {
                            if (grid[x, y] == "O" && grid[x - 1, y] == ".")
                            {
                                grid[x - 1, y] = "O";
                                grid[x, y] = ".";
                                stoppedMoving = false;
                            }
                        }
                    }

                }
                stoppedMoving = false;
                while (stoppedMoving == false)
                {
                    stoppedMoving = true;
                    // WEST
                    for (int x = 0; x < arrayLength; x++)
                    {
                        for (int y = 1; y < arrayWidth; y++)
                        {
                            if (grid[x, y] == "O" && grid[x, y - 1] == ".")
                            {
                                grid[x, y - 1] = "O";
                                grid[x, y] = ".";
                                stoppedMoving = false;
                            }
                        }
                    }
                }
                stoppedMoving = false;
                while (stoppedMoving == false)
                {
                    stoppedMoving = true;
                    // SOUTH
                    for (int x = 0; x < arrayLength - 1; x++)
                    {
                        for (int y = 0; y < arrayWidth; y++)
                        {
                            if (grid[x, y] == "O" && grid[x + 1, y] == ".")
                            {
                                grid[x + 1, y] = "O";
                                grid[x, y] = ".";
                                stoppedMoving = false;
                            }
                        }
                    }
                }
                stoppedMoving = false;
                while (stoppedMoving == false)
                {
                    stoppedMoving = true;
                    // EAST
                    for (int x = 0; x < arrayLength; x++)
                    {
                        for (int y = 0; y < arrayWidth - 1; y++)
                        {
                            if (grid[x, y] == "O" && grid[x, y + 1] == ".")
                            {
                                grid[x, y + 1] = "O";
                                grid[x, y] = ".";
                                stoppedMoving = false;
                            }
                        }
                    }
                }

                totalLoad = 0;
                for (int x = 0; x < arrayLength; x++)
                {
                    for (int y = 0; y < arrayWidth; y++)
                    {
                        if (grid[x, y] == "O")
                        {
                            totalLoad = totalLoad + arrayWidth - x;
                        }
                    }
                }
                if(!previousScores.ContainsKey(totalLoad))
                {
                    previousScores.Add(totalLoad, i);
                } else
                {
                    int previousIndexValue = previousScores[totalLoad];
                    int iterationDiff = i - previousIndexValue;
                    iterationToEnd = previousIndexValue + ((1000000000 - previousIndexValue) % iterationDiff) + (i - iterationDiff);
                    //iterationToEnd = (1000000000 % i) + i;
                    Console.WriteLine("Repeat at iteratiion " + i + " with score " + totalLoad + " previously found at "+ previousIndexValue+" so break at " +iterationToEnd);
                    
                }
                if (i <10000) continue;
                if(i % 10000 == 1)
                // 112 repeating pattern, after 176
                // 190 = 204 = 218
                // 200 103867
                // 300 103878
                // 400 103856
                // 500 103857
                // 600 103863
                // 700 103860
                // 800 103845
                // 900 103867
                //1000 103878
                //1100 103856
                //2000 103863
                //3000 103867
                //4000 103857
                //5000 103845
                //6000 103856
                //7000 103860
                // 103861 correct answer
                //if(i == iterationToEnd)
                {
                    // We've got to the end of where we need to get to after out pattern
                    Console.WriteLine("Breaking at " + i);
                    break;
                }


            }
            totalLoad = 0;
            for (int x = 0; x < arrayLength; x++)
            {
                for (int y = 0; y < arrayWidth; y++)
                {
                    if (grid[x, y] == "O")
                    {
                        totalLoad = totalLoad + arrayWidth - x;
                    }
                }
            }
            // 64

            outputGrid = printGrid(arrayLength, arrayWidth);
            output += outputGrid;
            output += "Part B:" + totalLoad;

        }

        

        public string printGrid(int xSize, int ySize)
        {
            string output = "\nGrid:\n";

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    string toWrite = grid[x, y];
                    //System.Console.Write(toWrite);

                    output += "" + toWrite;
                }
                //System.Console.Write("\n");
                output += "\n";
            }

            return output;
        }

        public string[,] createGrid(string[,] grid, int xSize, int ySize)
        {

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    grid[x, y] = ".";
                }
            }

            return grid;
        }

        public string output;
    }
}