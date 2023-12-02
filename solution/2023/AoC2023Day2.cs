using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks.Sources;
using Newtonsoft.Json.Linq;

namespace AoC2023.solution
{
    public class AoCDay2
    {
        public AoCDay2(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );



            List<int> possibleGames = new List<int>();
            int redCubes = 12; int greenCubes = 13; int blueCubes = 14; int idSum = 0;

            foreach (string line in lines)
            {
                int redCubesGame = 0; int greenCubesGame = 0; int blueCubesGame = 0;
                string[] games = line.Split(':');
                string gameName = games[0].Replace("Game ", "");
                string[] sections = games[1].Split(';');
                int possibleGame = 1;
                foreach (string section in sections)
                {
                    string[] cubes = section.Split(',');
                    if (cubes.Count() < 1)
                    {
                        string cubeInfo = section.Trim();
                        string[] cubeDetails = cubeInfo.Split(' ');
                        if (cubeDetails[1] == "green")
                        {
                            greenCubesGame = greenCubesGame + Int32.Parse(cubeDetails[0]);
                            if(Int32.Parse(cubeDetails[0]) > greenCubes)
                            {
                                possibleGame = 0;
                            }
                        }
                        else if (cubeDetails[1] == "blue")
                        {
                            blueCubesGame = blueCubesGame + Int32.Parse(cubeDetails[0]);
                            if (Int32.Parse(cubeDetails[0]) > blueCubes)
                            {
                                possibleGame = 0;
                            }
                        }
                        else if (cubeDetails[1] == "red")
                        {
                            redCubesGame = redCubesGame + Int32.Parse(cubeDetails[0]);
                            if (Int32.Parse(cubeDetails[0]) > redCubes)
                            {
                                possibleGame = 0;
                            }
                        }
                    }
                    else
                    {
                        foreach (string cube in cubes)
                        {
                            string cubeInfo = cube.Trim();
                            string[] cubeDetails = cubeInfo.Split(' ');
                            if (cubeDetails[1] == "green")
                            {
                                greenCubesGame = greenCubesGame + Int32.Parse(cubeDetails[0]);
                                if (Int32.Parse(cubeDetails[0]) > greenCubes)
                                {
                                    possibleGame = 0;
                                }
                            }
                            else if (cubeDetails[1] == "blue")
                            {
                                blueCubesGame = blueCubesGame + Int32.Parse(cubeDetails[0]);
                                if (Int32.Parse(cubeDetails[0]) > blueCubes)
                                {
                                    possibleGame = 0;
                                }
                            }
                            else if (cubeDetails[1] == "red")
                            {
                                redCubesGame = redCubesGame + Int32.Parse(cubeDetails[0]);
                                if (Int32.Parse(cubeDetails[0]) > redCubes)
                                {
                                    possibleGame = 0;
                                }
                            }
                        }
                    }
                }
                if (possibleGame > 0)
                {
                    possibleGames.Add(Int32.Parse(gameName));
                    Console.WriteLine("Adding game " + gameName + "\n");
                }
                //if (redCubesGame <= redCubes && greenCubesGame <= greenCubes && blueCubesGame <= blueCubes)
                //{
                //    possibleGames.Add(Int32.Parse(gameName));
                //}
            }

            foreach (int possibleGame in possibleGames)
            {
                idSum = idSum + possibleGame;
            }

            output = "Part A: " + idSum;





            List<int> possibleGamesSecond = new List<int>();
            //int redCubes = 12; int greenCubes = 13; int blueCubes = 14;int idSum = 0;

            foreach (string line in lines)
            {
                int redCubesGame = 0; int greenCubesGame = 0; int blueCubesGame = 0;
                string[] games = line.Split(':');
                string gameName = games[0].Replace("Game ", "");
                string[] sections = games[1].Split(';');
                foreach (string section in sections)
                {
                    string[] cubes = section.Split(',');
                    if(cubes.Count() < 1)
                    {
                        string cubeInfo = section.Trim();
                        string[] cubeDetails = cubeInfo.Split(' ');
                        if (cubeDetails[1] == "green")
                        {
                            if(Int32.Parse(cubeDetails[0]) > greenCubesGame)
                            {
                                greenCubesGame = Int32.Parse(cubeDetails[0]);
                            }
                            //greenCubesGame = greenCubesGame + Int32.Parse(cubeDetails[0]);
                        }
                        else if (cubeDetails[1] == "blue")
                        {
                            if (Int32.Parse(cubeDetails[0]) > blueCubesGame)
                            {
                                blueCubesGame = Int32.Parse(cubeDetails[0]);
                            }
                            //blueCubesGame = blueCubesGame + Int32.Parse(cubeDetails[0]);
                        }
                        else if (cubeDetails[1] == "red")
                        {
                            if (Int32.Parse(cubeDetails[0]) > redCubesGame)
                            {
                                redCubesGame = Int32.Parse(cubeDetails[0]);
                            }
                            //redCubesGame = redCubesGame + Int32.Parse(cubeDetails[0]);
                        }
                    } else
                    {
                        foreach (string cube in cubes)
                        {
                            string cubeInfo = cube.Trim();
                            string[] cubeDetails = cubeInfo.Split(' ');
                            if (cubeDetails[1]=="green")
                            {
                                if (Int32.Parse(cubeDetails[0]) > greenCubesGame)
                                {
                                    greenCubesGame = Int32.Parse(cubeDetails[0]);
                                }
                                //greenCubesGame = greenCubesGame + Int32.Parse(cubeDetails[0]);
                            } else if (cubeDetails[1] == "blue")
                            {
                                if (Int32.Parse(cubeDetails[0]) > blueCubesGame)
                                {
                                    blueCubesGame = Int32.Parse(cubeDetails[0]);
                                }
                                //blueCubesGame = blueCubesGame + Int32.Parse(cubeDetails[0]);
                            } else if (cubeDetails[1] == "red")
                            {
                                if (Int32.Parse(cubeDetails[0]) > redCubesGame)
                                {
                                    redCubesGame = Int32.Parse(cubeDetails[0]);
                                }
                                //redCubesGame = redCubesGame + Int32.Parse(cubeDetails[0]);
                            }
                        }
                    }
                }
                int gamePower = 0;
                gamePower = greenCubesGame * blueCubesGame * redCubesGame;
                Console.WriteLine("Game power " + gamePower + "\n");

                possibleGamesSecond.Add(gamePower);
                
            }
            idSum = 0;
            foreach (int possibleGame in possibleGamesSecond)
            {
                idSum = idSum + possibleGame;
            }

            output += "\nPart B: " + idSum;


            



        }

        public string output;
    }
}