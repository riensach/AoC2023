using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using static ABI.System.Collections.Generic.IReadOnlyDictionary_Delegates;

namespace AoC2023.solution
{
        public class AoCDay13
        {
        public List<string> packets = new List<string>();
        public List<string> packetsOrdered = new List<string>();

        public AoCDay13(int selectedPart, string input)
        {
            string[] lines = input.Split("\n\r");

            int indexSum = 0;
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }


            output = "Part A: " + indexSum;

            
        }



        public string output;
    }
}


