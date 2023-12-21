using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Reflection;
using static AoC2023.solution.AoCDay19;
using System.ComponentModel;

namespace AoC2023.solution
{
    public class AoCDay20
    {
        public long lowPulses = 0;
        public long highPulses = 0;

        public AoCDay20(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            HashSet<Module> modules = new HashSet<Module>();
            
            foreach (string line in lines)
            {

                List<string> destinationModules = new List<string>();
                string[] parts = line.Split(" -> ");
                if (parts[1].Contains(","))
                {
                    string[] moduleDestinations = parts[1].Split(", ");
                    foreach(string moduleDestination in moduleDestinations)
                    {
                        destinationModules.Add(moduleDestination.Trim());
                    }
                } else
                {
                    destinationModules.Add(parts[1].Trim());
                }

                string modName = "";
                string modType = "";
                if(parts[0] == "broadcaster")
                {
                    modName = "broadcaster";
                    modType = "broadcaster";
                } else
                {
                    modName = parts[0].Substring(1);
                    modType = parts[0].Substring(0,1);
                }

                modules.Add(new Module(modName, destinationModules, modType));
            }



            var queue = new PriorityQueue<Module,int>();            
            int currentPulse = 0;
            for (int i = 0; i < 1; i++) {
                queue.Enqueue(modules.Where(p => p.name == "broadcaster").First(), 0);
                int queueID = 0;
                while (queue.Count > 0)
                {
                    var module = queue.Dequeue();
                    // HERE
                    queueID++;
                    queue.Enqueue(modules.Where(p => p.name == "broadcaster").First(), queueID);

                }
            }




            
            foreach (Module mod in modules)
            {

                Console.WriteLine("Name: " + mod.name);
                Console.WriteLine("Type: " + mod.moduleType);
                Console.WriteLine("Destinations: ");
                mod.destinationModules.ForEach(i => Console.Write("{0}\t", i));
                Console.WriteLine();
                foreach (Module seedItem in modules)
                {
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(seedItem))
                    {
                        string name = descriptor.Name;
                        object value = descriptor.GetValue(seedItem);
                        Console.WriteLine("{0}={1}", name, value);
                    }
                }
            }    





        }

        public class Module
        {
            public string name;
            public List<string> destinationModules;
            public string moduleType;
            public int signal; // 0 = low, 1 = high
            public Module(string moduleName, List<string> destinationModulesList, string moduleTypeString)
            {
                name = moduleName;
                destinationModules = destinationModulesList;
                moduleType = moduleTypeString;
            }

        }

        public string output;
    }
}