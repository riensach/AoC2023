using System;
using System.IO;
using System.Diagnostics;
using LoreSoft.MathExpressions;
using System.Threading;

namespace AoC2022.solution
{
    public class AoCDay18
    {
        public List<string> cubes = new List<string>();
        public int maximumX = 0;
        public int maximumY = 0;
        public int maximumZ = 0;
        public int minimumX = 1000;
        public int minimumY = 1000;
        public int minimumZ = 1000;
        public AoCDay18(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int exposedSurfaces = 0;          

            foreach (string line in lines)
            {
                string[] parts = line.Split(",");
                int cubeX = int.Parse(parts[0]);
                int cubeY = int.Parse(parts[1]);
                int cubeZ = int.Parse(parts[2]);
                cubes.Add(cubeX + "," + cubeY + "," + cubeZ);
                if(cubeX > maximumX)
                {
                    maximumX = cubeX;
                }
                if (cubeY > maximumY)
                {
                    maximumY = cubeY;
                }
                if (cubeZ > maximumZ)
                {
                    maximumZ = cubeZ;
                }
                if (cubeX < minimumX)
                {
                    minimumX = cubeX;
                }
                if (cubeY < minimumY)
                {
                    minimumY = cubeY;
                }
                if (cubeZ < minimumZ)
                {
                    minimumZ = cubeZ;
                }
            }
            Console.WriteLine(maximumX + "," + maximumY + "," + maximumZ + " : " + minimumX + "," + minimumY + "," + minimumZ);

            foreach (string cube in cubes)
            {
                string[] cubeParts = cube.Split(",");
                int cubeX = int.Parse(cubeParts[0]);
                int cubeY = int.Parse(cubeParts[1]);
                int cubeZ = int.Parse(cubeParts[2]);
                int sidesExposed = exposedSides(cubeX, cubeY, cubeZ);

                if (sidesExposed > 0)
                {
                    exposedSurfaces = exposedSurfaces + sidesExposed;
                }
            }

            output = "Part A: " + exposedSurfaces;

            int exposedExternalSurfaces = 0;
            int iterator = 0;
            foreach (string cube in cubes)
            {
                string[] cubeParts = cube.Split(",");
                int cubeX = int.Parse(cubeParts[0]);
                int cubeY = int.Parse(cubeParts[1]);
                int cubeZ = int.Parse(cubeParts[2]);
                Console.WriteLine("Checking cube for exposed sides iterator " + iterator);
                int sidesExposed = exposedSidesNotTrapped(cubeX, cubeY, cubeZ);

                if (sidesExposed > 0)
                {
                    exposedExternalSurfaces = exposedExternalSurfaces + sidesExposed;
                }
                iterator++;
            }

            output += "\nPart B: " + exposedExternalSurfaces;

            // 3292 = too high


        }
        public int exposedSidesNotTrapped(int x, int y, int z)
        {
            int sidesExposed = 0;
            int cubeX = x;
            int cubeY = y;
            int cubeZ = z;
            bool exposedAbove = true;
            bool exposedBelow = true;
            bool exposedLeft = true;
            bool exposedRight = true;
            bool exposedFront = true;
            bool exposedBack = true;
            foreach (string cubeCompare in cubes)
            {

                string[] cubeCompareParts = cubeCompare.Split(",");
                int cubeCompareX = int.Parse(cubeCompareParts[0]);
                int cubeCompareY = int.Parse(cubeCompareParts[1]);
                int cubeCompareZ = int.Parse(cubeCompareParts[2]);

                // Check exposed front
                if (cubeX == cubeCompareX && cubeY == cubeCompareY && cubeZ - 1 == cubeCompareZ)
                {
                    exposedFront = false;
                }
                // Check exposed back
                if (cubeX == cubeCompareX && cubeY == cubeCompareY && cubeZ + 1 == cubeCompareZ)
                {
                    exposedBack = false;
                }
                // Check exposed Left
                if (cubeX == cubeCompareX && cubeY - 1 == cubeCompareY && cubeZ == cubeCompareZ)
                {
                    exposedLeft = false;
                }
                // Check exposed Right
                if (cubeX == cubeCompareX && cubeY + 1 == cubeCompareY && cubeZ == cubeCompareZ)
                {
                    exposedRight = false;
                }
                // Check exposed above
                if (cubeX - 1 == cubeCompareX && cubeY == cubeCompareY && cubeZ == cubeCompareZ)
                {
                    exposedAbove = false;
                }
                // Check exposed below
                if (cubeX + 1 == cubeCompareX && cubeY == cubeCompareY && cubeZ == cubeCompareZ)
                {
                    exposedBelow = false;
                }
            }
            if (exposedAbove == true)
            {
                bool exposed = canWaterGetHere(cubeX - 1, cubeY, cubeZ);
                if(exposed)
                {
                    sidesExposed++;
                }
                
            }
            if (exposedBelow == true)
            {
                bool exposed = canWaterGetHere(cubeX + 1, cubeY, cubeZ);
                if (exposed)
                {
                    sidesExposed++;
                }
            }
            if (exposedLeft == true)
            {
                bool exposed = canWaterGetHere(cubeX, cubeY - 1, cubeZ);
                if (exposed)
                {
                    sidesExposed++;
                }
            }
            if (exposedRight == true)
            {
                bool exposed = canWaterGetHere(cubeX, cubeY + 1, cubeZ);
                if (exposed)
                {
                    sidesExposed++;
                }
            }
            if (exposedFront == true)
            {
                bool exposed = canWaterGetHere(cubeX, cubeY, cubeZ - 1);
                if (exposed)
                {
                    sidesExposed++;
                }
            }
            if (exposedBack == true)
            {
                bool exposed = canWaterGetHere(cubeX, cubeY, cubeZ + 1);
                if (exposed)
                {
                    sidesExposed++;
                }
            }
            return sidesExposed;
        }
        public bool canWaterGetHere(int x, int y, int z)
        {
            string startingQueue = x+","+y+","+z;
            List<string> cubesChecked = new List<string>();
            cubesChecked.Add(startingQueue);
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(startingQueue);
            do
            {
                string currentItem = queue.Dequeue();
                string[] cubeParts = currentItem.Split(",");
                int cubeX = int.Parse(cubeParts[0]);
                int cubeY = int.Parse(cubeParts[1]);
                int cubeZ = int.Parse(cubeParts[2]);
                bool exposedAbove = true;
                bool exposedBelow = true;
                bool exposedLeft = true;
                bool exposedRight = true;
                bool exposedFront = true;
                bool exposedBack = true;
                foreach (string cubeCompare in cubes)
                {
                    string[] cubeCompareParts = cubeCompare.Split(",");
                    int cubeCompareX = int.Parse(cubeCompareParts[0]);
                    int cubeCompareY = int.Parse(cubeCompareParts[1]);
                    int cubeCompareZ = int.Parse(cubeCompareParts[2]);

                    // Check exposed front
                    if (cubeX == cubeCompareX && cubeY == cubeCompareY && cubeZ - 1 == cubeCompareZ)
                    {
                        exposedFront = false;
                    }
                    // Check exposed back
                    if (cubeX == cubeCompareX && cubeY == cubeCompareY && cubeZ + 1 == cubeCompareZ)
                    {
                        exposedBack = false;
                    }
                    // Check exposed Left
                    if (cubeX == cubeCompareX && cubeY - 1 == cubeCompareY && cubeZ == cubeCompareZ)
                    {
                        exposedLeft = false;
                    }
                    // Check exposed Right
                    if (cubeX == cubeCompareX && cubeY + 1 == cubeCompareY && cubeZ == cubeCompareZ)
                    {
                        exposedRight = false;
                    }
                    // Check exposed above
                    if (cubeX - 1 == cubeCompareX && cubeY == cubeCompareY && cubeZ == cubeCompareZ)
                    {
                        exposedAbove = false;
                    }
                    // Check exposed below
                    if (cubeX + 1 == cubeCompareX && cubeY == cubeCompareY && cubeZ == cubeCompareZ)
                    {
                        exposedBelow = false;
                    }
                }
                if (exposedAbove == true && (cubeX - 1) > minimumX)
                {
                    string newCubeToCheck = (cubeX - 1) + "," + cubeY + "," + cubeZ;
                    if (!cubesChecked.Contains(newCubeToCheck))
                    {
                        queue.Enqueue(newCubeToCheck);
                        cubesChecked.Add(newCubeToCheck);
                    }
                } else if (exposedAbove == true)
                {
                    // Water can get here
                    return true;
                }
                if (exposedBelow == true && (cubeX + 1) < maximumX)
                {
                    string newCubeToCheck = (cubeX + 1) + "," + cubeY + "," + cubeZ;
                    if (!cubesChecked.Contains(newCubeToCheck))
                    {
                        queue.Enqueue(newCubeToCheck);
                        cubesChecked.Add(newCubeToCheck);
                    }
                }
                else if (exposedBelow == true)
                {
                    // Water can get here
                    return true;
                }
                if (exposedLeft == true && (cubeY - 1) > minimumY)
                {
                    string newCubeToCheck = cubeX + "," + (cubeY -1) + "," + cubeZ;
                    if (!cubesChecked.Contains(newCubeToCheck))
                    {
                        queue.Enqueue(newCubeToCheck);
                        cubesChecked.Add(newCubeToCheck);
                    }
                }
                else if (exposedLeft == true)
                {
                    // Water can get here
                    return true;
                }
                if (exposedRight == true && (cubeY + 1) < maximumY)
                {
                    string newCubeToCheck = cubeX + "," + (cubeY + 1) + "," + cubeZ;
                    if (!cubesChecked.Contains(newCubeToCheck))
                    {
                        queue.Enqueue(newCubeToCheck);
                        cubesChecked.Add(newCubeToCheck);
                    }
                }
                else if (exposedRight == true)
                {
                    // Water can get here
                    return true;
                }
                if (exposedFront == true && (cubeZ - 1) > minimumZ)
                {
                    string newCubeToCheck = cubeX + "," + cubeY + "," + (cubeZ - 1);
                    if (!cubesChecked.Contains(newCubeToCheck))
                    {
                        queue.Enqueue(newCubeToCheck);
                        cubesChecked.Add(newCubeToCheck);
                    }
                }
                else if (exposedFront == true)
                {
                    // Water can get here
                    return true;
                }
                if (exposedBack == true && (cubeZ + 1) < maximumZ)
                {
                    string newCubeToCheck = cubeX + "," + cubeY + "," + (cubeZ + 1);
                    if (!cubesChecked.Contains(newCubeToCheck))
                    {
                        queue.Enqueue(newCubeToCheck);
                        cubesChecked.Add(newCubeToCheck);
                    }
                }
                else if (exposedBack == true)
                {
                    // Water can get here
                    return true;
                }
                //Console.WriteLine(queue.Count);
            } while (queue.Count > 0);

            return false;
        }
            public int exposedSides(int x, int y, int z)
        {
            int sidesExposed = 0;
            int cubeX = x;
            int cubeY = y;
            int cubeZ = z;
            bool exposedAbove = true;
            bool exposedBelow = true;
            bool exposedLeft = true;
            bool exposedRight = true;
            bool exposedFront = true;
            bool exposedBack = true;
            foreach (string cubeCompare in cubes)
            {

                string[] cubeCompareParts = cubeCompare.Split(",");
                int cubeCompareX = int.Parse(cubeCompareParts[0]);
                int cubeCompareY = int.Parse(cubeCompareParts[1]);
                int cubeCompareZ = int.Parse(cubeCompareParts[2]);

                // Check exposed front
                if (cubeX == cubeCompareX && cubeY == cubeCompareY && cubeZ - 1 == cubeCompareZ)
                {
                    exposedFront = false;
                }
                // Check exposed back
                if (cubeX == cubeCompareX && cubeY == cubeCompareY && cubeZ + 1 == cubeCompareZ)
                {
                    exposedBack = false;
                }
                // Check exposed Left
                if (cubeX == cubeCompareX && cubeY - 1 == cubeCompareY && cubeZ == cubeCompareZ)
                {
                    exposedLeft = false;
                }
                // Check exposed Right
                if (cubeX == cubeCompareX && cubeY + 1 == cubeCompareY && cubeZ == cubeCompareZ)
                {
                    exposedRight = false;
                }
                // Check exposed above
                if (cubeX - 1 == cubeCompareX && cubeY == cubeCompareY && cubeZ == cubeCompareZ)
                {
                    exposedAbove = false;
                }
                // Check exposed below
                if (cubeX + 1 == cubeCompareX && cubeY == cubeCompareY && cubeZ == cubeCompareZ)
                {
                    exposedBelow = false;
                }
            }
            if (exposedAbove == true)
            {
                sidesExposed++;
            }
            if (exposedBelow == true)
            {
                sidesExposed++;
            }
            if (exposedLeft == true)
            {
                sidesExposed++;
            }
            if (exposedRight == true)
            {
                sidesExposed++;
            }
            if (exposedFront == true)
            {
                sidesExposed++;
            }
            if (exposedBack == true)
            {
                sidesExposed++;
            }

            return sidesExposed;
        }

        public string output;
    }
}