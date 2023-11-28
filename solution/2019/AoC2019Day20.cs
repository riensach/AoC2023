using System;
using System.IO;
using System.Diagnostics;
using LoreSoft.MathExpressions;
using System.Threading;

namespace AoC2019.solution
{
    public class AoCDay20
    {
        public List<string> cubes = new List<string>();
        public int maximumX = 0;
        public int maximumY = 0;
        public int maximumZ = 0;
        public int minimumX = 1000;
        public int minimumY = 1000;
        public int minimumZ = 1000;
        public AoCDay20(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

  


            foreach (string cube in cubes)
            {

            }

            int exposedExternalSurfaces = 0;
            output += "\nPart B: " + exposedExternalSurfaces;



        }

        public string output;
    }
}