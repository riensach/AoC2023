using System;
using System.IO;
using System.Diagnostics;
using LoreSoft.MathExpressions;
using System.Threading;
using AoC2019.solution;
using static AoC2019.solution.AoCDay18;
using System.Collections.Generic;
using System.Linq;

namespace AoC2019.solution
{
    public class AoCDay18
    {
        public List<int> shortestSteps = new List<int>();
        public List<Key> keys = new List<Key>();
        public List<Door> doors = new List<Door>();
        public IDictionary<string, string> gridValues = new Dictionary<string, string>();
        //public IDictionary<char, GridPosition> keys = new Dictionary<char, GridPosition>();
        //public IDictionary<char, GridPosition> doors = new Dictionary<char, GridPosition>();
        public char[,] grid;
        public AoCDay18(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int arrayLength = lines.Count();
            int arrayWidth = lines[0].Count();
            Console.WriteLine(arrayLength + "," + arrayWidth);
            grid = new char[arrayLength, arrayWidth];
            createGrid(arrayLength, arrayWidth);

            int iteratorX = 0;
            int iteratorY = 0;
            int startingX = 0;
            int startingY = 0;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    if (character.ToString() == "@")
                    {                        
                        startingX = iteratorX;
                        startingY = iteratorY;
                        grid[iteratorX, iteratorY] = '.';
                    }
                    else if (character != '#' && character != '#' && Char.IsLower(character))
                    {
                        // It's a key!
                        Key key = new Key(character, new GridPosition(iteratorX, iteratorY));
                        //GridPosition key = new GridPosition(iteratorX, iteratorY);
                        //keys.Add(character, key);
                        keys.Add(key);
                        grid[iteratorX, iteratorY] = character;

                    }
                    else if (character != '#' && character != '#' && Char.IsUpper(character))
                    {
                        // It's a door!

                        Door door = new Door(character, new GridPosition(iteratorX, iteratorY));
                        //GridPosition door = new GridPosition(iteratorX, iteratorY);
                        //doors.Add(character, door);
                        doors.Add(door);
                        grid[iteratorX, iteratorY] = character;

                    }
                    else
                    {
                        grid[iteratorX, iteratorY] = character;
                    }
                    iteratorY++;
                }
                iteratorX++;
                iteratorY = 0;
            }

            Explorer explorer = new Explorer(0, new GridPosition(startingX, startingY));
            findShortestPath(grid, explorer, 0, new List<char>());

