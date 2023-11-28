using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.ComponentModel;

namespace AoC2022.solution
{
    public class AoCDay16
    {
        private IDictionary<string, int> valvesFlowRate = new Dictionary<string, int>();
        private IDictionary<string, List<string>> valvesTunnels = new Dictionary<string, List<string>>();
        private IDictionary<string, int> valves = new Dictionary<string, int>();

        public AoCDay16(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );


            int maximumValves = 0;
            foreach (string line in lines)
            {
                string inputString = line.Replace(" tunnels lead to valves ", "");
                inputString = inputString.Replace(" has flow rate", "");
                inputString = inputString.Replace(" tunnel leads to valve ", "");
                inputString = inputString.Replace("Valve ", "");
                string[] parts = inputString.Split(";");
                string[] firstPart = parts[0].Split("=");
                string[] secondPart = parts[1].Split(", ");
                string valve = firstPart[0];
                string flowRate = firstPart[1];
                if(Int32.Parse(flowRate) > 0)
                {
                    maximumValves++;
                }
                List<string> tunnels = new List<string>();
                foreach (string valves in secondPart)
                {
                    tunnels.Add(valves);
                }
                Console.Write("Value " + valve + " with flow rate " + flowRate + " has tunnels leading to ");
                for (int i = 0; i < tunnels.Count; i++)
                {
                    Console.Write(tunnels[i] + " ");
                    if (!valves.ContainsKey(tunnels[i]))
                    {
                        valves.Add(tunnels[i], 0);
                    }

                }
                Console.Write("\n");
                valvesFlowRate.Add(valve, Int32.Parse(flowRate));
                valvesTunnels.Add(valve, tunnels);
            }

            Random rand = new Random();
            int[] minutes = new int[] {30, 26};
            string[] puzzleParts = new string[] {"A", "B"};
            int maximumLength = (maximumValves) + (maximumValves * 2);
            Console.WriteLine("Maximum length: "+ maximumLength);

