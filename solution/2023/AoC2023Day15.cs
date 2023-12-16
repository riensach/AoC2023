using System;
using System.IO;
using System.Diagnostics;
using System.Data.SqlTypes;
using System.Collections.Specialized;
using System.Numerics;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;

namespace AoC2023.solution
{
    
    public class AoCDay15
    {

        public AoCDay15(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            string[] calcs = lines[0].Split(",");
            int sumResults = 0;

            foreach (string calc in calcs)
            {
                int calcValue = 0;
                byte[] asciValue = Encoding.ASCII.GetBytes(calc);

                for (int i = 0; i < calc.Length; i++)
                {
                    calcValue = calcValue + asciValue[i];
                    calcValue = calcValue * 17;
                    calcValue = calcValue % 256;
                }


                sumResults = sumResults + calcValue;
            }
            output = "\nPart A:" + sumResults;


            
            List<OrderedDictionary> boxes = new List<OrderedDictionary>();
            for (int i = 0; i < 256; i++)
            {
                OrderedDictionary tempDict = new OrderedDictionary();
                boxes.Add(tempDict);
            }
            string pattern = @"([a-z]+)([=|-])([0-9]*)[,]*";
            Regex rg = new Regex(pattern);
            MatchCollection matchedWords = rg.Matches(lines[0]);
            for (int i = 0; i < matchedWords.Count; i++)
            {

                
                string label = matchedWords[i].Groups[1].Value;
                byte[] asciValue = Encoding.ASCII.GetBytes(label);
                int calcValue = 0;
                for (int x = 0; x < label.Length; x++)
                {
                    calcValue = calcValue + asciValue[x];
                    calcValue = calcValue * 17;
                    calcValue = calcValue % 256;
                }
                int boxID = calcValue;
                if (matchedWords[i].Groups[3].Value.Length > 0)
                {
                    // final bit
                    calcValue = int.Parse(matchedWords[i].Groups[3].Value);
                }
                
                if(matchedWords[i].Groups[2].Value == "=")
                {
                    // it's an equals, update or insert
                    if (boxes[boxID].Contains(label))
                    {

                        boxes[boxID][label] = calcValue;
                    } else
                    {
                        boxes[boxID].Add(label, calcValue);
                    }

                } else
                {
                    // it's a minus, remove
                    if (boxes[boxID].Contains(label))
                    {
                        boxes[boxID].Remove(label);
                    }
                }
                
            }

            int sumResultsSecond = 0;




            foreach (var valueHistory in boxes[0])
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(valueHistory))
                {
                    string name = descriptor.Name;
                    int itemcount = 0;
                    //string itemcount = descriptor.GetValue(valueHistory);
                    object value = descriptor.GetValue(valueHistory);
                    //Console.WriteLine("{0}={1}", name, value, itemcount);

                }
            }

            foreach (var valueHistory in boxes[3])
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(valueHistory))
                {
                    string name = descriptor.Name;
                    int itemcount = 0;
                    //string itemcount = descriptor.GetValue(valueHistory);
                    object value = descriptor.GetValue(valueHistory);
                    //Console.WriteLine("{0}={1}", name, value, itemcount);

                }
            }

            for (int x = 0; x < boxes.Count; x++)
            {
                int boxID = x;
                //Console.WriteLine("got here1 - " + x + " - " + boxes[x].Count);
                for (int i = 0; i < boxes[x].Count; i++)
                {
                    int focalValue = int.Parse(boxes[x][i].ToString());
                    int newValue = 0;
                    //Console.WriteLine("got here :: " +  i);
                    newValue += ((boxID+1) * (i+1) * focalValue);
                    //Console.WriteLine("Adding: " + newValue + " to " + sumResultsSecond);
                    sumResultsSecond = sumResultsSecond + newValue;
                }

            }
            


            output += "\nPart B:" + sumResultsSecond;


        }
       

        public string output;
    }
}