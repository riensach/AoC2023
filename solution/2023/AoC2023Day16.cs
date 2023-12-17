using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.ComponentModel;
using ABI.Windows.ApplicationModel;

namespace AoC2023.solution
{
    public class AoCDay16
    {
        public string[,] grid;
        int arrayLength = 0;
        int arrayWidth = 0;

        public AoCDay16(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            arrayLength = lines.Count();
            arrayWidth = lines[0].Length;

            grid = new string[arrayLength, arrayWidth];
            grid = createGrid(grid, arrayLength, arrayWidth);
            int row = 0;
            int column = 0;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    grid[row, column] = character.ToString();
                    column++;
                }
                row++;
                column = 0;
            }

            Dictionary<int, string> beams = new Dictionary<int, string>();
            HashSet<string> energizedTiles = new HashSet<string>();
            beams.Add(0, "0,-1,>");
            int currentKey = 0;

            for (int i = 0; i < 700; i++)
            {
                //Console.WriteLine("Beams: " + beams.Count + " iterations:" + i);
                if (beams.Count < 1)
                {
                    break;
                    // All beams are done for
                }
                
                
                Dictionary<int, string> beamsCopy = new Dictionary<int, string>(beams);
                //Dictionary<int, string> beamsCopy = new Dictionary<int, string>(beams);
                foreach (KeyValuePair<int, string> beam in beams)
                {
                    string[] beamDetails = beam.Value.Split(",");
                    int x = int.Parse(beamDetails[0]);
                    int y = int.Parse(beamDetails[1]);
                    if (!energizedTiles.Contains(x + "," + y))
                    {
                        energizedTiles.Add(x + "," + y);
                    }
                    string direction = beamDetails[2];
                    string destGridValue = "";
                    //Console.WriteLine(x+","+y);
                    if (direction == ">")
                    {
                        if (y + 1 > arrayWidth - 1)
                        {
                            // Time to remove the beam                            
                            beamsCopy.Remove(beam.Key);                            
                            continue;
                        }
                        destGridValue = grid[x, y + 1];
                        int newY = y + 1;
                        if (destGridValue == "." || destGridValue == "-")
                        {
                            beamsCopy[beam.Key] = x + "," + newY + "," + direction;
                        }
                        else if (destGridValue == "/")
                        {
                            beamsCopy[beam.Key] = x + "," + newY + "," + "^";
                        }
                        else if (destGridValue == "\\")
                        {
                            beamsCopy[beam.Key] = x + "," + newY + "," + "V";
                        }
                        else if (destGridValue == "|")
                        {
                            beamsCopy[beam.Key] = x + "," + newY + "," + "^";
                            int newIndex = currentKey + 1;
                            currentKey++;
                            beamsCopy.Add(newIndex, x + "," + newY + "," + "V");
                        }
                    } else if (direction == "<")
                    {
                        if (y - 1 < 0)
                        {
                            // Time to remove the beam
                            beamsCopy.Remove(beam.Key);
                            continue;
                        }
                        destGridValue = grid[x, y - 1];
                        int newY = y - 1;
                        if (destGridValue == "." || destGridValue == "-")
                        {
                            beamsCopy[beam.Key] = x + "," + newY + "," + direction;
                        }
                        else if (destGridValue == "/")
                        {
                            beamsCopy[beam.Key] = x + "," + newY + "," + "V";
                        }
                        else if (destGridValue == "\\")
                        {
                            beamsCopy[beam.Key] = x + "," + newY + "," + "^";
                        }
                        else if (destGridValue == "|")
                        {
                            beamsCopy[beam.Key] = x + "," + newY + "," + "^";
                            int newIndex = currentKey + 1;
                            currentKey++;
                            beamsCopy.Add(newIndex, x + "," + newY + "," + "V");
                        }
                    }
                    else if (direction == "^")
                    {
                        if (x - 1 < 0)
                        {
                            // Time to remove the beam
                            beamsCopy.Remove(beam.Key);
                            continue;
                        }
                        destGridValue = grid[x - 1, y];
                        int newX = x - 1;
                        if (destGridValue == "." || destGridValue == "|")
                        {
                            beamsCopy[beam.Key] = newX + "," + y + "," + direction;
                        }
                        else if (destGridValue == "/")
                        {
                            beamsCopy[beam.Key] = newX + "," + y + "," + ">";
                        }
                        else if (destGridValue == "\\")
                        {
                            beamsCopy[beam.Key] = newX + "," + y + "," + "<";
                        }
                        else if (destGridValue == "-")
                        {
                            beamsCopy[beam.Key] = newX + "," + y + "," + ">";
                            int newIndex = currentKey + 1;
                            currentKey++;
                            beamsCopy.Add(newIndex, newX + "," + y + "," + "<");
                        }
                    }
                    else if (direction == "V")
                    {
                        if (x + 1 > arrayLength - 1)
                        {
                            // Time to remove the beam
                            beamsCopy.Remove(beam.Key);
                            continue;
                        }
                        destGridValue = grid[x + 1, y];
                        int newX = x + 1;
                        if (destGridValue == "." || destGridValue == "|")
                        {
                            beamsCopy[beam.Key] = newX + "," + y + "," + direction;
                        } else if (destGridValue == "/")
                        {
                            beamsCopy[beam.Key] = newX + "," + y + "," + "<";
                        }
                        else if (destGridValue == "\\")
                        {
                            beamsCopy[beam.Key] = newX + "," + y + "," + ">";
                        }
                        else if (destGridValue == "-")
                        {
                            beamsCopy[beam.Key] = newX + "," + y + "," + ">";
                            int newIndex = currentKey + 1;
                            currentKey++;
                            beamsCopy.Add(newIndex, newX + "," + y + "," + "<");
                        }
                    }

                }
                //Console.WriteLine("got here");
                beams.Clear();
                beams = new Dictionary<int, string>(beamsCopy);
                beams = beams.GroupBy(pair => pair.Value)
                         .Select(group => group.First())
                         .ToDictionary(pair => pair.Key, pair => pair.Value);
                //beamsCopy.Clear();
            }

