using System;
using System.IO;
using System.Diagnostics;

namespace AoC2022.solution
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
        public AoCDay14(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int arrayLength = 1400;
            int arrayWidth = 1400;
            int lowestMountain = 0;
            grid = new string[arrayLength, arrayWidth];
            createGrid(arrayLength, arrayWidth);


            foreach (string line in lines)
            {
                string[] moves = line.Split(" -> ");
                int startX = 0;
                int startY = 0;
                foreach (string move in moves)
                {
                    string[] coords = move.Split(",");

                    if (startX == 0)
                    {
                        startX = Int32.Parse(coords[1]);
                        startY = Int32.Parse(coords[0]);
                    } else
                    {

                        if(startX > Int32.Parse(coords[1]))
                        {
                            for (int x = Int32.Parse(coords[1]); x <= startX; x++)
                            {
                                grid[x, startY] = "#";
                            }

                        } else if (startX < Int32.Parse(coords[1]))
                        {
                            for (int x = startX; x <= Int32.Parse(coords[1]); x++)
                            {
                                grid[x, startY] = "#";
                            }
                        }
                        else if (startY > Int32.Parse(coords[0]))
                        {
                            for (int y = Int32.Parse(coords[0]); y <= startY; y++)
                            {
                                grid[startX, y] = "#";
                            }
                        } else
                        {
                            for (int y = startY; y <= Int32.Parse(coords[0]); y++)
                            {
                                grid[startX, y] = "#";
                            }
                        }
                                              
                        if (lowestMountain < Int32.Parse(coords[1]))
                        {
                            lowestMountain = Int32.Parse(coords[1]);
                        }
                        startX = Int32.Parse(coords[1]);
                        startY = Int32.Parse(coords[0]);
                    }
                    
                }
            }
            lowestMountain = lowestMountain + 1;
            Console.WriteLine(lowestMountain);

            bool voided = false;
            int totalSandUnits = 0;
            while (voided == false)
            {
                int sandX = 0;
                int sandY = 500;
                bool settled = false;
                while (settled == false)
                {
                    if (sandX == lowestMountain)
                    {
                        //voided = true;
                        //break;
                        grid[sandX, sandY] = "o";
                        totalSandUnits++;
                        settled = true;
                    }
                    if(grid[0, 500] == "o")
                    {
                        voided = true;
                        break;
                    }
                    if (grid[sandX + 1, sandY] == ".")
                    {
                        sandX++;
                        continue;
                    } else if (grid[sandX + 1, sandY - 1] == ".")
                    {
                        sandX++;
                        sandY--;
                        continue;
                    }
                    else if (grid[sandX + 1, sandY + 1] == ".")
                    {
                        sandX++;
                        sandY++;
                        continue;
                    } else
                    {
                        grid[sandX, sandY] = "o";
                        totalSandUnits++;
                        settled = true;
                    }
                    
                }
                    
            }



            output = "Part A:"+totalSandUnits;

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

        public void createGrid(int xSize, int ySize)
        {

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    grid[x, y] = ".";
                }
            }

        }

        public string output;
    }
}