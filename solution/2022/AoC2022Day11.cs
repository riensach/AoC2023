using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using LoreSoft.MathExpressions;
using System.Collections.Generic;

namespace AoC2022.solution
{
    public class AoCDay11
    {

        private int worryLevel = 0;
        public AoCDay11(int selectedPart, string input)
        {
            
            IDictionary<int, double> ints = new Dictionary<int, double>();
            ints.Add(1, 85);
            ints.Add(2, 79);
            ints.Add(3, 63);
            ints.Add(4, 72);
            monkeyObject monkey0 = new monkeyObject(ints,'*',"17",2,2,6);

            IDictionary<int, double> ints1 = new Dictionary<int, double>();
            ints1.Add(5, 53);
            ints1.Add(6, 94);
            ints1.Add(7, 65);
            ints1.Add(8, 81);
            ints1.Add(33, 93);
            ints1.Add(34, 73);
            ints1.Add(35, 57);
            ints1.Add(36, 92);
            monkeyObject monkey1 = new monkeyObject(ints1, '*', "old", 7, 0, 2);

            IDictionary<int, double> ints2 = new Dictionary<int, double>();
            ints2.Add(9, 62);
            ints2.Add(10, 63);
            monkeyObject monkey2 = new monkeyObject(ints2, '+', "7", 13, 7, 6);

            IDictionary<int, double> ints3 = new Dictionary<int, double>();
            ints3.Add(11, 57);
            ints3.Add(12, 92);
            ints3.Add(13, 56);
            monkeyObject monkey3 = new monkeyObject(ints3,'+', "4", 5, 4, 5);

            IDictionary<int, double> ints4 = new Dictionary<int, double>();
            ints4.Add(14, 67);

            monkeyObject monkey4 = new monkeyObject(ints4, '+', "5", 3, 1, 5);

            IDictionary<int, double> ints5 = new Dictionary<int, double>();
            ints5.Add(15, 85);
            ints5.Add(16, 56);
            ints5.Add(17, 66);
            ints5.Add(18, 72);
            ints5.Add(19, 57);
            ints5.Add(20, 99);
            monkeyObject monkey5 = new monkeyObject(ints5, '+', "6", 19, 1, 0);

            IDictionary<int, double> ints6 = new Dictionary<int, double>();
            ints6.Add(21, 86);
            ints6.Add(22, 65);
            ints6.Add(23, 98);
            ints6.Add(24, 97);
            ints6.Add(25, 69);
            monkeyObject monkey6 = new monkeyObject(ints6, '*', "13", 11, 3, 7);

            IDictionary<int, double> ints7 = new Dictionary<int, double>();
            ints7.Add(26, 87);
            ints7.Add(27, 68);
            ints7.Add(28, 92);
            ints7.Add(29, 66);
            ints7.Add(30, 91);
            ints7.Add(31, 50);
            ints7.Add(32, 68);
            monkeyObject monkey7 = new monkeyObject(ints7, '+', "2", 17, 4, 3);
            monkey0.TrueMonkeyDestinationObject = monkey2;
            monkey0.FalseMonkeyDestinationObject = monkey6;
            monkey1.TrueMonkeyDestinationObject = monkey0;
            monkey1.FalseMonkeyDestinationObject = monkey2;
            monkey2.TrueMonkeyDestinationObject = monkey7;
            monkey2.FalseMonkeyDestinationObject = monkey6;
            monkey3.TrueMonkeyDestinationObject = monkey4;
            monkey3.FalseMonkeyDestinationObject = monkey5;
            monkey4.TrueMonkeyDestinationObject = monkey1;
            monkey4.FalseMonkeyDestinationObject = monkey5;
            monkey5.TrueMonkeyDestinationObject = monkey1;
            monkey5.FalseMonkeyDestinationObject = monkey0;
            monkey6.TrueMonkeyDestinationObject = monkey3;
            monkey6.FalseMonkeyDestinationObject = monkey7;
            monkey7.TrueMonkeyDestinationObject = monkey4;
            monkey7.FalseMonkeyDestinationObject = monkey3;

            MathEvaluator eval = new MathEvaluator();

            double tempWorryLevel = 0;
            DataTable dt = new DataTable();
            List<double> tempItems = new List<double>();
            List<string> reviewedItemCount = new List<string>();
            double productVal = monkey0.divisibleTest * monkey1.divisibleTest * monkey2.divisibleTest * monkey3.divisibleTest * monkey4.divisibleTest * monkey5.divisibleTest * monkey6.divisibleTest * monkey7.divisibleTest;
            //double productVal = 2 * 7 * 13 * 5 * 3 * 19 * 11 * 17;
            double tempValue = productVal;

            for (int x = 0; x < 10000; x++)
            {
                foreach (var item in monkey0.items)
                {
                    tempWorryLevel = eval.Evaluate(item.Value + " " + monkey0.operation + " " + monkey0.operationAmount);
                    tempWorryLevel = Math.Floor(tempWorryLevel % tempValue);
                    //tempWorryLevel = Math.Floor(tempWorryLevel / 3);
                    if (tempWorryLevel % monkey0.divisibleTest == 0)
                    {
                        monkey0.TrueMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    else
                    {
                        monkey0.FalseMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    monkey0.inspectItemCount++;

                }
                monkey0.items.Clear();

                foreach (var item in monkey1.items)
                {
                    //tempWorryLevel = eval.Evaluate(item.Value + " " + monkey1.operation + " " + monkey1.operationAmount);
                    tempWorryLevel = eval.Evaluate(item.Value + " " + monkey1.operation + " " + item.Value);
                    tempWorryLevel = Math.Floor(tempWorryLevel % tempValue);
                    //tempWorryLevel = Math.Floor(tempWorryLevel / 3);

                    if (tempWorryLevel % monkey1.divisibleTest == 0)
                    {
                        monkey1.TrueMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    else
                    {
                        monkey1.FalseMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }

                    monkey1.inspectItemCount++;
                }
                monkey1.items.Clear();

                foreach (var item in monkey2.items)
                {
                    //tempWorryLevel = eval.Evaluate(item.Value + " " + monkey2.operation + " " + item.Value);
                    tempWorryLevel = eval.Evaluate(item.Value + " " + monkey2.operation + " " + monkey2.operationAmount);
                    tempWorryLevel = Math.Floor(tempWorryLevel % tempValue);
                    //tempWorryLevel = Math.Floor(tempWorryLevel / 3);
                    if (tempWorryLevel % monkey2.divisibleTest == 0)
                    {
                        monkey2.TrueMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    else
                    {
                        monkey2.FalseMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    monkey2.inspectItemCount++;
                }
                monkey2.items.Clear();

                foreach (var item in monkey3.items)
                {
                    tempWorryLevel = eval.Evaluate(item.Value + " " + monkey3.operation + " " + monkey3.operationAmount);
                    tempWorryLevel = Math.Floor(tempWorryLevel % tempValue);
                    //tempWorryLevel = Math.Floor(tempWorryLevel / 3);
                    if (tempWorryLevel % monkey3.divisibleTest == 0)
                    {
                        monkey3.TrueMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    else
                    {
                        monkey3.FalseMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    monkey3.inspectItemCount++;
                }
                monkey3.items.Clear();
                    
                foreach (var item in monkey4.items)
                {
                    tempWorryLevel = eval.Evaluate(item.Value + " " + monkey4.operation + " " + monkey4.operationAmount);
                    tempWorryLevel = Math.Floor(tempWorryLevel % tempValue);
                    //tempWorryLevel = Math.Floor(tempWorryLevel / 3);
                    if (tempWorryLevel % monkey4.divisibleTest == 0)
                    {
                        monkey4.TrueMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    else
                    {
                        monkey4.FalseMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    monkey4.inspectItemCount++;
                }
                monkey4.items.Clear();

                foreach (var item in monkey5.items)
                {
                    tempWorryLevel = eval.Evaluate(item.Value + " " + monkey5.operation + " " + monkey5.operationAmount);
                    tempWorryLevel = Math.Floor(tempWorryLevel % tempValue);
                    //tempWorryLevel = Math.Floor(tempWorryLevel / 3);
                    if (tempWorryLevel % monkey5.divisibleTest == 0)
                    {
                        monkey5.TrueMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    else
                    {
                        monkey5.FalseMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    monkey5.inspectItemCount++;
                }
                monkey5.items.Clear();

                foreach (var item in monkey6.items)
                {
                    tempWorryLevel = eval.Evaluate(item.Value + " " + monkey6.operation + " " + monkey6.operationAmount);
                    tempWorryLevel = Math.Floor(tempWorryLevel % tempValue);
                    //tempWorryLevel = Math.Floor(tempWorryLevel / 3);
                    if (tempWorryLevel % monkey6.divisibleTest == 0)
                    {
                        monkey6.TrueMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    else
                    {
                        monkey6.FalseMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    monkey6.inspectItemCount++;
                }
                monkey6.items.Clear();

                foreach (var item in monkey7.items)
                {
                    tempWorryLevel = eval.Evaluate(item.Value + " " + monkey7.operation + " " + monkey7.operationAmount);
                    tempWorryLevel = Math.Floor(tempWorryLevel % tempValue);
                    //tempWorryLevel = Math.Floor(tempWorryLevel / 3);
                    if (tempWorryLevel % monkey7.divisibleTest == 0)
                    {
                        monkey7.TrueMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    else
                    {
                        monkey7.FalseMonkeyDestinationObject.items.Add(item.Key, tempWorryLevel);
                    }
                    monkey7.inspectItemCount++;
                }
                monkey7.items.Clear();                      

            }

            List<int> monkeyList = new List<int>();
            monkeyList.Add(monkey0.inspectItemCount);
            monkeyList.Add(monkey1.inspectItemCount);
            monkeyList.Add(monkey2.inspectItemCount);
            monkeyList.Add(monkey3.inspectItemCount);
            monkeyList.Add(monkey4.inspectItemCount);
            monkeyList.Add(monkey5.inspectItemCount);
            monkeyList.Add(monkey6.inspectItemCount);
            monkeyList.Add(monkey7.inspectItemCount);
            monkeyList.Sort();
            monkeyList.Reverse();

            double topMonkeyA = monkeyList[0];
            double topMonkeyB = monkeyList[1];

            double monkeyBusiness = topMonkeyA * topMonkeyB;

            output = "Part A: " + monkeyBusiness + " - " + monkeyList[0] + " - " + monkeyList[1] + " - " + monkeyList[2] + " - " + monkeyList[3] + "\n";
            
        }

        public string output;

    }

    

    public class monkeyObject
    {
        //public List<double> items;
        public IDictionary<int, double> items;
        public char operation;
        public string operationAmount;
        public int divisibleTest;
        public int trueMonkeyDestination;
        public int falseMonkeyDestination;
        public int inspectItemCount = 0;
        private monkeyObject trueMonkeyDestinationObject;
        private monkeyObject falseMonkeyDestinationObject;

        public monkeyObject TrueMonkeyDestinationObject
        {
            get { return trueMonkeyDestinationObject; }   // get method
            set { trueMonkeyDestinationObject = value; }  // set method
        }
        public monkeyObject FalseMonkeyDestinationObject
        {
            get { return falseMonkeyDestinationObject; }   // get method
            set { falseMonkeyDestinationObject = value; }  // set method
        }
        //public monkeyObject(List<double> items, char operation, string operationAmount, int divisibleTest, int trueMonkeyDestination, int falseMonkeyDestination)
        public monkeyObject(IDictionary<int, double> items, char operation, string operationAmount, int divisibleTest, int trueMonkeyDestination, int falseMonkeyDestination)
        {
            this.items = items;
            this.operation = operation;
            this.operationAmount = operationAmount;
            this.divisibleTest = divisibleTest;
            this.trueMonkeyDestination = trueMonkeyDestination;
            this.falseMonkeyDestination = falseMonkeyDestination;
        }

    }
}