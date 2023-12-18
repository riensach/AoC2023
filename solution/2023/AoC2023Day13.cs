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
            int horizontalReflectionRowsAboveTemp = 0;
            int verticalReflectionColumnsLeft = 0;
            int verticalReflectionColumnsLeftTemp = 0;
            foreach (string data in grids)
            {
                string[] lines = data.Split("\n");
                lines[0] = lines[0].Replace("\r", "").Replace("\n", "");


                verticalReflectionColumnsLeftTemp = 0;
                horizontalReflectionRowsAboveTemp = 0;
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
                for (int x = 0; x < arrayLength; x++)
                {
                    isReflection = true;
                    int z = 1;
                    int reflectionRows = 0;
                    while (isReflection == true)
                    {
                        
                        // Check for reflection horizontal
                        if ((x - z + 1 < 0) || (x + z >= arrayLength))
                        {
                            //Console.WriteLine("breaking");
                            break;
                        }
                        isReflection = false;
                        int lineBefore = (x - z) + 1;
                        int lineAfter = x + z;
                        //string lineComp1 = lines[lineBefore].Replace("\r","").Replace("\n", "");
                        //string lineComp2 = lines[lineAfter].Replace("\r", "").Replace("\n", "");
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
                    if (isReflection == true && reflectionRows > 0 && (x + z == arrayLength))
                    {
                        reflectionRows = x + 1;
                        horizontalReflectionRowsAbove += reflectionRows;
                        //horizontalReflectionRowsAboveTemp = 1;


                        //if (reflectionRows > horizontalReflectionRowsAboveTemp && ((x - z + 1 == 0) || (x + z == arrayLength - 1)))
                        if ((x - z + 1 == 0) || (x + z == arrayLength))
                        {
                            Console.WriteLine("Horizontal reflection at line " + x + " with row count of " + reflectionRows + " with a max rows of " + arrayLength);
                            //horizontalReflectionRowsAboveTemp = reflectionRows;
                            //horizontalReflectionRowsAbove += reflectionRows;

                            //Console.WriteLine(horizontalReflectionRowsAboveTemp);
                        }
                    }

                }



                if(horizontalReflectionRowsAboveTemp > 0)
                {
                    //continue;
                }

                //Vertical now. Rotating the grid first


                isReflection = false;
                for (int x = 0; x < arrayWidth; x++)
                {
                    isReflection = true;
                    int z = 1;
                    int reflectionRows = 0;
                    while (isReflection == true)
                    {

                        // Check for reflection horizontal
                        if ((x - z + 1 < 0) || (x + z >= arrayWidth))
                        {
                            //Console.WriteLine("breaking");
                            break;
                        }
                        isReflection = false;
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
                    if (isReflection == true && reflectionRows > 0 && (x + z == arrayWidth))
                    {
                        reflectionRows = x + 1;
                        //reflectionRows = arrayLength - x;
                        
                        verticalReflectionColumnsLeft += reflectionRows;
                        //if (reflectionRows > verticalReflectionColumnsLeftTemp && ((x - z + 1 == 0) || (x + z == arrayWidth - 1)))
                        if ((x - z + 1 == 0) || (x + z == arrayWidth))
                        {
                            Console.WriteLine("Vertical reflection at line " + x + " with row count of " + reflectionRows + " with a max rows of " + arrayLength);
                            //verticalReflectionColumnsLeftTemp = reflectionRows;
                            //Console.WriteLine(verticalReflectionColumnsLeftTemp);
                            //verticalReflectionColumnsLeft += reflectionRows;
                        }
                    }

                }
                //horizontalReflectionRowsAbove += horizontalReflectionRowsAboveTemp;
                //verticalReflectionColumnsLeft += verticalReflectionColumnsLeftTemp;
                Console.WriteLine("MOVIN ON");
                Console.WriteLine(horizontalReflectionRowsAbove);
                Console.WriteLine(verticalReflectionColumnsLeft);


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
            // wrong 31735
            // wrong 27677
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


