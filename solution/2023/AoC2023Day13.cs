﻿using System;
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
        public string[,] gridTemp;
        int arrayLength = 0;
        int arrayWidth = 0;

        public AoCDay13(int selectedPart, string input)
        {
            string[] grids = input.Split("\n\r\n");            
            int horizontalReflectionRowsAbove = 0;
            int horizontalReflectionRowsAboveSecond = 0;
            int verticalReflectionColumnsLeft = 0;
            int verticalReflectionColumnsLeftSecond = 0;
            foreach (string data in grids)
            {
                string[] lines = data.Split("\n");
                lines[0] = lines[0].Replace("\r", "").Replace("\n", "");

                arrayLength = lines.Count();
                arrayWidth = lines[0].Length;

                grid = new string[arrayLength, arrayWidth];
                grid = createGrid(grid, arrayLength, arrayWidth);
                int row = 0;
                int column = 0;
                foreach (string line in lines)
                {
                    line.Replace("\r", "").Replace("\n", "");
                    foreach (var character in line)
                    {
                        if (character.ToString() == "\r") continue;
                        if (character.ToString() == "\n") continue;
                        //Console.WriteLine(character.ToString());
                        grid[row, column] = character.ToString();
                        column++;
                    }
                    row++;
                    column = 0;
                }

                bool isReflection = false;
                for (int x = arrayLength - 1; x >= -1; x--)
                {
                    isReflection = false;
                    int z = 1;
                    int reflectionRows = 0;
                    while ((x - z + 1 > -1) && (x + z < arrayLength))
                    {
                        
                        int lineBefore = (x - z) + 1;
                        int lineAfter = x + z;
                        string lineComp1 = "";
                        string lineComp2 = "";
                        for (int w = 0; w < arrayWidth; w++)
                        {
                            lineComp1 = lineComp1 + grid[lineBefore, w];
                        }
                        for (int w = 0; w < arrayWidth; w++)
                        {
                            lineComp2 = lineComp2 + grid[lineAfter, w];
                        }

                        //Console.WriteLine("Checking lines "+ lineBefore+" and "+ lineAfter);
                        //Console.WriteLine(lineComp1);
                        //Console.WriteLine(lineComp2);
                        //Console.WriteLine("Checking linesz ");
                        if (lineComp1 == lineComp2)
                        {
                            //Console.WriteLine("got here"); 
                            reflectionRows++;
                            isReflection = true;
                            
                        }
                        else
                        {
                            //Console.WriteLine("got here wut");
                            isReflection = false;
                            break;
                        }
                        z++;
                    }
                    //Console.WriteLine(isReflection);
                    if (isReflection == true)
                    {
                        reflectionRows = x + 1;
                        horizontalReflectionRowsAbove += reflectionRows;
                        Console.WriteLine("Horizontal reflection at line " + x + " with row count of " + reflectionRows + " with a max rows of " + arrayLength);
                        break;
                    }

                }


                isReflection = false;
                for (int x = arrayWidth - 1; x >= -1; x--)
                {
                    isReflection = false;
                    int z = 1;
                    int reflectionRows = 0;
                    while ((x - z + 1 > -1) && (x + z < arrayWidth))
                    {

                        // Check for reflection horizontal
                        if ((x - z + 1 < 0) || (x + z >= arrayWidth))
                        {
                            break;
                        }
                        int lineBefore = (x - z) + 1;
                        int lineAfter = x + z;
                        string lineComp1 = "";
                        string lineComp2 = "";
                        for (int w = 0; w < arrayLength; w++)
                        {
                            lineComp1 = lineComp1 + grid[w, lineBefore];
                        }
                        for (int w = 0; w < arrayLength; w++)
                        {
                            lineComp2 = lineComp2 + grid[w, lineAfter];
                        }

                        //Console.WriteLine("Checking lines " + lineBefore + " and " + lineAfter);
                        //Console.WriteLine(arrayWidth);
                        //Console.WriteLine(lineComp1);
                        //Console.WriteLine(lineComp2);
                        //Console.WriteLine("Checking linesz ");
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
                    if (isReflection == true)
                    {
                        reflectionRows = x + 1;                        
                        verticalReflectionColumnsLeft += reflectionRows;
                        Console.WriteLine("Vertical reflection at line " + x + " with row count of " + reflectionRows + " with a max rows of " + arrayLength);
                        break;
                    }

                }


            }

            string outputGrid = printGrid(arrayLength, arrayWidth);
            output += outputGrid;

            int finalCalt = verticalReflectionColumnsLeft + (horizontalReflectionRowsAbove * 100);
            output = "Part A: " + finalCalt;


            foreach (string data in grids)
            {
                string[] lines = data.Split("\n");
                lines[0] = lines[0].Replace("\r", "").Replace("\n", "");

                arrayLength = lines.Count();
                arrayWidth = lines[0].Length;

                grid = new string[arrayLength, arrayWidth];
                gridTemp = new string[arrayLength, arrayWidth];
                grid = createGrid(grid, arrayLength, arrayWidth);
                gridTemp = createGrid(gridTemp, arrayLength, arrayWidth);
                int row = 0;
                int column = 0;
                foreach (string line in lines)
                {
                    line.Replace("\r", "").Replace("\n", "");
                    foreach (var character in line)
                    {
                        if (character.ToString() == "\r") continue;
                        if (character.ToString() == "\n") continue;
                        //Console.WriteLine(character.ToString());
                        grid[row, column] = character.ToString();
                        gridTemp[row, column] = character.ToString();
                        column++;
                    }
                    row++;
                    column = 0;
                }

                bool isReflection = false;
                for (int a = 0; a < arrayLength; a++)
                {
                    if (isReflection == true)
                    {
                        Console.WriteLine("wut1"); 
                        break;
                    }
                    for (int b = 0; b < arrayWidth; b++)
                    {
                        if (isReflection == true)
                        {
                            Console.WriteLine("wut");
                            break;
                        }
                        //
                        //gridTemp[x, y] = ".";

                        
                        for (int x = arrayLength - 1; x >= -1; x--)
                        {
                            isReflection = false;
                            int z = 1;
                            int reflectionRows = 0;
                            while ((x - z + 1 > -1) && (x + z < arrayLength))
                            {

                                int lineBefore = (x - z) + 1;
                                int lineAfter = x + z;
                                string lineComp1 = "";
                                string lineComp2 = "";
                                for (int w = 0; w < arrayWidth; w++)
                                {
                                    if(a==lineBefore && b == w)
                                    {
                                        Console.WriteLine("got here3");
                                        //lineComp1 = lineComp1 + (grid[lineBefore, w] == "." ? "#": ".");
                                        lineComp1 = lineComp1 + grid[lineBefore, w];
                                    } else
                                    {
                                        lineComp1 = lineComp1 + grid[lineBefore, w];
                                    }
                                    
                                }
                                for (int w = 0; w < arrayWidth; w++)
                                {
                                    if (a == lineAfter && b == w)
                                    {
                                        Console.WriteLine("got here4");
                                        //lineComp2 = lineComp2 + (grid[lineAfter, w] == "." ? "#" : ".");
                                        lineComp2 = lineComp2 + grid[lineAfter, w];
                                    }
                                    else
                                    {
                                        lineComp2 = lineComp2 + grid[lineAfter, w];
                                    }                                    
                                }

                                //Console.WriteLine("Checking lines "+ lineBefore+" and "+ lineAfter);
                                //Console.WriteLine(lineComp1);
                                //Console.WriteLine(lineComp2);
                                //Console.WriteLine("Checking linesz ");
                                if (lineComp1 == lineComp2)
                                {
                                    //Console.WriteLine("got here"); 
                                    reflectionRows++;
                                    isReflection = true;

                                }
                                else
                                {
                                    //Console.WriteLine("got here wut");
                                    isReflection = false;
                                    break;
                                }
                                z++;
                            }
                            //Console.WriteLine(isReflection);
                            if (isReflection == true)
                            {
                                reflectionRows = x + 1;
                                horizontalReflectionRowsAboveSecond += reflectionRows;
                                Console.WriteLine("Horizontal reflection at line " + x + " with row count of " + reflectionRows + " with a max rows of " + arrayLength);
                                break;
                            }

                        }
                        if (isReflection == true)
                        {
                            //break;
                        }

                        isReflection = false;
                        for (int x = arrayWidth - 1; x >= -1; x--)
                        {
                            isReflection = false;
                            int z = 1;
                            int reflectionRows = 0;
                            while ((x - z + 1 > -1) && (x + z < arrayWidth))
                            {

                                // Check for reflection horizontal
                                if ((x - z + 1 < 0) || (x + z >= arrayWidth))
                                {
                                    break;
                                }
                                int lineBefore = (x - z) + 1;
                                int lineAfter = x + z;
                                string lineComp1 = "";
                                string lineComp2 = "";

                                for (int w = 0; w < arrayLength; w++)
                                {
                                    if (a == w && b == lineBefore)
                                    {
                                        Console.WriteLine("got here2");
                                        //lineComp1 = lineComp1 + (grid[w, lineBefore] == "." ? "#" : ".");
                                        lineComp1 = lineComp1 + grid[w, lineBefore];
                                    }
                                    else
                                    {
                                        lineComp1 = lineComp1 + grid[w, lineBefore];
                                    }

                                }
                                for (int w = 0; w < arrayLength; w++)
                                {
                                    if (a == w && b == lineAfter)
                                    {
                                        Console.WriteLine("got here1");
                                        //lineComp2 = lineComp2 + (grid[w, lineAfter] == "." ? "#" : ".");
                                        lineComp2 = lineComp2 + grid[w, lineAfter];
                                    }
                                    else
                                    {
                                        lineComp2 = lineComp2 + grid[w, lineAfter];
                                    }
                                }


                                //Console.WriteLine("Checking lines " + lineBefore + " and " + lineAfter);
                                //Console.WriteLine(arrayWidth);
                                //Console.WriteLine(lineComp1);
                                //Console.WriteLine(lineComp2);
                                //Console.WriteLine("Checking linesz ");
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
                            if (isReflection == true)
                            {
                                reflectionRows = x + 1;
                                verticalReflectionColumnsLeftSecond += reflectionRows;
                                Console.WriteLine("Vertical reflection at line " + x + " with row count of " + reflectionRows + " with a max rows of " + arrayLength);
                                break;
                            }

                        }
                        if (isReflection == true)
                        {
                            //break;
                        }
                    }
                }
                


            }

            int finalCaltSecond = verticalReflectionColumnsLeftSecond + (horizontalReflectionRowsAboveSecond * 100);
            output += "\nPart B: " + finalCaltSecond;





            return;
            /*for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    grid[x, y] = ".";
                }
            }*/







































            //output += "Part B:" + totalLoad;

            // too high 34327
            // too high 34326
            // too high 34267
            // wrong 34186
            // wrong 31735
            // wrong 27677
            // 33675
            // 33735
            // 33666



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