            shortestSteps.Sort();
            int shortestPath = shortestSteps.First();
            output += "\nPart B: " + shortestPath;

        }

        public record GridPosition(int x, int y);
        public record Key(char value, GridPosition position);
        public record Door(char value, GridPosition position);
        public record Explorer(int steps, GridPosition position);
        // HERE
        // Think we need to add collected keys to the Explorer record
        // Otherwise it's hard to do iteration without knowing what we've opened 
        // Maybe add some kind of grouping of keys?

        public void findShortestPath(char[,] currentGrid, Explorer explorer, int currentSteps, List<char> collectedKeys)
        {
            IDictionary<int, Key> pathOptions = findPathOptions(explorer, collectedKeys);

            int dictCount = pathOptions.Count();

            // NOW HERE

            // Need to work on step logic, and also keeping track of shortest path options, and what to do with multiple options

            while (pathOptions.Count > 0)
            {
                if (dictCount == 1)
                {
                    Console.WriteLine("Only 1 - collecting key " + pathOptions.First().Value.value + " with " + pathOptions.First().Key + " steps.");
                    List<char> newColKeys = new List<char>(collectedKeys);
                    newColKeys.Add(pathOptions.First().Value.value);
                    explorer = new Explorer(pathOptions.First().Key, pathOptions.First().Value.position);
                    //currentSteps = currentSteps + pathOptions.First().Key;
                    if (newColKeys.Count() == keys.Count())
                    {
                        // All keys collected!
                        shortestSteps.Add(pathOptions.First().Key);
                        break;
                    }
                    pathOptions = findPathOptions(explorer, newColKeys);


                    // 1 option, no need to do anything else
                }
                else if (dictCount > 1)
                {
                    // This code below won't work, due to not keeping track of keys
                    /*
                    foreach (var pathOption in pathOptions)
                    {
                        List<char> newColKeys = new List<char>(collectedKeys);
                        newColKeys.Add(pathOption.Value.value);
                        explorer = new Explorer(pathOption.Key, pathOption.Value.position);
                        //currentSteps = currentSteps + pathOptions.First().Key;
                        if (newColKeys.Count() == keys.Count())
                        {
                            // All keys collected!
                            shortestSteps.Add(pathOption.Key);
                            break;
                        }
                        pathOptions = findPathOptions(explorer, newColKeys);
                    }
                    */

                    Console.WriteLine("Only 2");
                    // Multiple options, time to path
                }
                else
                {
                    Console.WriteLine("Only 0");
                    // No options - what do here? All keys collected?

                }
                
                dictCount = pathOptions.Count();
            }
            

        }

        public char getGridState(GridPosition position)
        {
            //Console.WriteLine(position.x + " - " + position.y + " - " + grid[position.x, position.y]);
            return grid[position.x, position.y];         
        }

        IEnumerable<Explorer> movementOptions(Explorer explorer, Key searchingKey, List<char> collectedKeys)
        {
            foreach (var position in new GridPosition[]{
            explorer.position,
            explorer.position with {x=explorer.position.x -1},
            explorer.position with {x=explorer.position.x +1},
            explorer.position with {y=explorer.position.y -1},
            explorer.position with {y=explorer.position.y +1},
        })
            {
                //Console.WriteLine("At position " + explorer.position.x + "," + explorer.position.y + " and looking for our options after " + explorer.steps + " steps");
                //Console.WriteLine(map.getGridState(explorer.steps + 1, position));
                //Console.WriteLine("Chewcking");
                if (getGridState(position) == '.' || (Char.IsLower(getGridState(position)) && getGridState(position) == searchingKey.value) || (collectedKeys.Contains(Char.ToUpper(getGridState(position))) || collectedKeys.Contains(Char.ToLower(getGridState(position)))))
                {
                    //Console.WriteLine("Returning option");
                    yield return explorer with
                    {
                        steps = explorer.steps + 1,
                        position = position
                    };
                }
            }
        }
        public IDictionary<int, Key> findPathOptions(Explorer explorer, List<char> collectedKeys)
        {
            IDictionary<int, Key> pathOptions = new Dictionary<int, Key>();
            var keysNotFound = keys.Where(x => !collectedKeys.Contains(x.value));
            var queue = new PriorityQueue<Key, int>();

            foreach (var key in keysNotFound)
            {
                // Let's iterate through the keys, only if they're not collected, and then we can prioritise going to the nearest key to see if it can be found
                //Console.WriteLine(key.value);
                var dist =
                    Math.Abs(key.position.x - explorer.position.x) +
                    Math.Abs(key.position.y - explorer.position.y);
                queue.Enqueue(key, dist);
            }

            while (queue.Count > 0)
            {
                var searchingKey = queue.Dequeue();
                var queueFindKey = new PriorityQueue<Explorer, int>();

                queueFindKey.Enqueue(explorer, 1);
                // This should be a priority-ordered list of keys to find
                //Console.WriteLine($"{searchingKey}");
                HashSet<Explorer> previousExplorers = new HashSet<Explorer>();

                while (queueFindKey.Count > 0)
                {
                    var explorerCurrent = queueFindKey.Dequeue();
                    //Console.WriteLine("Searching for key " + searchingKey.value + " at position " + searchingKey.position + " where we are at " + explorerCurrent.position);
                    if (explorerCurrent.position == searchingKey.position)
                    {
                        // Found the key here
                        //return explorerCurrent;
                        //Console.WriteLine("Found the key");
                        pathOptions.Add(explorerCurrent.steps, searchingKey);
                        break;
                    }

                    // GOT HERE - need to figure out how to recognise doors/keys in the movementOptions function, and what to do, and how to know if I have discovered that key
                    var explorerOptions = movementOptions(explorerCurrent, searchingKey, collectedKeys);
                    foreach (var explorerOption in explorerOptions)
                    {
                        // Check if we've been here before, and for less steps
                        var explorerOptionBefore = previousExplorers.Where(x => x.position == explorerOption.position).Order();
                        if (explorerOptionBefore.Count() > 0) {
                            //Console.WriteLine("Steps: " + explorerOptionBefore.First().steps + " vs " + explorerOption.steps);
                            if (explorerOptionBefore.First().steps <= explorerOption.steps)
                            {
                                //Console.WriteLine("Got here???");
                                continue;
                            }
                        }

                        if (!previousExplorers.Contains(explorerOption))
                        {
                            //Console.WriteLine("Still adding to queue");
                            previousExplorers.Add(explorerOption);
                            var dist =
                                Math.Abs(searchingKey.position.x - explorerOption.position.x) +
                                Math.Abs(searchingKey.position.y - explorerOption.position.y);
                            queueFindKey.Enqueue(explorerOption, dist);
                            //Console.WriteLine("somehow here1");
                        } 
                        //Console.WriteLine("somehow here2" + previousExplorers.Count());
                    }
                }
                //Console.WriteLine("Finished searching for key " + searchingKey.value);

            }
            return pathOptions;
        }

        public string printGrid(int xSize, int ySize)
        {
            string output = "\nGrid:\n";
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    char toWrite = grid[x, y];
                    output += "" + toWrite.ToString();
                }
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
                    grid[x, y] = '.';
                }
            }
        }

        public string output;
    }
}