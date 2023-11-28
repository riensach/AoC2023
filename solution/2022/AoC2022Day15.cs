using System;
using System.IO;
using System.Diagnostics;
using System.Data.SqlTypes;
using System.Collections.Specialized;
using System.Numerics;

namespace AoC2022.solution
{
    
    public class AoCDay15
    {
        public string[,] grid;
        public List<string> sensorCoordsArray = new List<string>();
        public List<string> beaconCoordsArray = new List<string>();
        public List<string> beaconCoordsSensorArray = new List<string>();
        public List<string> gridArray = new List<string>();
        public List<string> specialSensorLocation = new List<string>();
        public AoCDay15(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int highestX = 0;
            int lowestX = 2000000;
            int y = 2000000;

            foreach (string line in lines)
            {
                string inputString = line.Replace("Sensor at x=", "");
                inputString = inputString.Replace(" closest beacon is at x=", "");
                inputString = inputString.Replace(" y=", "");
                string[] items = inputString.Split(":");
                string[] sensorCoords = items[0].Split(",");
                string[] beaconCoords = items[1].Split(",");

                if(Int32.Parse(sensorCoords[0]) > highestX)
                {
                    highestX = Int32.Parse(sensorCoords[0]);
                }
                if (Int32.Parse(beaconCoords[0]) > highestX)
                {
                    highestX = Int32.Parse(beaconCoords[0]);
                }
                if (Int32.Parse(sensorCoords[0]) < lowestX)
                {
                    lowestX = Int32.Parse(sensorCoords[0]);
                }
                if (Int32.Parse(beaconCoords[0]) < lowestX)
                {
                    lowestX = Int32.Parse(beaconCoords[0]);
                }

                sensorCoordsArray.Add(sensorCoords[0]+","+ sensorCoords[1]);
                beaconCoordsArray.Add(beaconCoords[0]+","+ beaconCoords[1]);

            }

            foreach (string sensor in sensorCoordsArray)
            {
                int nearestBeaconDistance = findNearestBeaconDistance(sensor);
                beaconCoordsSensorArray.Add(sensor + "," + nearestBeaconDistance);

            }

            int noBeaconCount = 0;

            Console.WriteLine("Highest X = " + highestX + " lowest X = " + lowestX);
            
            for (int x = lowestX; x <= highestX; x++)
            {
                bool sensorAtCoord = false;
                foreach (string beaconCoordsSensor in beaconCoordsSensorArray)
                {
                    string[] beaconCoordsSensorItem = beaconCoordsSensor.Split(",");

                    int xCoord = Int32.Parse(beaconCoordsSensorItem[0]);
                    int yCoord = Int32.Parse(beaconCoordsSensorItem[1]);
                    int maxDistance = Int32.Parse(beaconCoordsSensorItem[2]);
                    if (!beaconCoordsArray.Contains(x + "," + y) && !sensorCoordsArray.Contains(x + "," + y))
                    {
                        int combinedDistance = Math.Abs(y - yCoord) + Math.Abs(x - xCoord);
                        if(combinedDistance <= maxDistance)
                        {
                            sensorAtCoord = true;

                        }
                    }
                }
                if(!beaconCoordsArray.Contains(x + "," + y) && sensorAtCoord && !sensorCoordsArray.Contains(x + "," + y))
                {
                    noBeaconCount++;
                }
                    
            }

            Console.WriteLine("Part A: " + noBeaconCount);
            output = "Part A: " + noBeaconCount;

            long tuningFrequency = 0;            
            int targetX = 0;
            int targetY = 0;
            for (int x = 0; x <= 4000000; x++)
            {
                List<string> coverageRange = new List<string>();
                foreach (string beaconCoordsSensor in beaconCoordsSensorArray)
                {
                    string[] beaconCoordsSensorItem = beaconCoordsSensor.Split(",");

                    int xCoord = Int32.Parse(beaconCoordsSensorItem[0]);
                    int yCoord = Int32.Parse(beaconCoordsSensorItem[1]);
                    int maxDistance = Int32.Parse(beaconCoordsSensorItem[2]);

                    int distanceFromX = Math.Abs(x - xCoord);
                    if(distanceFromX > maxDistance)
                    {
                        continue;
                    }
                    int coverageStart =(yCoord - Math.Abs(distanceFromX - maxDistance));
                    int coverageEnd = (yCoord + Math.Abs(distanceFromX - maxDistance));

                    coverageRange.Add(coverageStart + ":" + coverageEnd);
                }
                coverageRange.Sort((n1, n2) => Int32.Parse(n1.Split(":")[0]).CompareTo(Int32.Parse(n2.Split(":")[0])));
                int startRange = -1;
                int endRange = -1;
                foreach (string covRanges in coverageRange)
                {
                    string[] covRangesItem = covRanges.Split(":");
                    int startCoord = Int32.Parse(covRangesItem[0]);
                    int endCoord = Int32.Parse(covRangesItem[1]);

                    if(startRange == -1 && endRange == -1)
                    {
                        // First
                        startRange = startCoord;
                        endRange = endCoord;
                    }
                    else if(startCoord > endRange + 1)
                    {
                        // Gap! Maybe
                        bool trueGap = true;
                        foreach (string covRanges2 in coverageRange)
                        {
                            string[] covRangesItem2 = covRanges2.Split(":");
                            int startCoord2 = Int32.Parse(covRangesItem2[0]);
                            int endCoord2 = Int32.Parse(covRangesItem2[1]);
                            if(endRange + 1 >= startCoord2 && endRange + 1 <= endCoord2)
                            {
                                trueGap = false;
                            }
                        }
                        if(trueGap)
                        {
                            targetX = x;
                            targetY = endRange + 1;
                            break;
                        }
                    } else
                    {
                        startRange = startCoord;
                        endRange = endCoord;
                    }
                }

            }

            tuningFrequency = (4000000L * targetX) + targetY;

            output += "\nPart B: X:" + targetX + " Y:" + targetY + " - frequency: " + tuningFrequency;           


        }
        public void populateNoBeacons(string sensor, int nearestBeaconDistance)
        {
            string[] sensorCoords = sensor.Split(",");
            int xCoord = Int32.Parse(sensorCoords[0]);
            int yCoord = Int32.Parse(sensorCoords[1]);
            for (int x = xCoord; x <= (xCoord + nearestBeaconDistance); x++)
            {
                for (int y = yCoord; y <= (yCoord + nearestBeaconDistance); y++)
                {
                    int combinedDistance = Math.Abs(y - yCoord) + Math.Abs(x - xCoord);
                    if (combinedDistance <= nearestBeaconDistance && !beaconCoordsArray.Contains(x + "," + y) && !gridArray.Contains(x + "," + y))
                    {
                        //Console.WriteLine("Adding noBeacon at "+x+","+y+" :: "+ nearestBeaconDistance + " :: " + combinedDistance);
                        gridArray.Add(x + "," + y);
                    }
                }

            }

            for (int x = xCoord; x >= (xCoord - nearestBeaconDistance); x--)
            {
                for (int y = yCoord; y >= (yCoord - nearestBeaconDistance); y--)
                {
                    int combinedDistance = Math.Abs(y - yCoord) + Math.Abs(x - xCoord);
                    if (combinedDistance <= nearestBeaconDistance && !beaconCoordsArray.Contains(x + "," + y) && !gridArray.Contains(x + "," + y))
                    {
                        //Console.WriteLine("Adding noBeacon at " + x + "," + y + " :: " + nearestBeaconDistance + " :: " + combinedDistance);
                        gridArray.Add(x + "," + y);
                    }
                }

            }

            for (int x = xCoord; x <= (xCoord + nearestBeaconDistance); x++)
            {
                for (int y = yCoord; y >= (yCoord - nearestBeaconDistance); y--)
                {
                    int combinedDistance = Math.Abs(y - yCoord) + Math.Abs(x - xCoord);
                    if (combinedDistance <= nearestBeaconDistance && !beaconCoordsArray.Contains(x + "," + y) && !gridArray.Contains(x + "," + y))
                    {
                        //Console.WriteLine("Adding noBeacon at " + x + "," + y + " :: " + nearestBeaconDistance + " :: " + combinedDistance);
                        gridArray.Add(x + "," + y);
                    }
                }

            }

            for (int x = xCoord; x >= (xCoord - nearestBeaconDistance); x--)
            {
                for (int y = yCoord; y <= (yCoord + nearestBeaconDistance); y++)
                {
                    int combinedDistance = Math.Abs(y - yCoord) + Math.Abs(x - xCoord);
                    if (combinedDistance <= nearestBeaconDistance && !beaconCoordsArray.Contains(x + "," + y) && !gridArray.Contains(x + "," + y))
                    {
                        //Console.WriteLine("Adding noBeacon at " + x + "," + y + " :: " + nearestBeaconDistance + " :: " + combinedDistance);
                        gridArray.Add(x + "," + y);
                    }
                }

            }
        }
        public int findNearestBeaconDistance(string sensor)
        {
            string[] sensorCoords = sensor.Split(",");
            int closestBeacon = 2000000000;
            foreach (string beacon in beaconCoordsArray)
            {
                string[] beaconCoords = beacon.Split(","); 
                int beaconDistance = Math.Abs(Int32.Parse(sensorCoords[0]) - Int32.Parse(beaconCoords[0])) + Math.Abs(Int32.Parse(sensorCoords[1]) - Int32.Parse(beaconCoords[1]));
                if(beaconDistance < closestBeacon)
                {
                    //Console.WriteLine("Nearest beacon at " + beaconCoords[0] + "," + beaconCoords[1]);
                    closestBeacon = beaconDistance;
                }
            }
            return closestBeacon;
        }
        public void createGrid(int xSize, int ySize)
        {

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    grid[x, y] = ".";
                }
            }

        }

        public string output;
    }
}