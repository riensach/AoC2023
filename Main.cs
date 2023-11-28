using System;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using AoC2023.solution;
using AoC2023.classes;
using AoC2019.solution;
using AoC2015.solution;

namespace AdventofCode2023
{
    public class DaySelection
    {

        public DaySelection(int selectedDay, int selectedPart, int selectedYear)
        {
            ReadFromFile ReadFromFile = new ReadFromFile(selectedDay, selectedYear);
            string input = ReadFromFile.getTextValue();

            switch (selectedDay, selectedYear)
            {

                case (1, 2015):
                    AoC2015.solution.AoCDay1 AoCDay12015 = new AoC2015.solution.AoCDay1(selectedPart, input);
                    Console.Write(AoCDay12015.output);
                    break;

                case (1, 2023):
                    AoC2023.solution.AoCDay1 AoCDay12023 = new AoC2023.solution.AoCDay1(selectedPart, input);
                    Console.Write(AoCDay12023.output);
                    break;

                case (2, 2023):
                    AoC2023.solution.AoCDay2 AoCDay22023 = new AoC2023.solution.AoCDay2(selectedPart, input);
                    Console.Write(AoCDay22023.output);
                    break;

                case (3, 2023):
                    AoC2023.solution.AoCDay3 AoCDay32023 = new AoC2023.solution.AoCDay3(selectedPart, input);
                    Console.Write(AoCDay32023.output);
                    break;

                case (4, 2023):
                    AoC2023.solution.AoCDay4 AoCDay42023 = new AoC2023.solution.AoCDay4(selectedPart, input);
                    Console.Write(AoCDay42023.output);
                    break;

                case (5, 2023):
                    AoC2023.solution.AoCDay5 AoCDay52023 = new AoC2023.solution.AoCDay5(selectedPart, input);
                    Console.Write(AoCDay52023.output);
                    break;

                case (6, 2023):
                    AoC2023.solution.AoCDay6 AoCDay62023 = new AoC2023.solution.AoCDay6(selectedPart, input);
                    Console.Write(AoCDay62023.output);
                    break;

                case (7, 2023):
                    AoC2023.solution.AoCDay7 AoCDay72023 = new AoC2023.solution.AoCDay7(selectedPart, input);
                    Console.Write(AoCDay72023.output);
                    break;
                        
                case (8, 2023):
                    AoC2023.solution.AoCDay8 AoCDay82023 = new AoC2023.solution.AoCDay8(selectedPart, input);
                    Console.Write(AoCDay82023.output);
                    break;

                case (9, 2023):
                    AoC2023.solution.AoCDay9 AoCDay92023 = new AoC2023.solution.AoCDay9(selectedPart, input);
                    Console.Write(AoCDay92023.output);
                    break;

                case (10, 2023):
                    AoC2023.solution.AoCDay10 AoCDay102023 = new AoC2023.solution.AoCDay10(selectedPart, input);
                    Console.Write(AoCDay102023.output);
                    break;

                case (11, 2023):
                    AoC2023.solution.AoCDay11 AoCDay112023 = new AoC2023.solution.AoCDay11(selectedPart, input);
                    Console.Write(AoCDay112023.output);
                    break;

                case (12, 2023):
                    AoC2023.solution.AoCDay12 AoCDay122023 = new AoC2023.solution.AoCDay12(selectedPart, input);
                    Console.Write(AoCDay122023.output);
                    break;

                case (13, 2023):
                    AoC2023.solution.AoCDay13 AoCDay132023 = new AoC2023.solution.AoCDay13(selectedPart, input);
                    Console.Write(AoCDay132023.output);
                    break;

                case (14, 2023):
                    AoC2023.solution.AoCDay14 AoCDay142023 = new AoC2023.solution.AoCDay14(selectedPart, input);
                    Console.Write(AoCDay142023.output);
                    break;

                case (15, 2023):
                    AoC2023.solution.AoCDay15 AoCDay152023 = new AoC2023.solution.AoCDay15(selectedPart, input);
                    Console.Write(AoCDay152023.output);
                    break;

                case (16, 2023):
                    AoC2023.solution.AoCDay16 AoCDay162023 = new AoC2023.solution.AoCDay16(selectedPart, input);
                    Console.Write(AoCDay162023.output);
                    break;

                case (17, 2023):
                    AoC2023.solution.AoCDay17 AoCDay172023 = new AoC2023.solution.AoCDay17(selectedPart, input);
                    Console.Write(AoCDay172023.output);
                    break;

                case (18, 2023):
                    AoC2023.solution.AoCDay18 AoCDay182023 = new AoC2023.solution.AoCDay18(selectedPart, input);
                    Console.Write(AoCDay182023.output);
                    break;

                case (18, 2019):
                    AoC2019.solution.AoCDay18 AoCDay182019 = new AoC2019.solution.AoCDay18(selectedPart, input);
                    Console.Write(AoCDay182019.output);
                    break;

                case (19, 2023):
                    AoC2023.solution.AoCDay19 AoCDay192023 = new AoC2023.solution.AoCDay19(selectedPart, input);
                    Console.Write(AoCDay192023.output);
                    break;

                case (20, 2023):
                    AoC2023.solution.AoCDay20 AoCDay202023 = new AoC2023.solution.AoCDay20(selectedPart, input);
                    Console.Write(AoCDay202023.output);
                    break;

                case (20, 2019):
                    AoC2019.solution.AoCDay20 AoCDay202019 = new AoC2019.solution.AoCDay20(selectedPart, input);
                    Console.Write(AoCDay202019.output);
                    break;

                case (21, 2023):
                    AoC2023.solution.AoCDay21 AoCDay212023 = new AoC2023.solution.AoCDay21(selectedPart, input);
                    Console.Write(AoCDay212023.output);
                    break;

                case (22, 2023):
                    AoC2023.solution.AoCDay22 AoCDay222023 = new AoC2023.solution.AoCDay22(selectedPart, input);
                    Console.Write(AoCDay222023.output);
                    break;

                case (23, 2023):
                    AoC2023.solution.AoCDay23 AoCDay232023 = new AoC2023.solution.AoCDay23(selectedPart, input);
                    Console.Write(AoCDay232023.output);
                    break;

                case (24, 2023):
                    AoC2023.solution.AoCDay24 AoCDay242023 = new AoC2023.solution.AoCDay24(selectedPart, input);
                    Console.Write(AoCDay242023.output);
                    break;

                case (25, 2023):
                    AoC2023.solution.AoCDay25 AoCDay252023 = new AoC2023.solution.AoCDay25(selectedPart, input);
                    Console.Write(AoCDay252023.output);
                    break;

                default:
                    AoC2023.solution.AoCDay1 AoCDay02023 = new AoC2023.solution.AoCDay1(selectedPart, input);
                    Console.Write(AoCDay02023.output);
                    break;
            }

        }
        
    }
}