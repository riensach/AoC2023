using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks.Sources;

namespace AoC2022.solution
{
    public class AoCDay8
    {

        public AoCDay8(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            int arrayLength = lines.Count();
            int arrayWidth = lines[0].Length;

            int[,] grid = new int[arrayLength, arrayWidth];
            List<int> scoresList = new List<int>();
            int row = 0;
            int column = 0;

            foreach (string line in lines)
            {
                foreach (var character in line)
                {
                    grid[row, column] = Int32.Parse(character.ToString());
                    column++;
                }
                row++;
                column = 0;
            }

            int visibleTrees = arrayLength + arrayLength + arrayWidth + arrayWidth - 4;
            bool visible = false;
            int score = 0;

            for (int i = 1; i < arrayLength - 1; i++)
            {
                for (int x = 1; x < arrayWidth - 1; x++)
                {

                    
                    visible = visibleTree(grid, i, x, arrayLength, arrayWidth);
                    int treeValue = grid[i, x];
                    //System.Console.WriteLine("checking tree at " + i + ","+ x+" with a value of "+ treeValue);

                    if (visible == true)
                    {

                       // System.Console.WriteLine(" - marked as visible \n");
                        visibleTrees++;
                    }
                    score = scenicScore(grid, i, x, arrayLength, arrayWidth);
                    scoresList.Add(score);

                }

                
            }
            scoresList.Sort();
            int highestScore = scoresList.Last();

            output = "Part A: " + visibleTrees;
            output += "\nPart B: " + highestScore;
        }

        public bool visibleTree(int[,] grid, int i, int x, int arrayLength, int arrayWidth)
        {
            int treeValue = grid[i, x];
            bool visibleLeft = true;
            bool visibleRight = true;
            bool visibleTop = true;
            bool visibleBottom = true;
            int treeValueTemp;


            for (int z = x + 1; z < arrayWidth; z++)
            {
                treeValueTemp = grid[i, z];
                if (treeValueTemp >= treeValue)
                {
                    visibleLeft = false;
                    break;
                }
            }

            for (int z = x - 1; z > -1; z--)
            {
                treeValueTemp = grid[i, z];
                if (treeValueTemp >= treeValue)
                {
                    visibleRight = false;
                    break;
                }
            }

            for (int z = i + 1; z < arrayLength; z++)
            {
                treeValueTemp = grid[z, x];
                if (treeValueTemp >= treeValue)
                {
                    visibleTop = false;
                    break;
                }
            }

            for (int z = i - 1; z > -1; z--)
            {
                treeValueTemp = grid[z, x];
                if (treeValueTemp >= treeValue)
                {
                    visibleBottom = false;
                    break;
                }
            }





            if (visibleLeft == true || visibleRight == true || visibleTop == true || visibleBottom == true)
            {
                //System.Console.WriteLine(visibleLeft + " - " + visibleRight + " - " + visibleTop + " - " + visibleBottom);
                return true;
            }
            return false;


        }



        public int scenicScore(int[,] grid, int i, int x, int arrayLength, int arrayWidth)
        {
            int treeValue = grid[i, x];
            int treeValueTemp;
            int scoreLeft = 0;
            int scoreRight = 0;
            int scoreTop = 0;
            int scoreBottom = 0;


            for (int z = x + 1; z < arrayWidth; z++)
            {
                treeValueTemp = grid[i, z];
                scoreLeft++;
                if (treeValueTemp >= treeValue)
                {
                    break;
                }
            }

            for (int z = x - 1; z > -1; z--)
            {
                treeValueTemp = grid[i, z];
                scoreRight++;
                if (treeValueTemp >= treeValue)
                {
                    break;
                }
            }

            for (int z = i + 1; z < arrayLength; z++)
            {
                treeValueTemp = grid[z, x];
                scoreTop++;
                if (treeValueTemp >= treeValue)
                {
                    break;
                }
            }

            for (int z = i - 1; z > -1; z--)
            {
                treeValueTemp = grid[z, x];
                scoreBottom++;
                if (treeValueTemp >= treeValue)
                {
                    break;
                }
            }

            int score = scoreLeft * scoreRight * scoreTop * scoreBottom;



            return score;


        }




        public string output;
    }
}