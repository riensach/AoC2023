using System;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Linq;

namespace AoC2022.solution
{

    public class AoCDay24
    {

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

            public string getGridState(int steps, Position position)
            {
                if (position.x == 0 && position.y == 1)
                {
                    // Starting position!
                    return ".";
                }

                if (position.x == length - 1 && position.y == width - 2)
                {
                    // Destination position!
                    return ".";
                }

                if (position.x <= 0 || position.x >= length - 1 || position.y <= 0 || position.y >= width - 1)
                {
                    // Out of range!
                    return "#";
                }
                // If we've got this far, we now need to calculate blizzard movements relative to time
                var leftBlizzard = (position.y - 1 - steps + 1000 * (width - 2)) % (width - 2) + 1;
                var rightBlizzard = (position.y - 1 + steps + 1000 * (width - 2)) % (width - 2) + 1;
                var upBlizzard = (position.x - 1 - steps + 1000 * (length - 2)) % (length - 2) + 1;
                var downBlizzard = (position.x - 1 + steps + 1000 * (length - 2)) % (length - 2) + 1;
                /*Console.WriteLine(position.x + " - " + leftBlizzard);
                Console.WriteLine(position.x + " - " + rightBlizzard);
                Console.WriteLine(position.y + " - " + upBlizzard);
                Console.WriteLine(position.y + " - " + downBlizzard + " - " + steps);*/
                if (
                    mapLines[position.x][leftBlizzard] == '>' ||
                    mapLines[position.x][rightBlizzard] == '<' ||
                    mapLines[upBlizzard][position.y] == 'v' ||
                    mapLines[downBlizzard][position.y] == '^'
                )
                {
                    return "#";
                }
                else
                {
                    return ".";
                }

            }
        }
        public record Explorer(int steps, Position position);
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

                foreach (var explorerOption in movementOptions(explorer, map))
                {
                    if (!previousExplorers.Contains(explorerOption))
                    {
                        previousExplorers.Add(explorerOption);
                        queue.Enqueue(explorerOption, f(explorerOption));
                    }
                }
            }
            throw new Exception();
        }

        IEnumerable<Explorer> movementOptions(Explorer explorer, Map map)
        {
            foreach (var position in new Position[]{
            explorer.position,
            explorer.position with {x=explorer.position.x -1},
            explorer.position with {x=explorer.position.x +1},
            explorer.position with {y=explorer.position.y -1},
            explorer.position with {y=explorer.position.y +1},
        })
            {
                //Console.WriteLine("At position " + explorer.position.x + "," + explorer.position.y + " and looking for our options after " + explorer.steps + " steps");
                //Console.WriteLine(map.getGridState(explorer.steps + 1, position));
                if (map.getGridState(explorer.steps + 1, position) == ".")
                {
                    yield return explorer with
                    {
                        steps = explorer.steps + 1,
                        position = position
                    };
                }
            }
        }

        public AoCDay24(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int arrayLength = lines.Length;
            int arrayWidth = lines[0].Length;

            Map maps = new Map(lines, arrayLength, arrayWidth);
            var startingLocation = new Position(0, 1);
            var targetLocation = new Position(arrayLength - 1, arrayWidth - 2);
            Console.WriteLine(targetLocation.ToString());

            var explorerInfo = new Explorer(0, startingLocation);
            var shortestPathExplorer = findThePath(maps, explorerInfo, targetLocation);
            output = "Part A:" + shortestPathExplorer.steps;

            shortestPathExplorer = findThePath(maps, explorerInfo, targetLocation);
            shortestPathExplorer = findThePath(maps, shortestPathExplorer, startingLocation);
            shortestPathExplorer = findThePath(maps, shortestPathExplorer, targetLocation);

            var totalTime = shortestPathExplorer.steps;

            output += "\nPart B:" + totalTime;

        }

        public string output;
    }
}