using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks.Sources;
using static AoC2023.solution.AoCDay7;
using static ABI.System.Collections.Generic.IReadOnlyDictionary_Delegates;
using System.ComponentModel;
using System.Collections.Generic;
using static AoC2023.solution.AoCDay8;

namespace AoC2023.solution
{
    public class AoCDay8
    {

        public AoCDay8(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            List<char> navigationList = new List<char>();
            List<string> startingNodeList = new List<string>();
            int stepsRequired = 0;
            int stepsRequiredSecond = 0;

            foreach (char navigation in lines[0])
            {
                navigationList.Add(navigation);
            }
            //List<Node> nodesList = new List<Node>();
            //List<Node> nodesList = new List<Node>();
            Dictionary<string, Node> nodesList = new Dictionary<string, Node>();
            //List<Ghost> ghostList = new List<Ghost>();
            Dictionary<int, string> ghostList = new Dictionary<int, string>();
            int ghostID = 0;
            foreach (string line in lines)
            {

                if (line == "") continue;
                string[] nodeDetails = line.Split(" = ");
                if (nodeDetails.Length < 2) continue;
                string[] nodeDirections = nodeDetails[1].Split(", ");
                string leftNode = nodeDirections[0].Replace("(", "");
                string rightNode = nodeDirections[1].Replace(")", "");

                if (nodeDetails[0][2] == 'A')
                {
                    //startingNodeList.Add(nodeDetails[0]);

                    ghostList.Add(ghostID,nodeDetails[0]);
                    ghostID++;
                }

                nodesList.Add(nodeDetails[0], new Node()
                {
                    nodeName = nodeDetails[0],
                    leftNode = leftNode,
                    rightNode = rightNode
                });



            }

            string currentLocation = "AAA";
            int locationIterator = 0;
            /*
            while(currentLocation != "ZZZ")
            {
                int navID = locationIterator % navigationList.Count();
                if (navigationList[navID] == 'L')
                {
                    currentLocation = nodesList.Where(p => p.nodeName == currentLocation).First().leftNode;
                    Console.WriteLine("Moving left to location " + currentLocation + "\n");
                } else
                {
                    currentLocation = nodesList.Where(p => p.nodeName == currentLocation).First().rightNode;
                    Console.WriteLine("Moving right to location " + currentLocation + "\n");
                }
                stepsRequired++;
                locationIterator++;
            }
            */

            output = "Part A: " + stepsRequired;

            /*foreach (Ghost seedItem in ghostList)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(seedItem))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(seedItem);
                    Console.WriteLine("{0}={1}", name, value);
                }
            }*/

            //startingNodeList.ForEach(Console.WriteLine);


            Dictionary<string, int> initialFound = new Dictionary<string, int>();

            while (areWeFinished(ghostList) == false)
            {
                // Get the fist time I make it to Z
                
                int navID = locationIterator % navigationList.Count();
                if (navigationList[navID] == 'L')
                {
                    foreach (KeyValuePair<int, string> ghost in ghostList)
                    {
                        if (ghost.Value[2] == 'Z' && !initialFound.ContainsKey(ghost.Value))
                        {
                            initialFound.Add(ghost.Value, stepsRequiredSecond);
                        }
                        //ghostList[ghost.Key] = nodesList.Single(p => p.nodeName == ghost.Value).leftNode;
                        ghostList[ghost.Key] = nodesList[ghost.Value].leftNode;
                        //Console.WriteLine("Moving left to location " + ghost.currentNode + "\n");
                    }
                    
                }
                else
                {
                    foreach (KeyValuePair<int, string> ghost in ghostList)
                    {
                        if (ghost.Value[2] == 'Z' && !initialFound.ContainsKey(ghost.Value))
                        {
                            initialFound.Add(ghost.Value, stepsRequiredSecond);
                        }
                        //ghostList[ghost.Key] = nodesList.Single(p => p.nodeName == ghost.Value).rightNode;
                        ghostList[ghost.Key] = nodesList[ghost.Value].rightNode;
                        //Console.WriteLine("Moving left to location " + ghost.currentNode + "\n");
                    }
                }
                stepsRequiredSecond++;
                locationIterator++;
                if(stepsRequiredSecond % 10000000 == 0)
                {
                    Console.WriteLine("Completed " + stepsRequiredSecond + " steps\n");
                }
                if(ghostList.Count == initialFound.Count)
                {
                    Console.WriteLine("All done one loop! " + stepsRequiredSecond + " steps\n");
                    Console.Write(lcm_of_array_elements(initialFound.Values.ToArray()));
                    break;
                }
            }
            

            output += "\nPart B: " + stepsRequiredSecond;

            bool areWeFinished(Dictionary<int, string> ghostList)
            {
                foreach (KeyValuePair<int, string> ghost in ghostList)
                {
                    while (ghost.Value[2] != 'Z')
                    {
                        return false;
                    }
                }
                return true;
            }

            /*
            foreach (Node seedItem in nodesList)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(seedItem))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(seedItem);
                    Console.WriteLine("{0}={1}", name, value);
                }
            }

            navigationList.ForEach(Console.WriteLine);*/

            /*
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
            */
        }

        public class Node
        {
            public string nodeName { get; set; }
            public string leftNode { get; set; }
            public string rightNode { get; set; }

        }

        public class Ghost
        {
            public int ghostID { get; set; }
            public string currentNode { get; set; }


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

        public static long lcm_of_array_elements(int[] element_array)
        {
            long lcm_of_array_elements = 1;
            int divisor = 2;

            while (true)
            {

                int counter = 0;
                bool divisible = false;
                for (int i = 0; i < element_array.Length; i++)
                {

                    // lcm_of_array_elements (n1, n2, ... 0) = 0.
                    // For negative number we convert into
                    // positive and calculate lcm_of_array_elements.
                    if (element_array[i] == 0)
                    {
                        return 0;
                    }
                    else if (element_array[i] < 0)
                    {
                        element_array[i] = element_array[i] * (-1);
                    }
                    if (element_array[i] == 1)
                    {
                        counter++;
                    }

                    // Divide element_array by devisor if complete
                    // division i.e. without remainder then replace
                    // number with quotient; used for find next factor
                    if (element_array[i] % divisor == 0)
                    {
                        divisible = true;
                        element_array[i] = element_array[i] / divisor;
                    }
                }

                // If divisor able to completely divide any number
                // from array multiply with lcm_of_array_elements
                // and store into lcm_of_array_elements and continue
                // to same divisor for next factor finding.
                // else increment divisor
                if (divisible)
                {
                    lcm_of_array_elements = lcm_of_array_elements * divisor;
                }
                else
                {
                    divisor++;
                }

                // Check if all element_array is 1 indicate 
                // we found all factors and terminate while loop.
                if (counter == element_array.Length)
                {
                    return lcm_of_array_elements;
                }
            }
        }

        public string output;
    }
}