            int bamCount = beams.Count(f => f.Value == "49,42,V");
            // 
            Console.WriteLine("Total beams left: " + beams.Count() + " - " + bamCount);
            //return;
            

            int energyTiles = energizedTiles.Count() - 1;
            // 1699 too low
            // 7488
            // 7495
            // 7496 correct


            output = "Part A:" + energyTiles;
            //return;
















            Dictionary<string, int> diffStartingBeams = new Dictionary<string, int>();
            row = 0;
            for (int x = 0; x < arrayLength; x++)
            {
                for (int y = 0; y < arrayWidth; y++)
                {
                    if(x == 0)
                    {
                        diffStartingBeams.Add("-1," + y +",V", 0);
                    } else if (x == arrayLength - 1)
                    {
                        diffStartingBeams.Add(arrayLength + "," + y + ",^", 0);
                    }
                    else if (y == 0)
                    {
                        diffStartingBeams.Add(x + ",-1,>", 0);
                    }
                    else if (y == arrayWidth - 1)
                    {
                        diffStartingBeams.Add(x + ","+ arrayLength+",<", 0);
                    }
                }
            }
            
            diffStartingBeams.Add("0,-1,>", 0);
            //diffStartingBeams.Add("-1,0,V", 0);
            //diffStartingBeams.Add(arrayLength-1+",-1,<", 0);
            //diffStartingBeams.Add("0,"+ arrayWidth - 1 + ",^", 0);

            //diffStartingBeams.Add("0,-1,>", 0);
            //diffStartingBeams.Add("0,-1,>", 0);
            foreach (KeyValuePair<string, int> mainBeam in diffStartingBeams)
            {
                //Console.WriteLine("Looking at location string: "+ mainBeam.Key);
                
            }
            //return;

