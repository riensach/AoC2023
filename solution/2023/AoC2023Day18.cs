using System;
using System.IO;
using System.Diagnostics;
using LoreSoft.MathExpressions;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using static AoC2023.solution.AoCDay10;
using System.Diagnostics.Metrics;
using System.Data.SqlTypes;
using System.Text;
using Newtonsoft.Json.Linq;

namespace AoC2023.solution
{
    public class AoCDay18
    {
        public List<string> cubes = new List<string>();
        public int startingX = 200;
        public int startingY = 100;
        public int arrayLength = 500;
        public int arrayWidth = 500;

        public string[,] grid;
        public string[,] gridTemp;
        

        public void test(string input)
        {
            //TEST
            startingX = 20;
            startingY = 10;
            arrayLength = 50;
            arrayWidth = 50;
            partA(input);
        }

        public void partA(string input)
        {
            string[] lines = input.Split("\n");       

            grid = new string[arrayLength, arrayWidth];
            grid = createGrid(grid, arrayLength, arrayWidth);

            Map maps = new Map(grid, arrayLength, arrayWidth);
            grid[startingX, startingY] = "#";

            int currentX = startingX;
            int currentY = startingY;
            int totalMovementSteps = 0;
            foreach (string line in lines)
            {
                line.Replace("\r", "").Replace("\n", "");
                // 
                string pattern = @"([U|D|R|L])[ ]([0-9]+)[ ][(]([#][a-zA-Z0-9]+)[)]";
                Regex rg = new Regex(pattern);
                string updatedString = new string(line);
                MatchCollection matchedWords = rg.Matches(updatedString);
                string movementDirection = matchedWords[0].Groups[1].Value;
                int movementAmount = int.Parse(matchedWords[0].Groups[2].Value);
                string rgbColor = matchedWords[0].Groups[3].Value;

                if (movementDirection == "D")
                {
                    for (int x = 0; x < movementAmount; x++)
                    {
                        grid[currentX + x, currentY] = "#";
                    }
                    currentX = currentX + movementAmount;
                }
                else if (movementDirection == "U")
                {
                    for (int x = 0; x < movementAmount; x++)
                    {
                        grid[currentX - x, currentY] = "#";
                    }
                    currentX = currentX - movementAmount;
                }
                else if (movementDirection == "L")
                {
                    for (int x = 0; x < movementAmount; x++)
                    {
                        grid[currentX, currentY - x] = "#";
                    }
                    currentY = currentY - movementAmount;
                }
                else if (movementDirection == "R")
                {
                    for (int x = 0; x < movementAmount; x++)
                    {
                        grid[currentX, currentY + x] = "#";
                    }
                    currentY = currentY + movementAmount;
                }
                totalMovementSteps = totalMovementSteps + movementAmount;
            }

            long countSquares = 0;
            var startingLocation = new Position(startingX + 1, startingY + 1);
            Console.WriteLine(startingLocation);
            HashSet<Position> visitedLocations = new HashSet<Position>();
            var explorerInfo = new Explorer(startingLocation, visitedLocations);
            countSquares = findThePath(maps, explorerInfo);
            long finalCalt = (totalMovementSteps) + (countSquares);

            output += "\nPart A: " + finalCalt;
        }

