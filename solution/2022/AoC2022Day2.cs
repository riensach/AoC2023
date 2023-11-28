using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks.Sources;

namespace AoC2022.solution
{
    public class AoCDay2
    {

        public AoCDay2(int selectedPart, string input)
        {

            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            int score = 0;

            foreach (string line in lines)
            {
                string[] moves = line.Split(' ');

                if (moves[0].Equals("A") && moves[1].Equals("X")) // Rock vs Rock
                {
                    score = score + 4;
                }
                else if(moves[0].Equals("A") && moves[1].Equals("Y")) // Rock vs Paper
                {
                    score = score + 8;
                }
                else if(moves[0].Equals("A") && moves[1].Equals("Z")) // Rock vs Sciccors 
                {
                    score = score + 3;
                }
                else if(moves[0].Equals("B") && moves[1].Equals("X")) // Paper vs Rock
                {
                    score = score + 1;
                }
                else if(moves[0].Equals("B") && moves[1].Equals("Y")) // Paper vs Paper
                {
                    score = score + 5;
                }
                else if(moves[0].Equals("B") && moves[1].Equals("Z")) // Paper vs Sciccors 
                {
                    score = score + 9;
                }
                else if(moves[0].Equals("C") && moves[1].Equals("X")) // Sciccors vs Rock
                {
                    score = score + 7;
                }
                else if(moves[0].Equals("C") && moves[1].Equals("Y")) // Sciccors vs Paper
                {
                    score = score + 2;
                }
                else if (moves[0].Equals("C") && moves[1].Equals("Z")) // Sciccors vs Sciccors 
                {
                    score = score + 6;
                }                

            }

            output = "Part A: " + score;

            score = 0;
            foreach (string line in lines)
            {
                string[] moves = line.Split(' ');

                if (moves[0].Equals("A") && moves[1].Equals("X")) // Rock vs Rock (must lose)
                {
                    score = score + 3;
                }
                else if (moves[0].Equals("A") && moves[1].Equals("Y")) // Rock vs Paper (must draw)
                {
                    score = score + 4;
                }
                else if (moves[0].Equals("A") && moves[1].Equals("Z")) // Rock vs Sciccors (must win)
                {
                    score = score + 8;
                }
                else if (moves[0].Equals("B") && moves[1].Equals("X")) // Paper vs Rock (must lose)
                {
                    score = score + 1;
                }
                else if (moves[0].Equals("B") && moves[1].Equals("Y")) // Paper vs Paper (must draw)
                {
                    score = score + 5;
                }
                else if (moves[0].Equals("B") && moves[1].Equals("Z")) // Paper vs Sciccors (must win) 
                {
                    score = score + 9;
                }
                else if (moves[0].Equals("C") && moves[1].Equals("X")) // Sciccors vs Rock (must lose)
                {
                    score = score + 2;
                }
                else if (moves[0].Equals("C") && moves[1].Equals("Y")) // Sciccors vs Paper (must draw)
                {
                    score = score + 6;
                }
                else if (moves[0].Equals("C") && moves[1].Equals("Z")) // Sciccors vs Sciccors (must win)
                {
                    score = score + 7;
                }

            }

            output += "\nPart B: " + score;

        }

        public string output;
    }
}