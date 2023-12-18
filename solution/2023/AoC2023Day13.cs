using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using static ABI.System.Collections.Generic.IReadOnlyDictionary_Delegates;

namespace AoC2023.solution
{
        public class AoCDay13
        {
        public string[,] grid;
        int arrayLength = 0;
        int arrayWidth = 0;

        public AoCDay13(int selectedPart, string input)
        {
            string[] grids = input.Split("\n\r\n");

            Console.WriteLine(grids[0]);
            Console.WriteLine("space");
            Console.WriteLine(grids[1]);

            
            int horizontalReflectionRowsAbove = 0;
            int verticalReflectionColumnsLeft = 0;
            foreach (string data in grids)
            {
                string[] lines = data.Split("\n");

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

                bool isReflection = false;
                for (int x = 1; x < arrayLength; x++)
                {
                    isReflection = true;
                    int z = 1;
                    int reflectionRows = 0;
                    while (isReflection == true)
                    {
                        
                        // Check for reflection horizontal
                        if ((x - z + 1 < 0) || (x + z >= arrayLength - 1))
                        {
                            //Console.WriteLine("breaking");
                            break;
                        }
                        isReflection = false;
                        int lineBefore = (x - z) + 1;
                        int lineAfter = x + z;
                        string lineComp1 = lines[lineBefore];
                        string lineComp2 = lines[lineAfter];

                        //Console.WriteLine("Checking lines "+ lineBefore+" and "+ lineAfter);
                        //Console.WriteLine(lineComp1);
                        //Console.WriteLine(lineComp2);
                        //Console.WriteLine("Checking lines " + lineComp1 + " and " + lineComp2);
                        if (lineComp1 == lineComp2)
                        {
                            reflectionRows++;
                            isReflection = true;
                        }
                        else
                        {
                            isReflection = false;
                            break;
                        }
                        z++;
                    }
                    if (isReflection == true && reflectionRows > 0)
                    {
                        reflectionRows = x + 1;
                        Console.WriteLine("Reflection at line "+ x + " with row count of " + reflectionRows);
                        
                        horizontalReflectionRowsAbove += reflectionRows;
                    }

                }





                //Vertical now. Rotating the grid first
                string[] newLines = new string[arrayWidth];
                row = 0;
                column = 0;
                foreach (string line in lines)
                {
                    foreach (var character in line)
                    {
                        int theID = arrayWidth - column - 1;
                        if (newLines.ElementAtOrDefault(theID) !=null)
                        {
                            newLines[theID] = newLines[theID] + character.ToString();
                        } else
                        {
                            newLines[theID] = character.ToString();
                        }
                        
                        column++;
                    }
                    row++;
                    column = 0;
                }




                isReflection = false;
                for (int x = 1; x <= arrayWidth; x++)
                {
                    isReflection = true;
                    int z = 1;
                    int reflectionRows = 0;
                    while (isReflection == true)
                    {

                        // Check for reflection horizontal
                        if ((x - z + 1 < 0) || (x + z >= arrayWidth - 1))
                        {
                            //Console.WriteLine("breaking");
                            break;
                        }
                        isReflection = false;
                        int lineBefore = (x - z) + 1;
                        int lineAfter = x + z;
                        string lineComp1 = newLines[lineBefore];
                        string lineComp2 = newLines[lineAfter];

                        Console.WriteLine("Checking lines "+ lineBefore+" and "+ lineAfter);
                        Console.WriteLine(lineComp1);
                        Console.WriteLine(lineComp2);
                        Console.WriteLine("Checking lines " + lineComp1 + " and " + lineComp2);
                        if (lineComp1 == lineComp2)
                        {
                            reflectionRows++;
                            isReflection = true;
                        }
                        else
                        {
                            isReflection = false;
                            break;
                        }
                        z++;
                    }
                    if (isReflection == true && reflectionRows > 0)
                    {
                        reflectionRows = x + 1;
                        //reflectionRows = arrayWidth - x - 1;
                        //Console.WriteLine("Reflection at line " + x + " with row count of " + reflectionRows);
                        verticalReflectionColumnsLeft += reflectionRows;
                    }

                }


            }

            string outputGrid = printGrid(arrayLength, arrayWidth);
            output += outputGrid;

            int finalCalt = verticalReflectionColumnsLeft + (horizontalReflectionRowsAbove * 100);
            output = "Part A: " + finalCalt;

            //output += "Part B:" + totalLoad;

            // too high 34327
            // too high 34326
            // too high 34267
            // wrong 34186
            // 33735



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


