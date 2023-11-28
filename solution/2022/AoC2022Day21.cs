using System;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using LoreSoft.MathExpressions;

namespace AoC2022.solution
{
    public class AoCDay21
    {
        public long partBRootLeft = 0;
        public long partBRootRight = 0;
        public AoCDay21(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            double finalMonkeyYellA = calculateMonkeyA(lines);
            double parsingValueB = 0;

            bool foundMonkeyB = false;
            long increaseDelta = 1000000000;
            long integer = 1000;
            int lastDirection = 0;
            while(foundMonkeyB == false)
            {
                Console.WriteLine("Attempting to pass " + integer);
                if (calculateMonkeyB(lines, integer))
                {
                    parsingValueB = integer;
                    break;
                }
                if(partBRootLeft > partBRootRight)
                {
                    if (lastDirection == 2)
                    {
                        increaseDelta = increaseDelta / 2;
                    }
                    integer = integer + increaseDelta;
                    lastDirection = 1;
                } else
                {
                    if(lastDirection == 1)
                    {
                        increaseDelta = increaseDelta / 2;
                    }
                    integer = integer - increaseDelta;
                    lastDirection = 2;
                }
            }

            output = "Part A: " + finalMonkeyYellA;
            output += "\nPart B: " + parsingValueB;
        }

        public bool calculateMonkeyB(string[] lines, double initialValue)
        {
            //List<(string Index, Monkey Value)> monkeyLists = new List<(string Index, Monkey Value)>();
            IDictionary<string, Monkey> monkeyLists = new Dictionary<string, Monkey>();
            Monkey monkey;
            int numericValue;

            foreach (string line in lines)
            {

                //Console.WriteLine(inputString);

                string[] parts = line.Split(": ");
                bool isNumber = int.TryParse(parts[1], out numericValue);
                if (isNumber)
                {
                    monkey = new Monkey(parts[0], numericValue, "");
                }
                else
                {
                    monkey = new Monkey(parts[0], 0, parts[1]);
                }
                if(parts[0] == "root")
                {
                    monkey.action = "=";
                }
                if (parts[0] == "humn")
                {
                    monkey.yellNumber = initialValue;
                }
                monkeyLists.Add(parts[0], monkey);
            }

            bool hasFinalMonkeyYelled = false;

            double monkeyAValue = 0;
            double monkeyBValue = 0;
            //MathEvaluator eval = new MathEvaluator();
            while (hasFinalMonkeyYelled == false)
            {
                foreach (Monkey monkeyObj in monkeyLists.Values)
                {
                    if (monkeyObj.yellNumber > 0 && monkeyObj.yelled == 0)
                    {
                        monkeyObj.yelled = 1;
                        //Console.WriteLine("Yellow for Monkey " + monkeyObj.name);
                    }
                    if (monkeyObj.monkeyAReference == "")
                    {
                        continue;
                    }
                    
                    if (monkeyLists[monkeyObj.monkeyAReference].yelled == 1 && monkeyLists[monkeyObj.monkeyBReference].yelled == 1)
                    {

                        monkeyAValue = monkeyLists[monkeyObj.monkeyAReference].yellNumber;
                        monkeyBValue = monkeyLists[monkeyObj.monkeyBReference].yellNumber;
                        if (monkeyObj.name == "root")
                        {
                            if (monkeyAValue == monkeyBValue)
                            {
                                return true;
                            }
                            else
                            {
                                partBRootLeft = (long)monkeyAValue;
                                partBRootRight = (long)monkeyBValue;
                                return false;
                            }
                        }
                        //Console.WriteLine("Evaluating string:" + monkeyAValue + monkeyObj.action + monkeyBValue);
                        if(monkeyObj.action == "*")
                        {
                            monkeyObj.yellNumber = monkeyAValue * monkeyBValue;

                        } else if (monkeyObj.action == "/")
                        {
                            monkeyObj.yellNumber = monkeyAValue / monkeyBValue;
                        }
                        else if(monkeyObj.action == "-")
                        {
                            monkeyObj.yellNumber = monkeyAValue - monkeyBValue;
                        }
                        else if(monkeyObj.action == "+")
                        {
                            monkeyObj.yellNumber = monkeyAValue + monkeyBValue;
                        } 
                            //monkeyObj.yellNumber = eval.Evaluate(monkeyAValue + monkeyObj.action + monkeyBValue);
                        
                       
                        monkeyObj.yelled = 1;

                    }
                }

            }
            return false;
        }