            foreach (KeyValuePair<string, int> mainBeam in diffStartingBeams)
            {
                Console.WriteLine("Attempting beam starting location: " + mainBeam.Key);
                Dictionary<int, string> beamsIt = new Dictionary<int, string>();
                HashSet<string> energizedTilesIt = new HashSet<string>();
                beamsIt.Add(0, mainBeam.Key);
                currentKey = 0;

                for (int i = 0; i < 670; i++)
                {
                    //Console.WriteLine("Beams: " + beamsIt.Count + " iterations:" + i);
                    if (beamsIt.Count < 1)
                    {
                        break;
                        // All beams are done for
                    }
                    
                    Dictionary<int, string> beamsCopy = new Dictionary<int, string>(beamsIt);
                    //Dictionary<int, string> beamsCopy = new Dictionary<int, string>(beamsIt);
                    foreach (KeyValuePair<int, string> beam in beamsIt)
                    {
                        string[] beamDetails = beam.Value.Split(",");
                        int x = int.Parse(beamDetails[0]);
                        int y = int.Parse(beamDetails[1]);








                        
                        if (!energizedTilesIt.Contains(x + "," + y))
                        {
                            energizedTilesIt.Add(x + "," + y);
                        }
                        string direction = beamDetails[2];
                        string destGridValue = "";
                        //Console.WriteLine(x+","+y);
                        if (direction == ">")
                        {
                            if (y + 1 > arrayWidth - 1)
                            {
                                // Time to remove the beam
                                beamsCopy.Remove(beam.Key);
                                continue;
                            }
                            destGridValue = grid[x, y + 1];
                            int newY = y + 1;
                            if (destGridValue == "." || destGridValue == "-")
                            {
                                beamsCopy[beam.Key] = x + "," + newY + "," + direction;
                            }
                            else if (destGridValue == "/")
                            {
                                beamsCopy[beam.Key] = x + "," + newY + "," + "^";
                            }
                            else if (destGridValue == "\\")
                            {
                                beamsCopy[beam.Key] = x + "," + newY + "," + "V";
                            }
                            else if (destGridValue == "|")
                            {
                                beamsCopy[beam.Key] = x + "," + newY + "," + "^";
                                int newIndex = currentKey + 1;
                                currentKey++;
                                beamsCopy.Add(newIndex, x + "," + newY + "," + "V");
                            }
                        }
                        else if (direction == "<")
                        {
                            if (y - 1 < 0)
                            {
                                // Time to remove the beam
                                beamsCopy.Remove(beam.Key);
                                continue;
                            }
                            destGridValue = grid[x, y - 1];
                            int newY = y - 1;
                            if (destGridValue == "." || destGridValue == "-")
                            {
                                beamsCopy[beam.Key] = x + "," + newY + "," + direction;
                            }
                            else if (destGridValue == "/")
                            {
                                beamsCopy[beam.Key] = x + "," + newY + "," + "V";
                            }
                            else if (destGridValue == "\\")
                            {
                                beamsCopy[beam.Key] = x + "," + newY + "," + "^";
                            }
                            else if (destGridValue == "|")
                            {
                                beamsCopy[beam.Key] = x + "," + newY + "," + "^";
                                int newIndex = currentKey + 1;
                                currentKey++;
                                beamsCopy.Add(newIndex, x + "," + newY + "," + "V");
                            }
                        }
                        else if (direction == "^")
                        {
                            if (x - 1 < 0)
                            {
                                // Time to remove the beam
                                beamsCopy.Remove(beam.Key);
                                continue;
                            }
                            destGridValue = grid[x - 1, y];
                            int newX = x - 1;
                            if (destGridValue == "." || destGridValue == "|")
                            {
                                beamsCopy[beam.Key] = newX + "," + y + "," + direction;
                            }
                            else if (destGridValue == "/")
                            {
                                beamsCopy[beam.Key] = newX + "," + y + "," + ">";
                            }
                            else if (destGridValue == "\\")
                            {
                                beamsCopy[beam.Key] = newX + "," + y + "," + "<";
                            }
                            else if (destGridValue == "-")
                            {
                                beamsCopy[beam.Key] = newX + "," + y + "," + ">";
                                int newIndex = currentKey + 1;
                                currentKey++;
                                beamsCopy.Add(newIndex, newX + "," + y + "," + "<");
                            }
                        }
                        else if (direction == "V")
                        {
                            if (x + 1 > arrayLength - 1)
                            {
                                // Time to remove the beam
                                beamsCopy.Remove(beam.Key);
                                continue;
                            }
                            destGridValue = grid[x + 1, y];
                            int newX = x + 1;
                            if (destGridValue == "." || destGridValue == "|")
                            {
                                beamsCopy[beam.Key] = newX + "," + y + "," + direction;
                            }
                            else if (destGridValue == "/")
                            {
                                beamsCopy[beam.Key] = newX + "," + y + "," + "<";
                            }
                            else if (destGridValue == "\\")
                            {
                                beamsCopy[beam.Key] = newX + "," + y + "," + ">";
                            }
                            else if (destGridValue == "-")
                            {
                                beamsCopy[beam.Key] = newX + "," + y + "," + ">";
                                int newIndex = currentKey + 1;
                                currentKey++;
                                beamsCopy.Add(newIndex, newX + "," + y + "," + "<");
                            }
                        }

                    }
                    //Console.WriteLine("got here");
                    beamsIt.Clear();
                    beamsIt = new Dictionary<int, string>(beamsCopy);
                    beamsIt = beamsIt.GroupBy(pair => pair.Value)
                         .Select(group => group.First())
                         .ToDictionary(pair => pair.Key, pair => pair.Value);
                }

                energyTiles = energizedTilesIt.Count() - 1;
                diffStartingBeams[mainBeam.Key] = energyTiles;
            }
            // 1699 too low
            // 7488
            // 7495
            // 7496 correct
            int maxEnergyTiles = diffStartingBeams.Values.Max();

            output += "\nPart B:" + maxEnergyTiles;






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