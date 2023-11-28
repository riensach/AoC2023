using System;
using System.IO;
using System.Diagnostics;

namespace AoC2022.solution
{
    public class AoCDay23
    {
        public string[,] grid;

        public IDictionary<string, string> gridValues = new Dictionary<string, string>();
        public List<Elf> elfList = new List<Elf>();
        public AoCDay23(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int arrayLength = lines.Count();
            int arrayWidth = lines[0].Count();
            Console.WriteLine(arrayLength + "," + arrayWidth);
            grid = new string[arrayLength, arrayWidth];
            createGrid(arrayLength, arrayWidth);
            int iteratorX = 0;
            int iteratorY = 0;
            int startingX = 0;
            int startingY = 0;
            int elfCount = 0;
            Elf elf;
            foreach (string line in lines)
            {
                foreach (var character in line)
                {

                    //gridValues[iteratorX + "," + iteratorY] = character.ToString();
                    if(character.ToString() == "#")
                    {
                        elf = new Elf(iteratorX, iteratorY);
                        elfList.Add(elf);
                        elfCount++;
                    }


                    iteratorY++;
                }

                iteratorX++;
                iteratorY = 0;
            }
            //output = printGrid(arrayLength, arrayWidth);
            int currentFacing = 0;
            int currentX = 0;
            int currentY = 0;
            int elfPos1 = 0;
            int elfPos2 = 0;
            int elfPos3 = 0;
            int elfPos4 = 0;
            int elfPos5 = 0;
            int elfPos6 = 0;
            int elfPos7 = 0;
            int elfPos8 = 0;
            int elfPos9 = 0;
            int elfPos10 = 0;
            int elfPos11 = 0;
            int elfPos12 = 0;


            string newCoord = 2+ "," + 4;
            //elfPos1 = (elfList.BinarySearch(newCoord, new ValueComparer()) ? 1 : 0);
            //console.WriteLine("Was elf found:"+ elfPos1);

            output += printGrid(arrayLength, arrayWidth, elfList);
            for (int i = 0; i < 100000000; i++)
            {
                foreach(Elf elfObj in elfList)
                {
                    currentX = elfObj.xCoord;
                    currentY = elfObj.yCoord;
                    elfPos1 = (elfList.Exists(x => x.xCoord == currentX - 1 && x.yCoord == currentY - 1) ? 1 : 0);
                    elfPos2 = (elfList.Exists(x => x.xCoord == currentX - 1 && x.yCoord == currentY) ? 1 : 0);
                    elfPos3 = (elfList.Exists(x => x.xCoord == currentX - 1 && x.yCoord == currentY + 1) ? 1 : 0);
                    elfPos4 = (elfList.Exists(x => x.xCoord == currentX + 1 && x.yCoord == currentY - 1) ? 1 : 0);
                    elfPos5 = (elfList.Exists(x => x.xCoord == currentX + 1 && x.yCoord == currentY) ? 1 : 0);
                    elfPos6 = (elfList.Exists(x => x.xCoord == currentX + 1 && x.yCoord == currentY + 1) ? 1 : 0);
                    elfPos7 = (elfList.Exists(x => x.xCoord == currentX - 1 && x.yCoord == currentY - 1) ? 1 : 0);
                    elfPos8 = (elfList.Exists(x => x.xCoord == currentX && x.yCoord == currentY - 1) ? 1 : 0);
                    elfPos9 = (elfList.Exists(x => x.xCoord == currentX + 1 && x.yCoord == currentY - 1) ? 1 : 0);
                    elfPos10 = (elfList.Exists(x => x.xCoord == currentX - 1 && x.yCoord == currentY + 1) ? 1 : 0);
                    elfPos11 = (elfList.Exists(x => x.xCoord == currentX && x.yCoord == currentY + 1) ? 1 : 0);
                    elfPos12 = (elfList.Exists(x => x.xCoord == currentX + 1 && x.yCoord == currentY + 1) ? 1 : 0);
                    if (elfPos1 == 0 && elfPos2 == 0 && elfPos3 == 0 && elfPos4 == 0 && elfPos5 == 0 && elfPos6 == 0 && elfPos7 == 0 && elfPos8 == 0 && elfPos9 == 0 && elfPos10 == 0 && elfPos11 == 0 && elfPos12 == 0)
                    {
                        // No elves near by
                        continue;
                    }
                        //Console.WriteLine(elfPos1 + " :: " + elfPos2 + " :: " + elfPos3 + " :: " + elfPos4 + " :: " + elfPos5+ " :: " + elfPos6 + " :: " + elfPos7 + " :: " + elfPos8 + " :: " + elfPos9 + " :: " + elfPos10 + " :: " + elfPos11 + " :: " + elfPos12);
                    if (currentFacing == 0)
                    {
                        // Consider north first                     
                        if(elfPos1 == 0 && elfPos2 == 0 && elfPos3 == 0)
                        {
                            // No north elf
                            elfObj.xCoordProposed = elfObj.xCoord - 1;
                            elfObj.yCoordProposed = elfObj.yCoord;
                            continue;
                        }
                        if (elfPos4 == 0 && elfPos5 == 0 && elfPos6 == 0)
                        {
                            // No south elf
                            elfObj.xCoordProposed = elfObj.xCoord + 1;
                            elfObj.yCoordProposed = elfObj.yCoord;
                            continue;
                        }
                        if (elfPos7 == 0 && elfPos8 == 0 && elfPos9 == 0)
                        {
                            // No west elf
                            elfObj.xCoordProposed = elfObj.xCoord;
                            elfObj.yCoordProposed = elfObj.yCoord - 1;
                            continue;
                        }
                        if (elfPos10 == 0 && elfPos11 == 0 && elfPos12 == 0)
                        {
                            // No east elf
                            elfObj.xCoordProposed = elfObj.xCoord;
                            elfObj.yCoordProposed = elfObj.yCoord + 1;
                            continue;
                        }

                    } else if (currentFacing == 1)
                    {
                        // Consider south first
                        if (elfPos4 == 0 && elfPos5 == 0 && elfPos6 == 0)
                        {
                            // No south elf
                            elfObj.xCoordProposed = elfObj.xCoord + 1;
                            elfObj.yCoordProposed = elfObj.yCoord;
                            continue;
                        }
                        if (elfPos7 == 0 && elfPos8 == 0 && elfPos9 == 0)
                        {
                            // No west elf
                            elfObj.xCoordProposed = elfObj.xCoord;
                            elfObj.yCoordProposed = elfObj.yCoord - 1;
                            continue;
                        }
                        if (elfPos10 == 0 && elfPos11 == 0 && elfPos12 == 0)
                        {
                            // No east elf
                            elfObj.xCoordProposed = elfObj.xCoord;
                            elfObj.yCoordProposed = elfObj.yCoord + 1;
                            continue;
                        }
                        if (elfPos1 == 0 && elfPos2 == 0 && elfPos3 == 0)
                        {
                            // No north elf
                            elfObj.xCoordProposed = elfObj.xCoord - 1;
                            elfObj.yCoordProposed = elfObj.yCoord;
                            continue;
                        }
                    }
                    else if (currentFacing == 2)
                    {
                        // Consider west first
                        if (elfPos7 == 0 && elfPos8 == 0 && elfPos9 == 0)
                        {
                            // No west elf
                            elfObj.xCoordProposed = elfObj.xCoord;
                            elfObj.yCoordProposed = elfObj.yCoord - 1;
                            continue;
                        }
                        if (elfPos10 == 0 && elfPos11 == 0 && elfPos12 == 0)
                        {
                            // No east elf
                            elfObj.xCoordProposed = elfObj.xCoord;
                            elfObj.yCoordProposed = elfObj.yCoord + 1;
                            continue;
                        }
                        if (elfPos1 == 0 && elfPos2 == 0 && elfPos3 == 0)
                        {
                            // No north elf
                            elfObj.xCoordProposed = elfObj.xCoord - 1;
                            elfObj.yCoordProposed = elfObj.yCoord;
                            continue;
                        }
                        if (elfPos4 == 0 && elfPos5 == 0 && elfPos6 == 0)
                        {
                            // No south elf
                            elfObj.xCoordProposed = elfObj.xCoord + 1;
                            elfObj.yCoordProposed = elfObj.yCoord;
                            continue;
                        }
                    }
                    else if (currentFacing == 3)
                    {
                        // Consider east first
                        if (elfPos10 == 0 && elfPos11 == 0 && elfPos12 == 0)
                        {
                            // No east elf
                            elfObj.xCoordProposed = elfObj.xCoord;
                            elfObj.yCoordProposed = elfObj.yCoord + 1;
                            continue;
                        }
                        if (elfPos1 == 0 && elfPos2 == 0 && elfPos3 == 0)
                        {
                            // No north elf
                            elfObj.xCoordProposed = elfObj.xCoord - 1;
                            elfObj.yCoordProposed = elfObj.yCoord;
                            continue;
                        }
                        if (elfPos4 == 0 && elfPos5 == 0 && elfPos6 == 0)
                        {
                            // No south elf
                            elfObj.xCoordProposed = elfObj.xCoord + 1;
                            elfObj.yCoordProposed = elfObj.yCoord;
                            continue;
                        }
                        if (elfPos7 == 0 && elfPos8 == 0 && elfPos9 == 0)
                        {
                            // No west elf
                            elfObj.xCoordProposed = elfObj.xCoord;
                            elfObj.yCoordProposed = elfObj.yCoord - 1;
                            continue;
                        }
                    }

                }
                bool elfMoves = false;
                foreach (Elf elfObj in elfList)
                {
                    if(elfObj.xCoordProposed == -1000)
                    {
                        // No movement proposed
                        continue;
                    }
                    int multipleElfProposed = elfList.FindAll(x => x.xCoordProposed == elfObj.xCoordProposed && x.yCoordProposed == elfObj.yCoordProposed).Count();
                    //Console.WriteLine(multipleElfProposed);
                    if(multipleElfProposed > 1)
                    {
                        //Console.WriteLine("Not moving from "+ elfObj.xCoord+", "+ elfObj.yCoord + " to " + elfObj.xCoordProposed + "," + elfObj.yCoordProposed);
                        continue;
                    }
                    elfMoves = true;
                   // Console.WriteLine("Moving elf from "+ elfObj.xCoord+","+ elfObj.yCoord + " to "+ elfObj.xCoordProposed + ","+ elfObj.yCoordProposed);
                    elfObj.xCoord = elfObj.xCoordProposed;
                    elfObj.yCoord = elfObj.yCoordProposed;
                }
                foreach (Elf elfObj in elfList)
                {

                    elfObj.xCoordProposed = -1000;
                    elfObj.yCoordProposed = -1000;
                }
                    //Console.WriteLine();
                    currentFacing++;
                if(currentFacing > 3)
                {
                    currentFacing = 0;
                }

                if(!elfMoves)
                {
                    Console.WriteLine("No elf moved!!!" + i+1);
                    break;
                }
                //output += printGrid(arrayLength, arrayWidth, elfList);
            }
            int minX = 1000;
            int maxX = 0;
            int minY = 1000;
            int maxY = 0;
            foreach (Elf elfObj in elfList)
            {
                if(elfObj.xCoord > maxX)
                {
                    maxX = elfObj.xCoord;
                }
                if(elfObj.xCoord < minX)
                {
                    minX = elfObj.xCoord;
                }
                if(elfObj.yCoord > maxY)
                {
                    maxY = elfObj.yCoord;
                }
                if(elfObj.yCoord < minY)
                {
                    minY = elfObj.yCoord;
                }
            }
            int xPos = (Math.Abs(maxX - minX) + 1);
            int yPox = (Math.Abs(maxY - minY) + 1);
            int totalPositions = (xPos * yPox) - elfCount;

            Console.WriteLine(maxX + " : " + minX + " : " + maxY + " : " + minY + " :: " + elfCount + " :: " + xPos + " :: " + yPox);
            // 4126 = low
            output += "Part A: " + totalPositions;
        }