        public double calculateMonkeyA(string[] lines)
        {
            List<Monkey> monkeyLists = new List<Monkey>();
            Monkey monkey;
            int numericValue;
            double finalMonkeyYell = 0;


            foreach (string line in lines)
            {

                //Console.WriteLine(inputString);

                string[] parts = line.Split(": ");
                bool isNumber = int.TryParse(parts[1], out numericValue);
                if (isNumber)
                {
                    monkey = new Monkey(parts[0], numericValue, "");
                }
                else
                {
                    monkey = new Monkey(parts[0], 0, parts[1]);
                }
                monkeyLists.Add(monkey);
            }
            bool hasFinalMonkeyYelled = false;
            
            double monkeyAValue = 0;
            double monkeyBValue = 0;
            MathEvaluator eval = new MathEvaluator();
            while (hasFinalMonkeyYelled == false)
            {
                foreach (Monkey monkeyObj in monkeyLists)
                {
                    if (monkeyObj.yellNumber > 0 && monkeyObj.yelled == 0)
                    {
                        monkeyObj.yelled = 1;
                        //Console.WriteLine("Yellow for Monkey " + monkeyObj.name);
                    }
                    if (monkeyObj.monkeyAReference == "")
                    {
                        continue;
                    }
                    if (monkeyLists.Find(x => x.name == monkeyObj.monkeyAReference).yelled == 1 && monkeyLists.Find(x => x.name == monkeyObj.monkeyBReference).yelled == 1)
                    {
                        monkeyAValue = monkeyLists.Find(x => x.name == monkeyObj.monkeyAReference).yellNumber;
                        monkeyBValue = monkeyLists.Find(x => x.name == monkeyObj.monkeyBReference).yellNumber;

                        monkeyObj.yellNumber = eval.Evaluate(monkeyAValue + monkeyObj.action + monkeyBValue);
                        //Console.WriteLine("Evaluating string:" + monkeyAValue + monkeyObj.action + monkeyBValue);
                        monkeyObj.yelled = 1;

                    }
                }
                if (monkeyLists.Find(x => x.name == "root").yelled == 1)
                {
                    Console.WriteLine("Fuinal monkey yell");
                    hasFinalMonkeyYelled = true;
                    finalMonkeyYell = monkeyLists.Find(x => x.name == "root").yellNumber;
                }
            }
            return finalMonkeyYell;
        }

        public string output;
    }

    public class Monkey
    {
        public string name;
        public double yellNumber;
        public string monkeyAReference = "";
        public string monkeyBReference = "";
        public string action = "";
        public int yelled = 0;
        public int constrained = 0;
        public Monkey(string monkeyName, double yellNumberInput, string actionParams) {
            name = monkeyName;
            yellNumber = yellNumberInput;
            if (actionParams != "") {
                constrained = 1;
                /*
                string pattern = @"(.{4}) (.{1}) (.{4})";
                Regex regex = new Regex(pattern);
                string[] substrings = regex.Split(actionParams);
                monkeyAReference = substrings[1];
                action = substrings[2];
                monkeyBReference = substrings[3];
                */

                string[] substrings = actionParams.Split(" ");
                monkeyAReference = substrings[0];
                action = substrings[1];
                monkeyBReference = substrings[2];


                //Console.WriteLine(monkeyAReference + " - " + action + " - " + monkeyBReference);
            }

        }
    }

}