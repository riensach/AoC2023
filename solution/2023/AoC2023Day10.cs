using System;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using Windows.UI.StartScreen;

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
            

            int row = 0;
            int column = 0;
            int startingLocationX = 0;
            int startingLocationY = 0;

            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    grid[row, column] = character.ToString();        
                    if(character == 'S')
                    {
                        startingLocationX = row;
                        startingLocationY = column;
                    } else if (character != '.')
                    {
                        possiblePositions.Add(new Position(row, column));
                    }
                    column++;
                }
                row++;
                column = 0;
            }
            HashSet<Position> visitedLocations = new HashSet<Position>();
            var startingLocation = new Position(startingLocationX, startingLocationY);
            Console.WriteLine(startingLocation.ToString());
            var explorerInfo = new Explorer(0, startingLocation, visitedLocations);
            Map maps = new Map(lines, arrayLength, arrayWidth);
            foreach (Position possiblePosition in possiblePositions)
            {
                Console.WriteLine("Checking path option " + possiblePosition.x + "," + possiblePosition.y); 
                var shortestPathExplorer = findThePath(maps, explorerInfo, possiblePosition);       
                if(shortestPathExplorer.steps > 0)
                {
                    shortestPathOptions.Add(possiblePosition, shortestPathExplorer.steps);
                }
                

            }
            foreach (var valueHistory in shortestPathOptions)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(valueHistory))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(valueHistory);
                    Console.WriteLine("{0}={1}", name, value);
                }
            }
            int highestSteps = shortestPathOptions.Values.Max();

         

            output = "Part A: " + highestSteps;

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

            public char getGridState(int steps, Position position)
            {
                if (position.x < 0 || position.y < 0 || position.x >= mapLines.Length || position.y >= mapLines[0].Length) return '.';
                return mapLines[position.x][position.y];                

            }
        }
        public record Explorer(int steps, Position position, HashSet<Position> visitedLocations);
        public record Position(int x, int y);
        public Explorer findThePath(Map map, Explorer explorerInformation, Position targetPosition)
        {
            var queue = new PriorityQueue<Explorer, int>();

            int f(Explorer explorer)
            {
                // estimate the remaining step count with Manhattan distance
                var dist =
                    Math.Abs(targetPosition.x - explorer.position.x) +
                    Math.Abs(targetPosition.y - explorer.position.y);
                return explorer.steps + dist;
            }

            queue.Enqueue(explorerInformation, f(explorerInformation));
            HashSet<Explorer> previousExplorers = new HashSet<Explorer>();

            while (queue.Count > 0)
            {
                var explorer = queue.Dequeue();
                if (explorer.position == targetPosition)
                {
                    return explorer;
                }

                foreach (Explorer explorerOption in movementOptions(explorer, map))
                {
                    explorerOption.visitedLocations.Add(explorerOption.position);
                    if (!previousExplorers.Contains(explorerOption))
                    {
                        //if (explorer.steps > 1000) break;
                        //explorerOption.visitedLocations.Add(explorerOption.position);
                        //Console.WriteLine("Adding previous location "+ explorerOption.position.x+","+ explorerOption.position.y);
                        previousExplorers.Add(explorerOption);
                        queue.Enqueue(explorerOption, f(explorerOption));
                        //Console.WriteLine("Adding explorer with steps: "+ explorer.steps);




                        //
                        //
                        //
                        // STUCK ON THE VISITED LOCATIONS ASPECT
                        // NEED TO FIGURE THAT OUT
                        //
                        //
                        //




                    }
                }
            }
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
            if (map.getGridState(explorer.steps + 1, southOption) != 'S' && map.getGridState(explorer.steps + 1, southOption) != '.' 
                && (map.getGridState(explorer.steps + 1, explorer.position) == 'S' || map.getGridState(explorer.steps + 1, explorer.position) == '|' 
                || map.getGridState(explorer.steps + 1, explorer.position) == 'F' || map.getGridState(explorer.steps + 1, explorer.position) == '7')
                && !explorer.visitedLocations.Contains(southOption)
                )
            {

                if (map.getGridState(explorer.steps + 1, southOption) == 'L' || map.getGridState(explorer.steps + 1, southOption) == 'J' || map.getGridState(explorer.steps + 1, southOption) == '|')
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
            if (map.getGridState(explorer.steps + 1, northOption) != 'S' && map.getGridState(explorer.steps + 1, northOption) != '.'
                && (map.getGridState(explorer.steps + 1, explorer.position) == 'S' || map.getGridState(explorer.steps + 1, explorer.position) == '|'
                || map.getGridState(explorer.steps + 1, explorer.position) == 'J' || map.getGridState(explorer.steps + 1, explorer.position) == 'L')
                && !explorer.visitedLocations.Contains(northOption)
                )
            {
                //Console.WriteLine("north option :: " + explorer.position.x +","+ explorer.position.y + " :: " + northOption.x +", "+ northOption.y);
                if (map.getGridState(explorer.steps + 1, northOption) == 'F' || map.getGridState(explorer.steps + 1, northOption) == '7' || map.getGridState(explorer.steps + 1, northOption) == '|')
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
            if (map.getGridState(explorer.steps + 1, eastOption) != 'S' && map.getGridState(explorer.steps + 1, eastOption) != '.'
                && (map.getGridState(explorer.steps + 1, explorer.position) == 'S' || map.getGridState(explorer.steps + 1, explorer.position) == '-'
                || map.getGridState(explorer.steps + 1, explorer.position) == 'L' || map.getGridState(explorer.steps + 1, explorer.position) == 'F')
                && !explorer.visitedLocations.Contains(eastOption)
                )
            {

                //Console.WriteLine("east option");
                if (map.getGridState(explorer.steps + 1, eastOption) == '7' || map.getGridState(explorer.steps + 1, eastOption) == 'J' || map.getGridState(explorer.steps + 1, eastOption) == '-')
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
            if (map.getGridState(explorer.steps + 1, westOption) != 'S' && map.getGridState(explorer.steps + 1, westOption) != '.'
                && (map.getGridState(explorer.steps + 1, explorer.position) == 'S' || map.getGridState(explorer.steps + 1, explorer.position) == '-'
                || map.getGridState(explorer.steps + 1, explorer.position) == 'J' || map.getGridState(explorer.steps + 1, explorer.position) == '7')
                && !explorer.visitedLocations.Contains(westOption)
                )
            {
                if (map.getGridState(explorer.steps + 1, westOption) == 'L' || map.getGridState(explorer.steps + 1, westOption) == 'F' || map.getGridState(explorer.steps + 1, westOption) == '-')
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

        public string printGrid(string[,] grid, int xSize, int ySize)
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