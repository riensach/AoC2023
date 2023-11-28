using System;
using System.IO;
using System.Diagnostics;
using System.Data.Common;
using System.Collections.Generic;

namespace AoC2022.solution
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

            arrayLength = lines.Count();
            arrayWidth = lines[0].Length;

            grid = new int[arrayLength, arrayWidth];
            grid = createGrid(grid, arrayLength, arrayWidth);
            // Starting square = S
            // Goal square = E

            int row = 0;
            int column = 0;
            int startingLocationX = 0;
            int startingLocationY = 0;

            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    int index = char.ToUpper(character) - 64;
                    grid[row, column] = index;
                    if (character.ToString() == "S")
                    {
                        startingLocationX = row;
                        startingLocationY = column;
                        grid[row, column] = 1;
                        Console.WriteLine("Starting Location: " + startingLocationX + "," + startingLocationY);
                    }
                    if (character.ToString() == "E")
                    {
                        targetLocationX = row;
                        targetLocationY = column;
                        grid[row, column] = 26;
                        Console.WriteLine("Target Location: " + targetLocationX + "," + targetLocationY);
                    }
                    column++;                    
                }
                row++;
                column = 0;
            }

            

            int lowestSteps = 0;
            int stepCount = 0;


            for (int x = 0; x < arrayLength; x++)
            {
                for (int y = 0; y < arrayWidth; y++)
                {
                    if (grid[x,y] == 1) {
                        findLocation(x, y, stepCount);
                    }
                }

            }

            

            pathSteps.Sort();

            lowestSteps = pathSteps.First();



            output = printGrid(grid, arrayLength, arrayWidth);

            output += "Part A: " + lowestSteps;
        }

        public void findLocation(int currentLocationX, int currentLocationY, int stepCount)
        {

            //Stack<BinaryNode> stack = new Stack<BinaryNode>();
            Queue<string> queue = new Queue<string>();
            string queueString = currentLocationX + "," + currentLocationY + "," + stepCount;
            queue.Enqueue(queueString);
            do
            {
                string currentItem = queue.Dequeue();
                string[] commands = currentItem.Split(',');
                int locationX = Int32.Parse(commands[0]);
                int locationY = Int32.Parse(commands[1]);
                int stepCountNew = Int32.Parse(commands[2]);

                //Console.WriteLine(locationX + " - " + locationY + " - " + stepCountNew);

                if (locationX == targetLocationX && locationY == targetLocationY)
                {
                    Console.WriteLine("Made it to the end with " + stepCountNew + " steps taken!\n");
                    pathSteps.Add(stepCountNew);
                    continue;
                }

                if (gridValues.ContainsKey(locationX + "," + locationY))
                {
                    if (gridValues[locationX + "," + locationY] <= stepCountNew)
                    {
                        continue; // I've made it here for less steps before
                    }
                    else
                    {
                        gridValues[locationX + "," + locationY] = stepCountNew;
                    }
                }
                else
                {
                    gridValues[locationX + "," + locationY] = stepCountNew;
                }
                int currentValue = grid[locationX, locationY];
                int optionAbove = (locationX - 1 < 0) ? 100 : grid[locationX - 1, locationY];
                int optionBelow = (locationX + 1 >= arrayLength) ? 100 : grid[locationX + 1, locationY];
                int optionLeft = (locationY - 1 < 0) ? 100 : grid[locationX, locationY - 1];
                int optionRight = (locationY + 1 >= arrayWidth) ? 100 : grid[locationX, locationY + 1];

                int tempChange;
                tempChange = locationX - 1;
                string optionAboveString = tempChange + "," + locationY;
                tempChange = locationX + 1;
                string optionBelowString = tempChange + "," + locationY;
                tempChange = locationY - 1;
                string optionLeftString = locationX + "," + tempChange;
                tempChange = locationY + 1;
                string optionRightString = locationX + "," + tempChange;
                if (optionAbove <= currentValue + 1)
                {
                    //Console.WriteLine("Option above, moving from " + locationX + "," + locationY + " with " + stepCountNew + " total steps so far.\n");

                    int newX = locationX - 1;
                    int newY = locationY;
                    int newSteps = stepCountNew + 1;
                    string newQueueString = newX + "," + newY + "," + newSteps;
                    queue.Enqueue(newQueueString);
                    //findLocation(locationX - 1, locationY, stepCountNew + 1);
                }
                if (optionBelow <= currentValue + 1)
                {
                    //Console.WriteLine("Option below, moving from " + locationX + "," + locationY + " with " + stepCountNew + " total steps so far.\n");
                    int newX = locationX + 1;
                    int newY = locationY;
                    int newSteps = stepCountNew + 1;
                    string newQueueString = newX + "," + newY + "," + newSteps;
                    queue.Enqueue(newQueueString);
                    //findLocation(locationX + 1, locationY, stepCountNew + 1);
                }
                if (optionLeft <= currentValue + 1)
                {
                    //Console.WriteLine("Option left, moving from " + locationX + "," + locationY + " with " + stepCountNew + " total steps so far.\n");
                    int newX = locationX;
                    int newY = locationY - 1;
                    int newSteps = stepCountNew + 1;
                    string newQueueString = newX + "," + newY + "," + newSteps;
                    queue.Enqueue(newQueueString);
                    //findLocation(locationX, locationY - 1, stepCountNew + 1);
                }
                if (optionRight <= currentValue + 1)
                {
                    //Console.WriteLine("Option right, moving from " + locationX + "," + locationY + " with " + stepCountNew + " total steps so far.\n");
                    int newX = locationX;
                    int newY = locationY + 1;
                    int newSteps = stepCountNew + 1;
                    string newQueueString = newX + "," + newY + "," + newSteps;
                    queue.Enqueue(newQueueString);
                    //findLocation(locationX, locationY + 1, stepCountNew + 1);
                }
            } while (queue.Count > 0);
            return;
            
        }

        public string printGrid(int[,] grid, int xSize, int ySize)
        {
            string output = "\nGrid:\n";

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    string toWrite = grid[x, y].ToString();
                    //System.Console.WriteLine(toWrite);

                    output += " " + toWrite;
                }
                //System.Console.WriteLine("\n");
                output += "\n";
            }

            return output;
        }

        public int[,] createGrid(int[,] grid, int xSize, int ySize)
        {

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    grid[x, y] = 0;
                }
            }

            return grid;
        }

        public string output;
    }
}