        public class Elf
        {
            public int xCoord;
            public int yCoord;
            public int xCoordProposed = -10000;
            public int yCoordProposed = -10000;
            public string combinedCoord;
            public Elf(int xCoordInput, int yCoordInput)
            {
                xCoord = xCoordInput;
                yCoord = yCoordInput;
                combinedCoord = xCoordInput+","+yCoordInput;               

            }

        }
        public class ValueComparer : IComparable<Elf>
        {
            public bool Compare(Elf x, Elf y)
            {
                if (x == null) return false;
                if (y == null) return false;

                if (x.combinedCoord == y.combinedCoord) return true;

                //return x.combinedCoord > y.combinedCoord ? 1 : -1;
                return false;
            }

            public int CompareTo(Elf? other)
            {
                throw new NotImplementedException();
            }
        }
        public string printGrid(int xSize, int ySize, List<Elf> elves)
        {
            string output = "\nGrid:\n";
            //string[,] gridCopy = new string[xSize, ySize];
            string[,] gridCopy = grid.Clone() as string[,];
            //grid.CopyTo(gridCopy,0); 

            foreach (Elf elfObj in elves)
            {
                gridCopy[elfObj.xCoord, elfObj.yCoord] = "#";
            }

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    //string toWrite = gridValues[x+","+y];
                    string toWrite = gridCopy[x,y];
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
                    //gridValues.Add(x+","+y,".");
                }
            }
        }

        public string output;
    }
}