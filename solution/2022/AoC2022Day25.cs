using System;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Linq;

namespace AoC2022.solution
{   
    public class AoCDay25
    {
        public long sumNumber = 0;
        public List<long> fuelRequirements = new List<long>();
        public IDictionary<char, int> SNAFU = new Dictionary<char, int>();

        public AoCDay25(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            SNAFU.Add('2', 2);
            SNAFU.Add('1', 1);
            SNAFU.Add('0', 0);
            SNAFU.Add('-', -1);
            SNAFU.Add('=', -2);

            foreach (string line in lines)
            {
                long fuel = SNAFUDecoder(line);
                fuelRequirements.Add(fuel);
            }

            long totalFuel = fuelRequirements.Sum();
            string SNAFUFuel = SNAFUEncoder(totalFuel);

            output = "Part A: Decimal = " + totalFuel +  " SNAFU = " + SNAFUFuel;
        }

        public long SNAFUDecoder(string SNAFUNumberInput)
        {
            long number = 0;
            int inputLength = SNAFUNumberInput.Length;
            int numberPosition = inputLength;
            foreach (var character in SNAFUNumberInput)
            {
                long value = (long)Math.Pow(5, numberPosition-1) * SNAFU[character];
                number = number + value;
                numberPosition--;
            }           
            return number;
        }

        public string SNAFUEncoder(long numberInput)
        {
            string SNAFUNumber = "";
            long value = numberInput;
            while(value > 0)
            {
                long tempValue = value % 5;
                if(tempValue == 0)
                {
                    SNAFUNumber = 0 + SNAFUNumber;
                } else if (tempValue == 1)
                {
                    SNAFUNumber = 1 + SNAFUNumber;
                } else if (tempValue == 2)
                {
                    SNAFUNumber = 2 + SNAFUNumber;
                } else if (tempValue == 3)
                {
                    SNAFUNumber = "=" + SNAFUNumber;
                    value = value + 5;
                } else if (tempValue == 4)
                {
                    SNAFUNumber = "-" + SNAFUNumber;
                    value = value + 5;
                }

                value = value / 5;
            }

            return SNAFUNumber;
        }
        public string output;
    }
}