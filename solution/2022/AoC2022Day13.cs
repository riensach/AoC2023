using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;

namespace AoC2022.solution
{
        public class AoCDay13
        {
        public List<string> packets = new List<string>();
        public List<string> packetsOrdered = new List<string>();

        public AoCDay13(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            int startPacket = 1;
            int index = 1;
            int indexSum = 0;
            string tempPacket = "";

            foreach (string line in lines)
            {
                if (line == "")
                {
                    startPacket = 1;
                    continue;
                }
                if (startPacket == 1)
                {
                    startPacket = 0;
                    tempPacket = line;
                }
                else
                {
                    packets.Add(tempPacket);
                    packets.Add(line);
                    JsonElement temp1 = JsonSerializer.Deserialize<JsonElement>(tempPacket);
                    JsonElement temp2 = JsonSerializer.Deserialize<JsonElement>(line);
                    int resultFound = comparison(temp1, temp2);
                    bool rightOrder = false;

                    if (resultFound < 0)
                    {
                        rightOrder = true;
                    }

                    if (rightOrder)
                    {
                        indexSum = indexSum + index;
                    }
                    index++;
                }
            }

            output = "Part A: " + indexSum;

            packets.Add("[[2]]");
            packets.Add("[[6]]");
            packetsOrdered = packets.Select(i => new string(i)).ToList();
            bool ordered = false;
            while (ordered == false) {
                bool correctOrder = true;
                for (int i = 0; i < packetsOrdered.Count - 1; i++)
                {
                    JsonElement packetComp1 = JsonSerializer.Deserialize<JsonElement>(packetsOrdered[i]);
                    JsonElement packetComp2 = JsonSerializer.Deserialize<JsonElement>(packetsOrdered[i+1]);

                    int resultFound = comparison(packetComp1, packetComp2);

                    if (resultFound >= 0)
                    {
                        correctOrder = false;
                        packetsOrdered.Insert(i + 2, packetsOrdered[i]);
                        packetsOrdered.RemoveAt(i);
                    }
                }

                if (correctOrder == true)
                {
                    break;
                }
            }

            int packet1 = packetsOrdered.FindIndex( x => x.Equals("[[2]]")) + 1;
            int packet2 = packetsOrdered.FindIndex( x => x.Equals("[[6]]")) + 1;

            int keyValue = packet1 * packet2;

            output += "\nPart B: " + keyValue + ". Index: "+ packet1+ " - "+ packet2;
        }


        public int comparison(JsonElement leftSide, JsonElement rightSide)
        {
            JsonElement newComp;
            switch (leftSide.ValueKind, rightSide.ValueKind)
            {
                case (JsonValueKind.Null, not JsonValueKind.Null):
                    return -1;
                case (not JsonValueKind.Null, JsonValueKind.Null):
                    return 1;
                case (JsonValueKind.Number, JsonValueKind.Number):
                    if (leftSide.GetInt32() < rightSide.GetInt32())
                    {
                        return -1;
                    }
                    else if (leftSide.GetInt32() > rightSide.GetInt32())
                    {
                        return 1;
                    }
                    return 0;
                case (JsonValueKind.Number, JsonValueKind.Array):
                    newComp = JsonSerializer.Deserialize<JsonElement>("[" + leftSide.GetInt32() + "]");
                    return comparison(newComp, rightSide);
                case (JsonValueKind.Array, JsonValueKind.Number):
                    newComp = JsonSerializer.Deserialize<JsonElement>("[" + rightSide.GetInt32() + "]");
                    return comparison(leftSide, newComp);
                case (JsonValueKind.Array, JsonValueKind.Array):
                    var leftArray = leftSide.EnumerateArray();
                    var rightArray = rightSide.EnumerateArray();

                    while (leftArray.MoveNext() && rightArray.MoveNext())
                    {
                        var leftArrayItem = leftArray.Current;
                        var rightArrayItem = rightArray.Current;

                        var compair = comparison(leftArrayItem, rightArrayItem);

                        if (compair != 0)
                        {
                            return compair;
                        }
                    }

                    var countItem = leftArray.Count() - rightArray.Count();

                    return countItem switch
                    {
                        0 => 0,
                        < 0 => -1,
                        _ => 1
                    };
                default:
                    return 0;
            }
        }

        public string output;
    }
}


