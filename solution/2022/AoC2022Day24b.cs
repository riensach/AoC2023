using System;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace AoC2022.solution
{
    public class Blizzard
    {
        public int x;
        public int y;
        public string direction;
        public Blizzard(int xInput, int yInput, string directionInput)
        {
            x = xInput;
            y = yInput;
            direction = directionInput;
        }
    }



    public class TravellerComparer : IEqualityComparer<Traveller>
    {
        public bool Equals(Traveller x, Traveller y)
        {
            return (x.x == y.x) && (x.y == y.y) && (x.steps == y.steps);
        }

        public int GetHashCode(Traveller obj)
        {
            return obj.x.GetHashCode();
        }
    }
    public class Traveller
    {
        public int x;
        public int y;
        public int targetX;
        public int targetY;
        public int steps;
        public int waitCount;
        public bool finished = false;
        public bool toRemove = false;
        public Traveller(int xInput, int yInput, int targetXInput, int targetYInput, int stepsInput, int waitCountInput)
        {
            x = xInput;
            y = yInput;
            targetX = targetXInput;
            targetY = targetYInput;
            steps = stepsInput;
            waitCount = waitCountInput;
        }

        /*public override bool Equals(object? obj)
        {
            return Equals((Traveller)obj);
        }
        public bool Equals(Traveller y)
        {
            //if (Enumerable.SequenceEqual(openedValves,y.openedValves) && currentValue.Equals(y.currentValue) && currentMinute.Equals(y.currentMinute) && currentPressureReleased.Equals(y.currentPressureReleased))
            if (x.Equals(y.x) && y.Equals(y.y) && steps.Equals(y.steps))
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                hash = hash * 23 + steps.GetHashCode();

                return hash;
            }
        }
    }


    /*
    IEnumerable<SearchState> NextStates(SearchState state, Maps maps)
    {
        foreach (var pos in new Pos[]{
            state.pos,
            state.pos with {irow=state.pos.irow -1},
            state.pos with {irow=state.pos.irow +1},
            state.pos with {icol=state.pos.icol -1},
            state.pos with {icol=state.pos.icol +1},
        })
        {
            if (maps.Get(state.time + 1, pos) == '.')
            {
                yield return state with
                {
                    time = state.time + 1,
                    pos = pos
                };
            }
        }
    }*/
    public class AoCDay24b
    {
        public string[,] grid;
        public string[,] emptyGrid;
        public IDictionary<string, int> shortestPathReference = new Dictionary<string, int>();
        public IDictionary<int, string> cubes = new Dictionary<int, string>();
        public List<Blizzard> blizzards = new List<Blizzard>();
        public List<Traveller> travellers = new List<Traveller>();

        public AoCDay24b(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int arrayLength = lines.Count();
            int arrayWidth = lines[0].Count();
            grid = new string[arrayLength, arrayWidth];
            emptyGrid = new string[arrayLength, arrayWidth];
            createGrid(arrayLength, arrayWidth);

            int iteratorX = 0;
            int iteratorY = 0;
            int entranceX = 0;
            int entranceY = 0;
            int exitX = 0;
            int exitY = 0;
            Blizzard blizzard;
            Traveller startingTraveller;
            Traveller newTraveller;
            foreach (string line in lines)
            {
                string[] parts = line.Split(",");

                foreach (var character in line)
                {

                    grid[iteratorX, iteratorY] = character.ToString();
                    emptyGrid[iteratorX, iteratorY] = character.ToString();
                    if(grid[iteratorX, iteratorY] != "#")
                    {
                        emptyGrid[iteratorX, iteratorY] = ".";
                    }

                    if (iteratorX == arrayLength-1 && character.ToString() == ".")
                    {
                        //Exit
                        exitX = iteratorX;
                        exitY = iteratorY;
                        //grid[iteratorX, iteratorY] = "Y";
                        //traveller = new Traveller(iteratorX, iteratorY);
                        //travellers.Add(traveller);
                    }
                    if (iteratorX == 0 && character.ToString() == ".")
                    {
                        //Entrance
                        entranceX = iteratorX;
                        entranceY = iteratorY;
                        //grid[iteratorX, iteratorY] = "X";
                    }

                    if(character.ToString() == "^" || character.ToString() == "v" || character.ToString() == "<" || character.ToString() == ">")
                    {
                        // Blizard
                        blizzard = new Blizzard(iteratorX, iteratorY, character.ToString());
                        blizzards.Add(blizzard);
                        grid[iteratorX, iteratorY] = character.ToString();
                    }
                    iteratorY++;
                }

                iteratorX++;
                iteratorY = 0;
            }

            Console.WriteLine("Starting at "+ entranceX+","+ entranceY + " with a target of "+ exitX + ","+ exitY);

            startingTraveller = new Traveller(entranceX, entranceY, exitX, exitY, 0, 0);
            travellers.Add(startingTraveller);


            Console.WriteLine(printGrid(arrayLength, arrayWidth));
            


            int minimumSteps = 10000000;
            int iteratorCount = 1;
            bool finished = false;

            while (!finished) {
                Array.Copy(emptyGrid, grid, emptyGrid.Length);
                // Update Blizzards
                foreach (Blizzard blizzardObj in blizzards)
                {
                    int newBlizzardX = 0;
                    int newBlizzardY = 0;
                    if(blizzardObj.direction == "<")
                    {
                        newBlizzardX = blizzardObj.x;
                        newBlizzardY = blizzardObj.y - 1;
                        if(grid[newBlizzardX, newBlizzardY] == "#")
                        {
                            // Hit a wall, time to reset 
                            newBlizzardY = arrayWidth-2;
                        }
                    } else if (blizzardObj.direction == ">")
                    {
                        newBlizzardX = blizzardObj.x;
                        newBlizzardY = blizzardObj.y + 1;
                        if (grid[newBlizzardX, newBlizzardY] == "#")
                        {
                            // Hit a wall, time to reset 
                            newBlizzardY = 1;
                        }
                    } else if (blizzardObj.direction == "^")
                    {
                        newBlizzardX = blizzardObj.x - 1;
                        newBlizzardY = blizzardObj.y;
                        if (grid[newBlizzardX, newBlizzardY] == "#")
                        {
                            // Hit a wall, time to reset 
                            newBlizzardX = arrayLength - 2;
                        }
                    } else if (blizzardObj.direction == "v")
                    {
                        newBlizzardX = blizzardObj.x + 1;
                        newBlizzardY = blizzardObj.y;
                        if (grid[newBlizzardX, newBlizzardY] == "#")
                        {
                            // Hit a wall, time to reset 
                            newBlizzardX = 1;
                        }
                    }
                    //Console.WriteLine("Updating blizzard " + blizzardObj.x + "," + blizzardObj.y + " to " + newBlizzardX + "," + newBlizzardY);
                    //grid[blizzardObj.x, blizzardObj.y] = ".";
                    blizzardObj.x = newBlizzardX;
                    blizzardObj.y = newBlizzardY;
                    grid[newBlizzardX, newBlizzardY] = blizzardObj.direction;
                }
                // I think the pathfinding is the issue
                List<Traveller> travellersToRemove = new List<Traveller>();
                List<Traveller> travellersToAdd = new List<Traveller>();




                /* 
                 * int f(Explorer explorer)
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

                foreach (var explorerOption in movementOptions(explorer, map))
                {
                    if (!previousExplorers.Contains(explorerOption))
                    {
                        previousExplorers.Add(explorerOption);
                        queue.Enqueue(explorerOption, f(explorerOption));
                    }
                }
            }
            throw new Exception();*/




                var queue = new PriorityQueue<Traveller, int>();

                int f(Traveller traveller)
                {
                    // estimate the remaining step count with Manhattan distance
                    var dist =
                        Math.Abs(traveller.targetX - traveller.x) +
                        Math.Abs(traveller.targetY - traveller.y);
                    return traveller.steps + dist;
                }

                queue.Enqueue(startingTraveller, f(startingTraveller));

                HashSet<Traveller> previousTravellers = new HashSet<Traveller>();

                while (queue.Count > 0)
                {

                    var traveller = queue.Dequeue();
                    if (traveller.x == traveller.targetX && traveller.y == traveller.targetY)
                    {
                        Console.WriteLine("Solved in steps: " + traveller.steps);
                    }



                    // Check right
                    if (grid[traveller.x, traveller.y + 1] == ".")
                    {
                        // Need to create a new traveller
                        newTraveller = new Traveller(traveller.x, traveller.y + 1, traveller.targetX, traveller.targetY, traveller.steps + 1, 0);
                        if (!previousTravellers.Contains(newTraveller))
                        {
                            queue.Enqueue(newTraveller, f(newTraveller));
                        }

                    }
                    // Check left
                    if (grid[traveller.x, traveller.y - 1] == ".")
                    {

                        // Need to create a new traveller
                        newTraveller = new Traveller(traveller.x, traveller.y - 1, traveller.targetX, traveller.targetY, traveller.steps + 1, 0);
                        if (!previousTravellers.Contains(newTraveller))
                        {
                            queue.Enqueue(newTraveller, f(newTraveller));
                        }

                    }
                    // Check down
                    if (grid[traveller.x + 1, traveller.y] == ".")
                    {

                        // Need to create a new traveller
                        newTraveller = new Traveller(traveller.x + 1, traveller.y, traveller.targetX, traveller.targetY, traveller.steps + 1, 0);
                        if (!previousTravellers.Contains(newTraveller))
                        {
                            queue.Enqueue(newTraveller, f(newTraveller));
                        }

                    }
                    // Check up
                    if (traveller.x != 0 && traveller.x != 1)
                    {
                        if (grid[traveller.x - 1, traveller.y] == ".")
                        {
                            // Need to create a new traveller
                            newTraveller = new Traveller(traveller.x - 1, traveller.y, traveller.targetX, traveller.targetY, traveller.steps + 1, 0);
                            if (!previousTravellers.Contains(newTraveller))
                            {
                                queue.Enqueue(newTraveller, f(newTraveller));
                            }
                        }
                    }
                    // Check current!
                    if (grid[traveller.x, traveller.y] == ".")
                    {
                        // We can wait
                        traveller.waitCount = traveller.waitCount + 1;
                        traveller.steps = traveller.steps + 1;
                    }


                


                    
                }

                if(travellers.Count < 1)
                {
                    finished = true;
                }
                //Console.WriteLine("Grid after " + iteratorCount + " minutes:");
                //Console.WriteLine(printGrid(arrayLength, arrayWidth));
                iteratorCount++;
                    //break;
                    // Try to move

                } 
            



            output = "Part A: minimum steps taken: " + minimumSteps;
        }

        public string printGrid(int xSize, int ySize)
        {
            string output = "\nGrid:\n";
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    string toWrite = grid[x, y];
                    output += "" + toWrite;
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
                    grid[x, y] = ".";
                }
            }
        }

        public string output;
    }
}