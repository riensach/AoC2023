using System;
using System.IO;
using System.Diagnostics;
using System.Data.Common;

namespace AoC2022.solution
{
    public class AoCDay9
    {

        public AoCDay9(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int arrayLength = 1400;
            int arrayWidth = 1400;
            int startingX = arrayLength / 2;
            int startingY = arrayWidth / 2;

            string[,] grid = new string[arrayLength, arrayWidth];
            grid = createGrid(grid, arrayLength, arrayWidth);

            List<string> visitedLocation = new List<string>();
            visitedLocation.Add(startingX + "," + startingY);
            int headx = startingX;
            int heady = startingY;
            string[] ropes = new string[9];
            ropes[0] = startingX+","+startingY;
            ropes[1] = startingX+","+startingY;
            ropes[2] = startingX+","+startingY;
            ropes[3] = startingX+","+startingY;
            ropes[4] = startingX+","+startingY;
            ropes[5] = startingX+","+startingY;
            ropes[6] = startingX+","+startingY;
            ropes[7] = startingX+","+startingY;
            ropes[8] = startingX+","+startingY;
            //int tailx = startingX;
            //int taily = startingY;
            foreach (string line in lines)
            {
                string[] commands = line.Split(' ');
                string move = commands[0];
                int amount = Int32.Parse(commands[1]);
                for (int i = 0; i < amount; i++)
                {

                    grid[headx, heady] = ".";
                    //grid[tailx, taily] = "#";
                    if (move == "R")
                    {
                        heady = heady + 1;
                    }
                    else if (move == "L")
                    {
                        heady = heady - 1;
                    }
                    else if (move == "U")
                    {
                        headx = headx - 1;
                    }
                    else if (move == "D")
                    {
                        headx = headx + 1;
                    }

                    for (int y = 0; y < 9; y++)
                    {
                        string[] ropePosition = ropes[y].Split(',');
                        int currentTailX = Int32.Parse(ropePosition[0]);
                        int currentTailY = Int32.Parse(ropePosition[1]);
                        int currentHeadX = headx;
                        int currentHeadY = heady;
                        //grid[currentTailX, currentHeadY] = ".";
                        if (y > 0)
                        {
                            string[] headPosition = ropes[y - 1].Split(',');
                            currentHeadX = Int32.Parse(headPosition[0]);
                            currentHeadY = Int32.Parse(headPosition[1]);
                            grid[currentTailX, currentTailY] = ".";
                        }

                        if (currentTailX - currentHeadX > 0 && currentTailY - currentHeadY > 1) // Diagonal move
                        {
                            currentTailX--;
                            currentTailY--;
                        }
                        else if (currentTailX - currentHeadX > 1 && currentTailY - currentHeadY > 0) // Diagonal move
                        {
                            currentTailX--;
                            currentTailY--;
                        }
                        else if (currentTailX - currentHeadX > 0 && currentHeadY - currentTailY > 1) // Diagonal move
                        {
                            currentTailX--;
                            currentTailY++;
                        }
                        else if (currentTailX - currentHeadX > 1 && currentHeadY - currentTailY > 0) // Diagonal move
                        {
                            currentTailX--;
                            currentTailY++;
                        }
                        else if (currentHeadX - currentTailX > 0 && currentTailY - currentHeadY > 1) // Diagonal move
                        {
                            currentTailX++;
                            currentTailY--;
                        }
                        else if (currentHeadX - currentTailX > 1 && currentTailY - currentHeadY > 0) // Diagonal move
                        {
                            currentTailX++;
                            currentTailY--;
                        }
                        else if (currentHeadX - currentTailX > 1 && currentHeadY - currentTailY > 0) // Diagonal move
                        {
                            currentTailX++;
                            currentTailY++;
                        }
                        else if (currentHeadX - currentTailX > 0 && currentHeadY - currentTailY > 1) // Diagonal move
                        {
                            currentTailX++;
                            currentTailY++;
                        }
                        else if (currentHeadX - currentTailX > 1)
                        {
                            currentTailX++;
                        }
                        else if (currentTailX - currentHeadX > 1)
                        {
                            currentTailX--;
                        }
                        else if (currentHeadY - currentTailY > 1)
                        {
                            currentTailY++;
                        }
                        else if (currentTailY - currentHeadY > 1)
                        {
                            currentTailY--;
                        }
                        if (y == 8)
                        {
                            visitedLocation.Add(currentTailX + "," + currentTailY);
                        }
                        ropes[y] = currentTailX + "," + currentTailY;
                        grid[currentTailX, currentTailY] = y.ToString();
                    }
                    //visitedLocation.Add(tailx + "," + taily);
                    grid[headx, heady] = "H";
                    //grid[tailx, taily] = "T";
                    //output += printGrid(grid, arrayLength, arrayWidth);
                    //output += "Adding tail location: " + tailx + "," + taily + "\n";
                }

            }

            List<string> noDupes = visitedLocation.Distinct().ToList();
            int visitedLocations = noDupes.Count();

            output += "Part A: " + visitedLocations;
        }

        public string printGrid(string[,] grid,int xSize, int ySize)
        {
            string output = "\nGrid:\n";

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    string toWrite = grid[x,y];
                    //System.Console.WriteLine(toWrite);

                    output += ""+ toWrite;
                }
                //System.Console.WriteLine("\n");
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