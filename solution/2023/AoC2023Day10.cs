﻿using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using Windows.UI.StartScreen;
using System.Collections.Generic;

namespace AoC2023.solution
{
    public class AoCDay10
    {
        public HashSet<int> pathSteps = new HashSet<int>();
        public IDictionary<string, int> gridValues = new Dictionary<string, int>();
        public string[,] grid;
        public Dictionary<Position, int> shortestPathOptions = new Dictionary<Position, int>();
        int arrayLength = 0;
        int arrayWidth = 0;
        public AoCDay10(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            arrayLength = lines.Count();
            arrayWidth = lines[0].Length;

            grid = new string[arrayLength, arrayWidth];
            grid = createGrid(grid, arrayLength, arrayWidth);
            
            HashSet<Position> possiblePositions = new HashSet<Position>();
            HashSet<Position> allPositions = new HashSet<Position>();
            HashSet<Position> possibleCages = new HashSet<Position>();
            Dictionary<string, int> locationSteps = new Dictionary<string,int>();
            

            int row = 0;
            int column = 0;
            int startingLocationX = 0;
            int startingLocationY = 0;
            int possCages = 0;

            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    grid[row, column] = character.ToString();
                    allPositions.Add(new Position(row, column));
                    if (character == 'S')
                    {
                        startingLocationX = row;
                        startingLocationY = column;
                        locationSteps[row + "," + column] = 999999999;
                    } else if (character != '.')
                    {
                        possiblePositions.Add(new Position(row, column));
                        locationSteps[row+","+column] = 999999999;
                    }
                    else if (character == '.')
                    {
                        //possCages++;
                        possibleCages.Add(new Position(row, column));
                        locationSteps[row + "," + column] = 999999999;
                    }
                    column++;
                }
                row++;
                column = 0;
            }
            //Console.WriteLine("Possible cages: " + possCages);
            //string theGrid = printGrid(grid, arrayLength, arrayWidth);
            //Console.WriteLine(theGrid);
            //output += "Part B: " + possCages;

            











            //return;

            var startingLocation = new Position(startingLocationX, startingLocationY);
            Console.WriteLine(startingLocation.ToString());
            Map maps = new Map(lines, arrayLength, arrayWidth);
            HashSet<Position> pathtoTrack = new HashSet<Position>();
            int shortestPath = 0;
            foreach (Position possiblePosition in possiblePositions)
            {
                HashSet<Position> visitedLocations = new HashSet<Position>(); 
                var explorerInfo = new Explorer(0, startingLocation, visitedLocations);
                var shortestPathExplorer = findThePath(maps, explorerInfo, possiblePosition, locationSteps);
                Console.WriteLine("Looking for position " + possiblePosition.x + "," + possiblePosition.y);
                if(shortestPathExplorer.steps > 0 && shortestPathExplorer.steps > shortestPath)
                {
                    //shortestPathOptions.Add(possiblePosition, shortestPathExplorer.steps);
                    shortestPath = shortestPathExplorer.steps;
                    pathtoTrack.Clear();
                    pathtoTrack = shortestPathExplorer.visitedLocations;
                }
                

            }
            pathtoTrack.Add(new Position(startingLocationX, startingLocationY));

            //int highestSteps = shortestPathOptions.Values.Max();

            int enclosedCount = 0;

            output += "Part A: " + shortestPath;

            HashSet<Position> possibleCagesList = new HashSet<Position>(allPositions.Except(pathtoTrack));
            //possibleCagesList.ForEach(i => Console.Write("{0}\t", i));

            foreach (Position possCage in possibleCagesList)
            {
                int northCount = 0;
                int southCount = 0;
                int eastCount = 0;
                int westCount = 0;
                for (int i = 1; i+ possCage.x < arrayLength; i++)
                {
                    Position southOption = new Position(possCage.x + i, possCage.y);
                    if (pathtoTrack.Contains(southOption))
                    {
                        southCount++;
                    }
                }
                for (int i = 1; possCage.x - i >= 0; i++)
                {
                    Position northOption = new Position(possCage.x - i, possCage.y);
                    if (pathtoTrack.Contains(northOption))
                    {
                        northCount++;
                    }
                }
                for (int i = 1; i + possCage.y < arrayWidth; i++)
                {
                    Position eastOption = new Position(possCage.x, possCage.y + i);
                    if (pathtoTrack.Contains(eastOption))
                    {
                        eastCount++;
                    }
                }
                for (int i = 1; possCage.y - i >= 0; i++)
                {
                    Position westOption = new Position(possCage.x, possCage.y - i);
                    if(pathtoTrack.Contains(westOption))
                    {
                        westCount++;
                    }
                }

                Console.WriteLine("Location "+possCage.x+","+ possCage.y+" crosses the path the following times. East:"+ eastCount+", West:" + westCount + ", North:" + northCount + ", South:"+ southCount);
                if (westCount % 2 == 1)
                //if (eastCount + westCount + southCount + northCount % 2 == 1)
                //if ((eastCount % 2 != 0) && (westCount % 2 != 0) && (southCount % 2 != 0) && (northCount % 2 != 0))
                {
                    Console.WriteLine("Location " + possCage.x + "," + possCage.y + " is containted!");
                    enclosedCount++;
                }
                

            }


