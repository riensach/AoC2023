using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using LoreSoft.MathExpressions;
using System.Collections.Generic;
using System.Data.Common;

namespace AoC2023.solution
{
    public class AoCDay11
    {

        private int worryLevel = 0;
        public List<Galexy> galaxyLocations = new List<Galexy>();
        public HashSet<Galexy> galexysChecked = new HashSet<Galexy>();
        public List<GalaxyRelationship> galaxyRelationships = new List<GalaxyRelationship>();

        public string output;

        public AoCDay11(int selectedPart, string input)
        {

            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int arrayLength = lines.Length;
            int arrayWidth = lines[0].Length;

            Map maps = new Map(lines, arrayLength, arrayWidth);
            maps.expandUniverse();

            int row = 0;
            int column = 0;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    
                    if (character == '#')
                    {
                        galaxyLocations.Add(new Galexy(new Position(row, column)));
                    }
                    column++;
                }
                row++;
                column = 0;
            }
            Int64 totalSteps = 0;
            foreach (Galexy galaxyLocation in galaxyLocations)
            {
                foreach (Galexy galaxyLocationSecond in galaxyLocations)
                {
                    if (galaxyLocation == galaxyLocationSecond) continue;
                    if (galexysChecked.Contains(galaxyLocationSecond)) continue;
                    //if (galaxyRelationships.Where(p => p.galexy1 == galaxyLocationSecond).Where(p => p.galexy2 == galaxyLocation).Count() > 0) continue;
                    Int64 stepsRequired = maps.calculateDifference(galaxyLocation, galaxyLocationSecond);

                    //galaxyRelationships.Add(new GalaxyRelationship(stepsRequired, galaxyLocation, galaxyLocationSecond));
                    galexysChecked.Add(galaxyLocation);
                    totalSteps = totalSteps + stepsRequired;
                    Console.WriteLine("Steps from galexy "+ galaxyLocation.position.x+","+ galaxyLocation.position.y+" to galexy "+ galaxyLocationSecond.position.x+","+ galaxyLocationSecond.position.y+" are "+ stepsRequired);
                }
            }

            output = "Part A:" + totalSteps;

            /*
            var startingLocation = new Position(0, 1);
            var targetLocation = new Position(arrayLength - 1, arrayWidth - 2);
            Console.WriteLine(targetLocation.ToString());

            var explorerInfo = new Explorer(0, startingLocation);
            var shortestPathExplorer = findThePath(maps, explorerInfo, targetLocation);
            //output = "Part A1:" + shortestPathExplorer.steps;

            shortestPathExplorer = findThePath(maps, explorerInfo, targetLocation);
            shortestPathExplorer = findThePath(maps, shortestPathExplorer, startingLocation);
            shortestPathExplorer = findThePath(maps, shortestPathExplorer, targetLocation);

            var totalTime = shortestPathExplorer.steps;

            output += "\nPart B:" + totalTime;*/
        }

        //output = "Part A: " + monkeyBusiness;









        public class Map
        {
            public string[] mapLines;
            public readonly int width;
            public readonly int length;
            public List<int> expandRows = new List<int>();
            public List<int> expandColumns = new List<int>();
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

            public void expandUniverse()
            {

                for (int i = 0; i < length; i++)
                {
                    if (mapLines[i].Contains('#')) continue;
                    expandRows.Add(i);
                    Console.WriteLine("Need to expand row " + i);
                }

                for (int x = 0; x < width; x++)
                {
                    bool expandColumn = true;
                    for (int i = 0; i < length; i++)
                    {
                        if (mapLines[i][x] == '#') expandColumn = false;
                    }
                    if (expandColumn)
                    {
                        expandColumns.Add(x);
                        Console.WriteLine("Need to expand column " + x);
                    }
                }
            }

            public Int64 calculateDifference(Galexy galexy1, Galexy galexy2)
            {
                var dist =
                    Math.Abs(galexy1.position.x - galexy2.position.x) +
                    Math.Abs(galexy1.position.y - galexy2.position.y);
                
                foreach(int expandedRow in expandRows)
                {
                    if((expandedRow > galexy1.position.x && expandedRow < galexy2.position.x) || (expandedRow > galexy2.position.x && expandedRow < galexy1.position.x))
                    {
                        dist = dist + 1000000 - 1;
                    }
                }
                foreach (int expandedColumns in expandColumns)
                {
                    if ((expandedColumns > galexy1.position.y && expandedColumns < galexy2.position.y) || (expandedColumns > galexy2.position.y && expandedColumns < galexy1.position.y))
                    {
                        dist = dist + 1000000 - 1;
                    }
                }


                return dist;
            }

        }

        public record Explorer(int steps, Position position);
        public record Galexy(Position position);
        public record GalaxyRelationship(int steps, Galexy galexy1, Galexy galexy2);
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
                if (map.getGridState(explorer.steps + 1, position) == '.')
                {
                    yield return explorer with
                    {
                        steps = explorer.steps + 1,
                        position = position
                    };
                }
            }
        }
    }
}