            for (int h = 0; h < 2; h++)
            {
                int pressureReleased = 0;
                int startingPressureReleased = 0;
                int startingMinute = 1;
                int MaximumPressureReleasedHistory = 0;
                string startingValve = "AA";
                string openedValvesStart = "";
                valveObject startingValveObject = new valveObject(openedValvesStart, startingValve, startingMinute, startingPressureReleased, startingValve);
                Queue<valveObject> queue = new Queue<valveObject>();

                int maximumMinutes = minutes[h];                

                queue.Enqueue(startingValveObject);
                do
                {
                    // Queue
                    valveObject currentItem = queue.Dequeue();
                    int currentMinute = currentItem.currentMinute;
                    int maximumPressureReleased = currentItem.currentPressureReleased;
                    string currentValveFirst = currentItem.currentValue;
                    string currentValveSecond = currentItem.currentValueSecond;
                    bool isValveOpenFirst = currentItem.openedValves.Contains(currentValveFirst);
                    bool isValveOpenSecond = currentItem.openedValves.Contains(currentValveSecond);
                    int optionCountFirst = valvesTunnels[currentValveFirst].Count();
                    int optionCountSecond = valvesTunnels[currentValveSecond].Count();
                    int currentFlowRateFirst = valvesFlowRate[currentValveFirst];
                    int currentFlowRateSecond = valvesFlowRate[currentValveSecond];

                    if (currentMinute > maximumMinutes)
                    {
                        continue;
                    }
                    if (maximumPressureReleased > pressureReleased)
                    {
                        pressureReleased = maximumPressureReleased;
                    }
                    if (maximumPressureReleased > MaximumPressureReleasedHistory)
                    {
                        MaximumPressureReleasedHistory = maximumPressureReleased;
                    }
                    if (currentMinute > maximumMinutes)
                    {
                        break;
                    }
                    if (currentMinute > 6 && maximumPressureReleased < 1)
                    {
                        // After 12 steps we have no pressure, so no need to go further
                        continue;
                    }
                    if (currentMinute > 7 && maximumPressureReleased < (MaximumPressureReleasedHistory - 1000))
                    {
                        // We've released more pressure before, no need to go down this track
                        continue;
                    }
                    if (currentMinute > 8 && maximumPressureReleased < (MaximumPressureReleasedHistory - 500))
                    {
                        // We've released more pressure before, no need to go down this track
                        continue;
                    }
                    if (currentMinute > 9 && maximumPressureReleased < (MaximumPressureReleasedHistory - 100))
                    {
                        // We've released more pressure before, no need to go down this track
                        continue;
                    }
                    if (currentMinute > 12 && maximumPressureReleased < (MaximumPressureReleasedHistory - 50))
                    {
                        // We've released more pressure before, no need to go down this track
                        continue;
                    }
                    if (currentMinute > 14 && maximumPressureReleased < (MaximumPressureReleasedHistory - 50))
                    {
                        // We've released more pressure before, no need to go down this track
                        continue;
                    }
                    if (currentMinute > 18 && maximumPressureReleased < (MaximumPressureReleasedHistory - 50))
                    {
                        // We've released more pressure before, no need to go down this track
                        continue;
                    }
                    if (currentItem.openedValves.Length == maximumLength)
                    {
                        // All the valves are open
                        continue;
                    }
                    if (h == 0)
                    {
                        // Part A
                        for (int i = 0; i < optionCountFirst; i++)
                        {
                            string currentValveMovedTo = valvesTunnels[currentValveFirst][i];
                            string copyOfValves = currentItem.openedValves;
                            valveObject newValveObject = new valveObject(copyOfValves, currentValveMovedTo, currentMinute + 1, maximumPressureReleased, currentValveSecond);
                            queue.Enqueue(newValveObject);
                        }

                        if (currentFlowRateFirst > 0 && !isValveOpenFirst)
                        {
                            // We can open the valve, it's not open
                            int pressureToRelease = (currentFlowRateFirst * (maximumMinutes - currentMinute));
                            int NewmaximumPressureReleased = maximumPressureReleased + pressureToRelease;
                            valveObject newValveObject = new valveObject(currentItem.openedValves + ":" + currentValveFirst, currentValveFirst, currentMinute + 1, NewmaximumPressureReleased, currentValveSecond);
                            queue.Enqueue(newValveObject);

                        }
                    } else
                    {
                        if(currentMinute > 8)
                        {
                            Queue<valveObject> queue2 = new Queue<valveObject>(queue.Distinct());
                            queue = queue2;
                        }
                        if(currentFlowRateFirst > 0 && currentFlowRateSecond > 0 && !isValveOpenFirst && !isValveOpenSecond && currentValveFirst != currentValveSecond)
                        {
                            // Option to open both valves - simples
                            int pressureToReleaseFirst = (currentFlowRateFirst * (maximumMinutes - currentMinute));
                            int pressureToReleaseSecond = (currentFlowRateSecond * (maximumMinutes - currentMinute));
                            int NewmaximumPressureReleased = maximumPressureReleased + pressureToReleaseFirst + pressureToReleaseSecond;
                            valveObject newValveObject = new valveObject(currentItem.openedValves + ":" + currentValveFirst + ":" + currentValveSecond, currentValveFirst, currentMinute + 1, NewmaximumPressureReleased, currentValveSecond);
                            queue.Enqueue(newValveObject);
                        } 
                        if (currentFlowRateFirst > 0 && !isValveOpenFirst)
                        {
                            // We can open the first valve, so we need all the permutations of open first valve + second person moves
                            int pressureToRelease = (currentFlowRateFirst * (maximumMinutes - currentMinute));
                            int NewmaximumPressureReleased = maximumPressureReleased + pressureToRelease;
                            for (int i = 0; i < optionCountSecond; i++)
                            {
                                string currentValveMovedToSecond = valvesTunnels[currentValveSecond][i];
                                valveObject newValveObject = new valveObject(currentItem.openedValves + ":" + currentValveFirst, currentValveFirst, currentMinute + 1, NewmaximumPressureReleased, currentValveMovedToSecond);
                                queue.Enqueue(newValveObject);
                            }
                        } 
                        if (currentFlowRateSecond > 0 && !isValveOpenSecond)
                        {
                            // We can open the second valve, so we need all the permutations of open second valve + first person moves
                            int pressureToRelease = (currentFlowRateSecond * (maximumMinutes - currentMinute));
                            int NewmaximumPressureReleased = maximumPressureReleased + pressureToRelease;
                            for (int i = 0; i < optionCountFirst; i++)
                            {
                                string currentValveMovedToFirst = valvesTunnels[currentValveFirst][i];
                                valveObject newValveObject = new valveObject(currentItem.openedValves + ":" + currentValveSecond, currentValveMovedToFirst, currentMinute + 1, NewmaximumPressureReleased, currentValveSecond);
                                queue.Enqueue(newValveObject);
                            }
                        }

                        for (int q = 0; q < optionCountFirst; q++)
                        {
                            for (int w = 0; w < optionCountSecond; w++)
                            {
                                string currentValveMovedToFirst = valvesTunnels[currentValveFirst][q];
                                string copyOfValves = currentItem.openedValves;
                                string currentValveMovedToSecond = valvesTunnels[currentValveSecond][w];
                                valveObject newValveObject = new valveObject(copyOfValves, currentValveMovedToFirst, currentMinute + 1, maximumPressureReleased, currentValveMovedToSecond);
                                queue.Enqueue(newValveObject);
                            }
                        }

                    }

                } while (queue.Count > 0);

                Console.WriteLine("\nPart "+ puzzleParts[h] + ": Maximum Pressure Released over "+ minutes[h] + " minutes:" + pressureReleased+"\n");
            }
        }
        public string output;
    }

    public class valveObject
    {
        //public IDictionary<string, int> valves = new Dictionary<string, int>();
        //public HashSet<string> openedValves = new HashSet<string>();
        public string openedValves;
        public string currentValue;
        public string currentValueSecond;
        public int currentMinute;
        public int currentPressureReleased;
        public valveObject(string valvesInput, string currentValveInput, int currentMinuteInput, int currentPressureReleasedInput, string currentValveSecondInput)
        {
            openedValves = valvesInput;
            currentValue = currentValveInput;
            currentValueSecond = currentValveSecondInput;
            currentMinute = currentMinuteInput;
            currentPressureReleased = currentPressureReleasedInput;
        }
        public override bool Equals(object? obj)
        {
            return Equals((valveObject) obj);
        }
        public bool Equals(valveObject y)
        {
            //if (Enumerable.SequenceEqual(openedValves,y.openedValves) && currentValue.Equals(y.currentValue) && currentMinute.Equals(y.currentMinute) && currentPressureReleased.Equals(y.currentPressureReleased))
            if (openedValves.Equals(y.openedValves) && currentValue.Equals(y.currentValue) && currentMinute.Equals(y.currentMinute) && currentPressureReleased.Equals(y.currentPressureReleased) && currentValueSecond.Equals(y.currentValueSecond))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (openedValves ?? "").GetHashCode();
                hash = hash * 23 + (currentValue ?? "").GetHashCode();
                hash = hash * 23 + (currentValueSecond ?? "").GetHashCode();
                hash = hash * 23 + currentMinute.GetHashCode();
                hash = hash * 23 + currentPressureReleased.GetHashCode();

                return hash;
            }
        }

    }

}