            output += "\nPart B: " + enclosedCount;

            string theGrid = printGrid(grid, arrayLength, arrayWidth, pathtoTrack);
            Console.WriteLine(theGrid);


            // Part B is close, but needs to consider all tiles not in the grid













        }

        public class Map
        {
            public string[] mapLines;
            public readonly int width;
            public readonly int length;
            public Map(string[] input, int lengthInput, int widthInput)
            {
                mapLines = input;
                length = lengthInput;
                width = widthInput;
            }

            public char getGridState(Position position)
            {
                if (position.x < 0 || position.y < 0 || position.x >= mapLines.Length || position.y >= mapLines[0].Length) return '.';
                return mapLines[position.x][position.y];                

            }
        }
        public record Explorer(int steps, Position position, HashSet<Position> visitedLocations);
        public record LocationSteps(int steps, Position position);
        public record Position(int x, int y);
        public Explorer findThePath(Map map, Explorer explorerInformation, Position targetPosition, Dictionary<string, int> locationSteps)
        {            
            var queue = new PriorityQueue<Explorer, int>();
            int f(Explorer explorer)
            {
                // estimate the remaining step count with Manhattan distance
                var dist =
                    Math.Abs(targetPosition.x - explorer.position.x) +
                    Math.Abs(targetPosition.y - explorer.position.y);
                //Console.WriteLine("Retunring info = "+explorer.steps + "-"+dist);
                return explorer.steps + dist;
            }

            queue.Enqueue(explorerInformation, f(explorerInformation));
            HashSet<Explorer> previousExplorers = new HashSet<Explorer>();

            while (queue.Count > 0)
            {
                //Console.WriteLine("got here");
                var explorer = queue.Dequeue();
                if (locationSteps[explorer.position.x+","+ explorer.position.y] < explorer.steps)
                {
                    //Console.WriteLine("got here4");
                    return explorerInformation with
                    {
                        steps = -1,
                        position = explorerInformation.position
                    };
                }
                locationSteps[explorer.position.x + "," + explorer.position.y] = explorer.steps;
                if (explorer.position == targetPosition)
                {
                    //Console.WriteLine("got here4");
                    return explorer;
                }
                //Console.WriteLine("got here2");
                foreach (Explorer explorerOption in movementOptions(explorer, map))
                {
                    explorerOption.visitedLocations.Add(explorerOption.position);
                    //Console.WriteLine("got here3");
                    if (!previousExplorers.Contains(explorerOption))
                    {
                        //Console.WriteLine("got here4");
                        //if (explorer.steps > 1000) break;
                        //explorerOption.visitedLocations.Add(explorerOption.position);
                        //Console.WriteLine("Adding previous location "+ explorerOption.position.x+","+ explorerOption.position.y);
                        previousExplorers.Add(explorerOption);
                        queue.Enqueue(explorerOption, f(explorerOption));
                        //Console.WriteLine("Adding explorer with steps: "+ explorer.steps);



                    } else
                    {
                        //Console.WriteLine("Been here before! " + explorerOption.position + " - " + explorerOption.steps + " - " + explorerOption.visitedLocations);
                    }
                }
            }
            // 58 is wrong
            // 57 is wrong
            // 56 is wrong
            // 55 is wrong
            // 54 is wrong
            //Console.WriteLine("Returning");
            return explorerInformation with
            {
                steps = -1,
                position = explorerInformation.position
            };
            //throw new Exception("No path available");
        }

        IEnumerable<Explorer> movementOptions(Explorer explorer, Map map)
        {
            // Check South
            Position southOption = new Position(explorer.position.x + 1, explorer.position.y);
            if (map.getGridState(southOption) != 'S' && map.getGridState(southOption) != '.' 
                && (map.getGridState(explorer.position) == 'S' || map.getGridState(explorer.position) == '|' 
                || map.getGridState(explorer.position) == 'F' || map.getGridState(explorer.position) == '7')
                && !explorer.visitedLocations.Contains(southOption)
                )
            {
                /*Console.WriteLine("hello");
                Console.WriteLine("coord: " + southOption.x + " :: " + southOption.y);
                foreach (var valueHistory in explorer.visitedLocations)
                {
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(valueHistory))
                    {
                        string name = descriptor.Name;
                        object value = descriptor.GetValue(valueHistory);
                        Console.WriteLine("{0}={1}", name, value);
                    }
                }*/
                //explorer.visitedLocations.ForEach(i => Console.Write("{0}\t", i));


                if (map.getGridState(southOption) == 'L' || map.getGridState(southOption) == 'J' || map.getGridState(southOption) == '|')
                {
                    //HashSet<Position> tempList = new HashSet<Position>(explorer.visitedLocations);
                    //tempList.Add(southOption);
                    yield return explorer with
                    {
                        steps = explorer.steps + 1,
                        position = southOption,
                        //visitedLocations = tempList
                    };
                }
                
            }

            // Check North
            Position northOption = new Position(explorer.position.x - 1, explorer.position.y);
            if (map.getGridState(northOption) != 'S' && map.getGridState(northOption) != '.'
                && (map.getGridState(explorer.position) == 'S' || map.getGridState(explorer.position) == '|'
                || map.getGridState(explorer.position) == 'J' || map.getGridState(explorer.position) == 'L')
                && !explorer.visitedLocations.Contains(northOption)
                )
            {
                //Console.WriteLine("hello");
                //Console.WriteLine("north option :: " + explorer.position.x +","+ explorer.position.y + " :: " + northOption.x +", "+ northOption.y);
                if (map.getGridState(northOption) == 'F' || map.getGridState(northOption) == '7' || map.getGridState(northOption) == '|')
                {
                    //HashSet<Position> tempList = new HashSet<Position>(explorer.visitedLocations);
                    //tempList.Add(northOption);
                    //HashSet<Position> tempList = new HashSet<Position>();
                    //tempList.Add(northOption);
                    //tempList.UnionWith(explorer.visitedLocations);
                    yield return explorer with
                    {
                        steps = explorer.steps + 1,
                        position = northOption,
                        //visitedLocations = tempList
                    };
                }

            }

            // Check East
            Position eastOption = new Position(explorer.position.x, explorer.position.y + 1);
            if (map.getGridState(eastOption) != 'S' && map.getGridState(eastOption) != '.'
                && (map.getGridState(explorer.position) == 'S' || map.getGridState(explorer.position) == '-'
                || map.getGridState(explorer.position) == 'L' || map.getGridState(explorer.position) == 'F')
                && !explorer.visitedLocations.Contains(eastOption)
                )
            {
                //Console.WriteLine("hello");
                //Console.WriteLine("east option");
                if (map.getGridState(eastOption) == '7' || map.getGridState(eastOption) == 'J' || map.getGridState(eastOption) == '-')
                {
                    //HashSet<Position> tempList = new HashSet<Position>(explorer.visitedLocations);
                    //tempList.Add(eastOption);
                    yield return explorer with
                    {
                        steps = explorer.steps + 1,
                        position = eastOption,
                        //visitedLocations = tempList
                    };
                }

            }

            // Check West
            Position westOption = new Position(explorer.position.x, explorer.position.y - 1);
            if (map.getGridState(westOption) != 'S' && map.getGridState(westOption) != '.'
                && (map.getGridState(explorer.position) == 'S' || map.getGridState(explorer.position) == '-'
                || map.getGridState(explorer.position) == 'J' || map.getGridState(explorer.position) == '7')
                && !explorer.visitedLocations.Contains(westOption)
                )
            {
                //Console.WriteLine("hello");
                if (map.getGridState(westOption) == 'L' || map.getGridState(westOption) == 'F' || map.getGridState(westOption) == '-')
                {
                    //HashSet<Position> tempList = new HashSet<Position>(explorer.visitedLocations);
                    //tempList.Add(westOption);
                    yield return explorer with
                    {
                        steps = explorer.steps + 1,
                        position = westOption,
                        //visitedLocations = tempList
                    };

                }

            }



        }

        public string printGrid(string[,] grid, int xSize, int ySize, HashSet<Position> pathtoTrack)
        {
            string output = "\nGrid:\n";

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    Position checkPath = new Position(x, y);
                    string toWrite = "";
                    if (pathtoTrack.Contains(checkPath))
                    {
                        //toWrite = "\x1b[1m" + grid[x, y].ToString()+ "\x1b[0m";
                        toWrite = "\x1b[1mX\x1b[0m";

                    } else
                    {
                        toWrite = grid[x, y].ToString();
                    }
                    
                    //System.Console.WriteLine(toWrite);

                    output += " " + toWrite;
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