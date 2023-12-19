// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;
using AdventofCode2023;

namespace AdventofCode2023
{

    class Launch
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Advent of Code 2023 in C# \r");
            Console.WriteLine("------------------------\n");


            int daySelection = 19;
            int yearSelection = 2023;
            int partSelection = 1;

            DaySelection Advent = new DaySelection(daySelection, partSelection, yearSelection);
            
            return;
        }
    }
}