        public void partB(string input)
        {
            string[] lines = input.Split("\n");

            startingX = 20000;
            startingY = 10000;
            arrayLength = 50000;
            arrayWidth = 50000;

            grid = new string[arrayLength, arrayWidth];
            grid = createGrid(grid, arrayLength, arrayWidth);

            Map maps = new Map(grid, arrayLength, arrayWidth);
            grid[startingX, startingY] = "#";

            int currentX = startingX;
            int currentY = startingY;
            int totalMovementSteps = 0;
            foreach (string line in lines)
            {
                line.Replace("\r", "").Replace("\n", "");
                // 
                string pattern = @"([U|D|R|L])[ ]([0-9]+)[ ][(][#]([a-zA-Z0-9]+)[)]";
                Regex rg = new Regex(pattern);
                string updatedString = new string(line);
                MatchCollection matchedWords = rg.Matches(updatedString);
                string rgbColor = matchedWords[0].Groups[3].Value;
                char[] parts = rgbColor.ToCharArray();
                string movementCode = rgbColor.Substring(5, 1);
                string movementAmountString = rgbColor.Substring(0, 5);
                string movementDirection = "";
                if (int.Parse(movementCode) == 0)
                {
                    movementDirection = "R";
                } else if (int.Parse(movementCode) == 1)
                {
                    movementDirection = "D";
                }
                else if (int.Parse(movementCode) == 2)
                {
                    movementDirection = "L";
                }
                else if (int.Parse(movementCode) == 3)
                {
                    movementDirection = "U";
                }

                int movementAmount = Convert.ToInt32(movementAmountString, 16);

                Console.WriteLine(movementDirection + " - " + movementAmount);
                continue;


                if (movementDirection == "D")
                {
                    for (int x = 0; x < movementAmount; x++)
                    {
                        grid[currentX + x, currentY] = "#";
                    }
                    currentX = currentX + movementAmount;
                }
                else if (movementDirection == "U")
                {
                    for (int x = 0; x < movementAmount; x++)
                    {
                        grid[currentX - x, currentY] = "#";
                    }
                    currentX = currentX - movementAmount;
                }
                else if (movementDirection == "L")
                {
                    for (int x = 0; x < movementAmount; x++)
                    {
                        grid[currentX, currentY - x] = "#";
                    }
                    currentY = currentY - movementAmount;
                }
                else if (movementDirection == "R")
                {
                    for (int x = 0; x < movementAmount; x++)
                    {
                        grid[currentX, currentY + x] = "#";
                    }
                    currentY = currentY + movementAmount;
                }
                totalMovementSteps = totalMovementSteps + movementAmount;
            }


            long countSquares = 0;
            var startingLocation = new Position(startingX + 1, startingY + 1);
            Console.WriteLine(startingLocation);
            HashSet<Position> visitedLocations = new HashSet<Position>();
            var explorerInfo = new Explorer(startingLocation, visitedLocations);
            countSquares = findThePath(maps, explorerInfo);
            long finalCalt = (totalMovementSteps) + (countSquares);

            output += "\nPart B: " + finalCalt;
        }

        public AoCDay18(int selectedPart, string input)
        {
            test(input);
            partA(input);
            partB(input);
           

        }
        public class Map
        {
            public string[,] mapLines;
            public readonly int width;
            public readonly int length;
            public Map(string[,] input, int lengthInput, int widthInput)
            {
                mapLines = input;
                length = lengthInput;
                width = widthInput;
            }

            public string getGridState(Position position)
            {
                //if (position.x < 0 || position.y < 0 || position.x >= mapLines.Length || position.y >= mapLines[0].Length) return '.';
                return mapLines[position.x,position.y];

            }
        }
        public long findThePath(Map map, Explorer explorerInformation)
        {
            long totalFoundSquares = 0;
            var queue = new PriorityQueue<Explorer, int>();

            queue.Enqueue(explorerInformation, 1);
            HashSet<Explorer> previousExplorers = new HashSet<Explorer>();

            while (queue.Count > 0)
            {
                //Console.WriteLine("got here");
                var explorer = queue.Dequeue();

                //Console.WriteLine("got here2");
                foreach (Explorer explorerOption in movementOptions(explorer, map))
                {
                    explorerOption.visitedLocations.Add(explorerOption.position);
                    //Console.WriteLine("got here3");
                    if (!previousExplorers.Contains(explorerOption))
                    {
                        previousExplorers.Add(explorerOption);
                        queue.Enqueue(explorerOption, 1);
                        totalFoundSquares++;
                    }
                }
            }
            return totalFoundSquares;
        }


        IEnumerable<Explorer> movementOptions(Explorer explorer, Map map)
        {
            // Check South
            Position southOption = new Position(explorer.position.x + 1, explorer.position.y);
            if (map.getGridState(southOption) == "." && !explorer.visitedLocations.Contains(southOption))
            {
                yield return explorer with
                {
                    position = southOption,
                };
            }

            // Check North
            Position northOption = new Position(explorer.position.x - 1, explorer.position.y);
            if (map.getGridState(northOption) == "." && !explorer.visitedLocations.Contains(northOption))
            {
                yield return explorer with
                {
                    position = northOption,
                };
            }

            // Check East
            Position eastOption = new Position(explorer.position.x, explorer.position.y + 1);
            if (map.getGridState(eastOption) == "." && !explorer.visitedLocations.Contains(eastOption))
            {
                yield return explorer with
                {
                    position = eastOption,
                };
            }

            // Check West
            Position westOption = new Position(explorer.position.x, explorer.position.y - 1);
            if (map.getGridState(westOption) == "." && !explorer.visitedLocations.Contains(westOption))
            {
                yield return explorer with
                {
                    position = westOption,
                };
            }

        }

        public record Explorer(Position position, HashSet<Position> visitedLocations);

        public record Position(int x, int y);

        public string printGrid(int xSize, int ySize)
        {
            string output = "\nGrid:\n";

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    string toWrite = grid[x, y];
                    System.Console.Write(toWrite);

                    //output += "" + toWrite;
                }
                System.Console.Write("\n");
                //output += "\n";
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