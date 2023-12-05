using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using static ABI.System.Collections.Generic.IReadOnlyDictionary_Delegates;
using System.Linq;
using Windows.Globalization.DateTimeFormatting;
using System.ComponentModel;
using static AoC2023.solution.AoCDay5;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using LoreSoft.MathExpressions;
using Windows.Data.Xml.Dom;
using System.Collections.Generic;

namespace AoC2023.solution
{
    public class AoCDay5
    {
        public Int64 seedLocationsSecondKey = 999999999999;
        public Int64 seedLocationsSecondValue = 999999999999;



        public AoCDay5(int selectedPart, string input)
        {
            string[] lines = input.Split("\n\r\n");
            List<Int64> seedsList = new List<Int64>();
            Dictionary<Int64, Int64> seedToSoil = new Dictionary<Int64, Int64>();
            Dictionary<Int64, Int64> soilToFertilizer = new Dictionary<Int64, Int64>();
            Dictionary<Int64, Int64> FertilizertoWater = new Dictionary<Int64, Int64>();
            Dictionary<Int64, Int64> waterToLight = new Dictionary<Int64, Int64>();
            Dictionary<Int64, Int64> lightToTemperature = new Dictionary<Int64, Int64>();
            Dictionary<Int64, Int64> temperatureToHumidity = new Dictionary<Int64, Int64>();
            Dictionary<Int64, Int64> humidityToLocation = new Dictionary<Int64, Int64>();

            List<Map> mapList = new List<Map>();
            List<Seed> seedList = new List<Seed>();
            List<Location> locationList = new List<Location>();

            foreach (string line in lines)
            {
                //Console.WriteLine("Line1: " + line + "\n");
                string[] splits = line.Split("\n");
                //Console.WriteLine("Line: " + splits[0] + "\n");
                string name = splits[0].Substring(0, 6);
                if(name == "seeds:")
                {
                    foreach (string split in splits)
                    {
                        string[] seeds = split.Split(" ");
                        int i = 0;
                        Int64 seedStart = 0;
                        Int64 seedLength = 0;
                        foreach (string seed in seeds)
                        {
                            string test = seed.Substring(0, 1);
                            if (!Char.IsDigit(test, 0)) continue;
                            i++;
                            if(i % 2 == 0)
                            {
                                seedLength = Int64.Parse(seed);
                            } else
                            {
                                seedStart = Int64.Parse(seed);
                            }
                            if (i % 2 == 0)
                            {
                                seedList.Add(new Seed()
                                {
                                    SeedStart = seedStart,
                                    SeedEnd = seedStart + seedLength,

                                });
                            }

                                Console.WriteLine("Adding seed: " + seed + "\n");
                            
                            seedsList.Add(Int64.Parse(seed));
                            //Console.WriteLine("Adding seed: " + seed + "\n");
                            
                            
                        }


                     
                    }
                }
                foreach (string split in splits)
                {
                    if (split == "") continue;

                    string test = split.Substring(0, 1);
                    if (!Char.IsDigit(test, 0)) continue;
                    string[] parts = split.Split(" ");

                    string[] groupName = splits[0].Split(" ");

                    mapList.Add(new Map()
                    {
                        SourceStart = Int64.Parse(parts[1]),
                        SourceEnd = Int64.Parse(parts[1]) + Int64.Parse(parts[2]),
                        DestinationStart = Int64.Parse(parts[0]),
                        DestinationEnd = Int64.Parse(parts[0]) + Int64.Parse(parts[2]),
                        MapType = groupName[0]
                    });

                    if(groupName[0] == "humidity-to-location")
                    {
                        locationList.Add(new Location()
                        {
                            SourceStart = Int64.Parse(parts[1]),
                            SourceEnd = Int64.Parse(parts[1]) + Int64.Parse(parts[2]),
                            DestinationStart = Int64.Parse(parts[0]),
                            DestinationEnd = Int64.Parse(parts[0]) + Int64.Parse(parts[2])

                        });
                    }


                }
            }
            /*foreach (Map seedItem in mapList)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(seedItem))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(seedItem);
                    //Console.WriteLine("{0}={1}", name, value);
                }
            }*/

            Dictionary<Int64, Int64> seedLocations = new Dictionary<Int64, Int64>();
            foreach (Int64 seed in seedsList)
            {
                Int64 soilFoundValue = seed;
                var soilMaps = mapList.Where(p => p.MapType == "seed-to-soil").ToList();
                foreach (Map soilMap in soilMaps)
                {
                    if(soilMap.destinationExists(seed))
                    {
                        soilFoundValue = soilMap.findDestination(seed);
                    }
                }

                Int64 fertilizerFoundValue = soilFoundValue;
                var fertilizerMaps = mapList.Where(p => p.MapType == "soil-to-fertilizer").ToList();
                foreach (Map fertilizerMap in fertilizerMaps)
                {
                    if (fertilizerMap.destinationExists(soilFoundValue))
                    {
                        fertilizerFoundValue = fertilizerMap.findDestination(soilFoundValue);
                    }
                }

                Int64 waterFoundValue = fertilizerFoundValue;
                var waterMaps = mapList.Where(p => p.MapType == "fertilizer-to-water").ToList();
                foreach (Map waterMap in waterMaps)
                {
                    if (waterMap.destinationExists(fertilizerFoundValue))
                    {
                        waterFoundValue = waterMap.findDestination(fertilizerFoundValue);
                    }
                }

                Int64 lightFoundValue = waterFoundValue;
                var lightMaps = mapList.Where(p => p.MapType == "water-to-light").ToList();
                foreach (Map lightMap in lightMaps)
                {
                    if (lightMap.destinationExists(waterFoundValue))
                    {
                        lightFoundValue = lightMap.findDestination(waterFoundValue);
                    }
                }

                Int64 temperatureFoundValue = lightFoundValue;
                var temperatureMaps = mapList.Where(p => p.MapType == "light-to-temperature").ToList();
                foreach (Map temperatureMap in temperatureMaps)
                {
                    if (temperatureMap.destinationExists(lightFoundValue))
                    {
                        
                        temperatureFoundValue = temperatureMap.findDestination(lightFoundValue);
                    }
                }

                Int64 humidityFoundValue = temperatureFoundValue;
                var humidityMaps = mapList.Where(p => p.MapType == "temperature-to-humidity").ToList();
                foreach (Map humidityMap in humidityMaps)
                {
                    if (humidityMap.destinationExists(temperatureFoundValue))
                    {
                        humidityFoundValue = humidityMap.findDestination(temperatureFoundValue);
                    }
                }

                Int64 locationFoundValue = humidityFoundValue;
                var locationMaps = mapList.Where(p => p.MapType == "humidity-to-location").ToList();
                foreach (Map locationMap in locationMaps)
                {
                    if (locationMap.destinationExists(humidityFoundValue))
                    {
                        locationFoundValue = locationMap.findDestination(humidityFoundValue);
                    }
                }

                seedLocations.Add(seed, locationFoundValue);
                Console.WriteLine("Seed "+ seed + ", soil "+ soilFoundValue + ", fertilizer "+ fertilizerFoundValue + ", water "+ waterFoundValue + ", light "+ lightFoundValue + ", temperature "+ temperatureFoundValue + ", humidity "+ humidityFoundValue + ", location "+ locationFoundValue + "\n");
            }
            foreach (var month in seedLocations)
            {
                //Console.WriteLine("SeedtoSoil: " +month+"\n");
            }
            Int64 minKey = seedLocations.Min(x => x.Key);
            Int64 minValue = seedLocations.Min(x => x.Value);

            Console.WriteLine("Part A:" + minValue);

            Dictionary<Int64, Int64> seedLocationsSecond = new Dictionary<Int64, Int64>();
            

            foreach (Seed seedItem in seedList)
            {
                Int64 seedLenth = seedItem.SeedEnd - seedItem.SeedStart;
                Console.WriteLine("Starting on seeds : " + seedItem.SeedStart + " with a length of "+ seedLenth+" seeds \n");
                //Thread workerThread = new Thread(processSeeds(seedItem.SeedStart, seedItem.SeedEnd));

                processSeeds(seedItem.SeedStart, seedItem.SeedEnd);


            }
            foreach (var month in seedLocations)
            {
                //Console.WriteLine("SeedtoSoil: " +month+"\n");
            }
            Int64 minKeySecond = seedLocationsSecondKey;
            Int64 minValueSecond = seedLocationsSecondValue;

            Console.WriteLine("\nPart B:" + minValueSecond);

            void processSeeds(Int64 seedStart, Int64 seedEnd)
            {
                var soilMaps = mapList.Where(p => p.MapType == "seed-to-soil").ToList();
                var fertilizerMaps = mapList.Where(p => p.MapType == "soil-to-fertilizer").ToList();
                var waterMaps = mapList.Where(p => p.MapType == "fertilizer-to-water").ToList();
                var lightMaps = mapList.Where(p => p.MapType == "water-to-light").ToList();
                var temperatureMaps = mapList.Where(p => p.MapType == "light-to-temperature").ToList();
                var humidityMaps = mapList.Where(p => p.MapType == "temperature-to-humidity").ToList();
                var locationMaps = mapList.Where(p => p.MapType == "humidity-to-location").ToList();
                for (Int64 seed = seedStart; seed <= seedEnd; seed++)
                {

                    // Task to be executed by a thread from the pool
                    //Console.WriteLine("Added "+seed+" to the thread pool.\n");
                    
                    Int64 locationFoundValue = findLocation(seed, soilMaps, fertilizerMaps, waterMaps, lightMaps, temperatureMaps, humidityMaps, locationMaps);
                    //seedLocationsSecond.Add(seed, locationFoundValue);
                    if (locationFoundValue < seedLocationsSecondValue)
                    {
                        seedLocationsSecondKey = seed;
                        seedLocationsSecondValue = locationFoundValue;
                    }


                    if(seed % 1000000 == 0)
                    {
                        Console.WriteLine("Done " + seed + " seeds.\n");
                    }


                    //Console.WriteLine("Seed " + seed + ", soil " + soilFoundValue + ", fertilizer " + fertilizerFoundValue + ", water " + waterFoundValue + ", light " + lightFoundValue + ", temperature " + temperatureFoundValue + ", humidity " + humidityFoundValue + ", location " + locationFoundValue + "\n");
                }
            }
            
            Int64 findLocation(Int64 seed, List<Map> soilMaps, List<Map> fertilizerMaps, List<Map> waterMaps, List<Map> lightMaps, List<Map> temperatureMaps, List<Map> humidityMaps, List<Map> locationMaps)
            {

                Int64 soilFoundValue = 999999999999;
                
                foreach (Map soilMap in soilMaps)
                {
                    if (soilMap.destinationExists(seed))
                    {
                        //soilFoundValue = soilMap.findDestination(seed);
                        Int64 tempVal = soilMap.findDestination(seed);
                        if (tempVal != seed && tempVal < soilFoundValue)
                        {
                            soilFoundValue = tempVal;
                        }
                    }
                }
                if (soilFoundValue == 999999999999) soilFoundValue = seed;
                Int64 fertilizerFoundValue = 999999999999;
                foreach (Map fertilizerMap in fertilizerMaps)
                {
                    if (fertilizerMap.destinationExists(soilFoundValue))
                    {
                        //fertilizerFoundValue = fertilizerMap.findDestination(soilFoundValue);
                        Int64 tempVal = fertilizerMap.findDestination(soilFoundValue);
                        if (tempVal != soilFoundValue && tempVal < fertilizerFoundValue)
                        {
                            fertilizerFoundValue = tempVal;
                        }
                    }
                }
                if (fertilizerFoundValue == 999999999999) fertilizerFoundValue = soilFoundValue;
                Int64 waterFoundValue = 999999999999;
                foreach (Map waterMap in waterMaps)
                {
                    if (waterMap.destinationExists(fertilizerFoundValue))
                    {
                        //waterFoundValue = waterMap.findDestination(fertilizerFoundValue);
                        Int64 tempVal = waterMap.findDestination(fertilizerFoundValue);
                        if (tempVal != fertilizerFoundValue && tempVal < waterFoundValue)
                        {
                            waterFoundValue = tempVal;
                        }
                    }
                }
                if (waterFoundValue == 999999999999) waterFoundValue = fertilizerFoundValue;
                Int64 lightFoundValue = 999999999999;
                foreach (Map lightMap in lightMaps)
                {
                    if (lightMap.destinationExists(waterFoundValue))
                    {
                        //lightFoundValue = lightMap.findDestination(waterFoundValue);
                        Int64 tempVal = lightMap.findDestination(waterFoundValue);
                        if (tempVal != waterFoundValue && tempVal < lightFoundValue)
                        {
                            lightFoundValue = tempVal;
                        }
                    }
                }
                if (lightFoundValue == 999999999999) lightFoundValue = waterFoundValue;
                Int64 temperatureFoundValue = 999999999999;
                foreach (Map temperatureMap in temperatureMaps)
                {
                    if (temperatureMap.destinationExists(lightFoundValue))
                    {
                        Int64 tempVal = temperatureMap.findDestination(lightFoundValue);
                        if (tempVal != lightFoundValue && tempVal < temperatureFoundValue)
                        {
                            temperatureFoundValue = tempVal;

                        }
                    }
                }
                if (temperatureFoundValue == 999999999999) temperatureFoundValue = lightFoundValue;
                Int64 humidityFoundValue = 999999999999;
                foreach (Map humidityMap in humidityMaps)
                {
                    if (humidityMap.destinationExists(temperatureFoundValue))
                    {
                        //humidityFoundValue = humidityMap.findDestination(temperatureFoundValue);
                        Int64 tempVal = humidityMap.findDestination(temperatureFoundValue);
                        if (tempVal != temperatureFoundValue && tempVal < humidityFoundValue)
                        {
                            humidityFoundValue = tempVal;
                        }
                    }
                }
                if (humidityFoundValue == 999999999999) humidityFoundValue = temperatureFoundValue;
                Int64 locationFoundValue = 999999999999;
                foreach (Map locationMap in locationMaps)
                {
                    if (locationMap.destinationExists(humidityFoundValue))
                    {
                        //locationFoundValue = locationMap.findDestination(humidityFoundValue);
                        Int64 tempVal = locationMap.findDestination(humidityFoundValue);
                        if (tempVal != humidityFoundValue && tempVal < locationFoundValue)
                        {
                            locationFoundValue = tempVal;
                        }
                    }
                }
                if (locationFoundValue == 999999999999) locationFoundValue = humidityFoundValue;

                return locationFoundValue;
            }

        }

        

        public string output;
        public class Map
        {
            public Int64 SourceStart { get; set; }
            public Int64 SourceEnd { get; set; }
            public Int64 DestinationStart { get; set; }
            public Int64 DestinationEnd { get; set; }
            public string MapType { get; set; }

            public Int64 findDestination(Int64 location)
            {
                //Console.WriteLine("Sent location " + location);
                if (location >= SourceStart && location <= SourceEnd)
                {                    
                    Int64 value = DestinationStart + (location - SourceStart);
                    //Console.WriteLine("Found it - returning " + value + "\n");
                    return DestinationStart + (location - SourceStart);
                } 
                return -1;
            }
            public bool destinationExists(Int64 location)
            {
                if (location >= SourceStart && location <= SourceEnd)
                {
                    return true;
                }
                return false;
            }
        }

        public class Seed
        {
            public Int64 SeedStart { get; set; }
            public Int64 SeedEnd { get; set; }

        }

        public class Location
        {
            public Int64 SourceStart { get; set; }
            public Int64 SourceEnd { get; set; }
            public Int64 DestinationStart { get; set; }
            public Int64 DestinationEnd { get; set; }

        }
    }
}