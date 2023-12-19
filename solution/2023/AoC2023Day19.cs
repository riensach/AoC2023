using System;
using System.IO;
using System.Diagnostics;
using System.Linq;

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
                string[] mainDetails = inputDetails[1].Split(",");
                foreach (string details in mainDetails)
                {
                    string[] ruleDetails = details.Split(":");
                    if(ruleDetails.Count() > 1)
                    {
                        string ruleDecision = ruleDetails[1];
                        String[] delimiters = { ">", "<" };
                        String[] ruleSections = ruleDetails[0].Split(delimiters, StringSplitOptions.None);
                        string calc = (ruleDetails[0].Contains(">") ? ">" : "<");
                        rulesList.Add(new Rules(ruleSections[0], calc, int.Parse(ruleSections[1]), ruleDecision));
                    }                    
                }
                bool defaultRule = (mainDetails.Last() == "A" ? true : false);
                workflowsList.Add(new Workflows(workflowName, rulesList, defaultRule));
            }

            HashSet<Parts> partsList = new HashSet<Parts>();
            foreach (string data in section2)
            {
                string dataNew = data.Replace("{","").Replace("}", "");
                string[] inputDetails = dataNew.Split(",");
                int x = int.Parse(inputDetails[0].Replace("x=", ""));
                int m = int.Parse(inputDetails[1].Replace("m=", ""));
                int a = int.Parse(inputDetails[2].Replace("a=", ""));
                int s = int.Parse(inputDetails[3].Replace("s=", ""));
                partsList.Add(new Parts(x,m,a,s, false));
            }


            string currentWorkflow = "in";
            foreach (Parts part in partsList)
            {
                // Here we process the steps
                bool decisionMade = false;
                bool workflowDecision = false;
                while(decisionMade == false)
                {
                    int workflowDecision = 0;


                    if (workflowDecision > 0)
                    {
                        decisionMade = true;
                    }
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
        public record Parts(int x, int m, int a, int s, bool acceptedStatus);
        public record Workflows(string workflowName, List<Rules> workflowRules, bool defaultAccept);
        public record Rules(string partCheck, string checkCalc, int checkAmount, string destinationWorkflow);

        public string output;
    }


}