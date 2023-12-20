using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel;

namespace AoC2023.solution
{
    public class AoCDay19
    {
       
        public AoCDay19(int selectedPart, string input)
        {

            string[] sections = input.Split("\n\r\n");
            string[] section1 = sections[0].Split("\n");
            string[] section2 = sections[1].Split("\n");
            HashSet<Workflows> workflowsList = new HashSet<Workflows>();

            foreach (string data in section1)
            {
                List<Rules> rulesList = new List<Rules>();
                string[] inputDetails = data.Split("{");
                string workflowName = inputDetails[0];
                string[] mainDetails = inputDetails[1].Replace("\r", "").Replace("\n", "").Split(",");
                foreach (string details in mainDetails)
                {
                    string[] ruleDetails = details.Replace("\r", "").Replace("\n", "").Split(":");
                    if(ruleDetails.Count() > 1)
                    {
                        string ruleDecision = ruleDetails[1];
                        String[] delimiters = { ">", "<" };
                        String[] ruleSections = ruleDetails[0].Split(delimiters, StringSplitOptions.None);
                        string calc = (ruleDetails[0].Contains(">") ? ">" : "<");
                        rulesList.Add(new Rules(ruleSections[0], calc, int.Parse(ruleSections[1]), ruleDecision));
                    }                    
                }
                string defaultRule = mainDetails.Last().Replace("}", "");
                workflowsList.Add(new Workflows(workflowName, rulesList, defaultRule));
            }

            HashSet<Parts> partsList = new HashSet<Parts>();
            foreach (string data in section2)
            {
                string dataNew = data.Replace("{","").Replace("}", "").Replace("\r", "").Replace("\n", "");
                string[] inputDetails = dataNew.Split(",");
                int x = int.Parse(inputDetails[0].Replace("x=", ""));
                int m = int.Parse(inputDetails[1].Replace("m=", ""));
                int a = int.Parse(inputDetails[2].Replace("a=", ""));
                int s = int.Parse(inputDetails[3].Replace("s=", ""));
                partsList.Add(new Parts(x,m,a,s));
            }

            
            //string currentWorkflow = "in";
            foreach (Parts part in partsList)
            {
                // Here we process the steps
                string currentWorkflow = "in";
                bool decisionMade = false;
                bool workflowDecision = false;
                while(decisionMade == false)
                {

                    /*foreach (Rules seedItem in workflowsList.Where(p => p.workflowName == currentWorkflow).First().workflowRules)
                    {
                        foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(seedItem))
                        {
                            string name = descriptor.Name;
                            object value = descriptor.GetValue(seedItem);
                            Console.WriteLine("{0}={1}", name, value);
                        }
                    }*/
                    string defaultRule = workflowsList.Where(p => p.workflowName == currentWorkflow).First().defaultRule;
                    string initialWorkflow = currentWorkflow;
                    foreach (Rules rule in workflowsList.Where(p => p.workflowName == currentWorkflow).First().workflowRules)
                    {

                        if (rule.partCheck=="x" && rule.checkCalc == ">")
                        {
                            if(part.x > rule.checkAmount)
                            {
                                currentWorkflow = rule.destinationWorkflow;
                                break;
                            }
                        } else if (rule.partCheck == "x" && rule.checkCalc == "<")
                        {
                            if (part.x < rule.checkAmount)
                            {
                                currentWorkflow = rule.destinationWorkflow;
                                break;
                            }
                        }
                        else if (rule.partCheck == "m" && rule.checkCalc == ">")
                        {
                            if (part.m > rule.checkAmount)
                            {
                                currentWorkflow = rule.destinationWorkflow;
                                break;
                            }
                        }
                        else if (rule.partCheck == "m" && rule.checkCalc == "<")
                        {
                            if (part.m < rule.checkAmount)
                            {
                                currentWorkflow = rule.destinationWorkflow;
                                break;
                            }
                        }
                        else if (rule.partCheck == "a" && rule.checkCalc == ">")
                        {
                            if (part.a > rule.checkAmount)
                            {
                                currentWorkflow = rule.destinationWorkflow;
                                break;
                            }
                        }
                        else if (rule.partCheck == "a" && rule.checkCalc == "<")
                        {
                            if (part.a < rule.checkAmount)
                            {
                                currentWorkflow = rule.destinationWorkflow;
                                break;
                            }
                        }
                        else if (rule.partCheck == "s" && rule.checkCalc == ">")
                        {
                            if (part.s > rule.checkAmount)
                            {
                                currentWorkflow = rule.destinationWorkflow;
                                break;
                            }
                        }
                        else if (rule.partCheck == "s" && rule.checkCalc == "<")
                        {
                            if (part.s < rule.checkAmount)
                            {
                                currentWorkflow = rule.destinationWorkflow;
                                break;
                            }
                        }  
                    }

                    if (currentWorkflow == "R")
                    {
                        workflowDecision = false;
                        decisionMade = true;
                        break;
                    }
                    else if (currentWorkflow == "A")
                    {
                        workflowDecision = true;
                        decisionMade = true;
                        break;
                    }
                    if (decisionMade == false && currentWorkflow == initialWorkflow)
                    {
                        if (defaultRule == "R")
                        {
                            workflowDecision = false;
                            decisionMade = true;
                        }
                        else if (defaultRule == "A")
                        {
                            workflowDecision = true;
                            decisionMade = true;
                        } else
                        {
                           currentWorkflow = defaultRule;
                        }
                    }
                    //return;
                }
                part.acceptedStatus = workflowDecision;
            }

            
            int xSum = 0;
            int mSum = 0;
            int aSum = 0;
            int sSum = 0;
            foreach (Parts part in partsList)
            {
                if(part.acceptedStatus == true)
                {
                    xSum = xSum + part.x;
                    mSum = mSum + part.m;
                    aSum = aSum + part.a;
                    sSum = sSum + part.s;
                }
            }

            int totalSum = 0;
            totalSum = xSum + mSum + aSum + sSum;

            output = "Part A: " + totalSum;

        }
        //public record Parts(int x, int m, int a, int s, bool acceptedStatus);

        public class Parts
        {
            public int x;
            public int m;
            public int a;
            public int s;
            public bool acceptedStatus;
            public Parts(int xInput, int mInput, int aInput, int sInput)
            {
                x = xInput;
                m = mInput;
                a = aInput;
                s = sInput;
                acceptedStatus = false;
            }

        }
        public record Workflows(string workflowName, List<Rules> workflowRules, string defaultRule);
        public record Rules(string partCheck, string checkCalc, int checkAmount, string destinationWorkflow);

        public string output;
    }


}