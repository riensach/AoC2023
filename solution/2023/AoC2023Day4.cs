using System;
using System.IO;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;

namespace AoC2023.solution
{
    public class AoCDay4
    {

        public AoCDay4(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );

            int totalWinningPoints = 0;
            Dictionary<int, int> totalCardGroups = new Dictionary<int, int>();
            foreach (string line in lines)
            {
                string[] games = line.Split(':');
                string gameName = games[0].Replace("Card ", "");
                totalCardGroups.Add(Int32.Parse(gameName), 1);
                string cardSections = games[1].Trim();
                string[] sections = cardSections.Split(" | ");
                List<int> winningNumbers = new List<int>();
                List<int> myCards = new List<int>();
                
                string[] winningNumberCards = sections[0].Split(' ');
                foreach (string card in winningNumberCards)
                {
                    string cardNumbers = card.Trim();
                    if(cardNumbers != "")
                    {
                        //Console.WriteLine("Card:"+cardNumbers+"\n");
                        winningNumbers.Add(Int32.Parse(cardNumbers));
                    }
                }
                string[] myCardsList = sections[1].Split(' ');
                int winningCards = 0;
                int winningCardsPoints = 0;
                foreach (string card in myCardsList)
                {
                    string cardNumbers = card.Trim();
                    if (cardNumbers != "")
                    {
                        //Console.WriteLine("Card:" + cardNumbers + "\n");

                        myCards.Add(Int32.Parse(cardNumbers));
                        if(winningNumbers.Contains(Int32.Parse(cardNumbers)))
                        {
                            winningCards++;
                            if(winningCards > 1)
                            {
                                winningCardsPoints = winningCardsPoints * 2;
                            } else
                            {
                                winningCardsPoints++;
                            }
                            //Console.WriteLine("Winning Number:" + cardNumbers + " for card "+ gameName+"\n");
                        }

                    }
                }
                totalWinningPoints = totalWinningPoints + winningCardsPoints;


            }
            output = "Part A: " + totalWinningPoints;




            
            int totalCards = 0;
            foreach (string line in lines)
            {
                string[] games = line.Split(':');
                string gameName = games[0].Replace("Card ", "");
                string cardSections = games[1].Trim();
                string[] sections = cardSections.Split(" | ");
                List<int> winningNumbers = new List<int>();
                List<int> myCards = new List<int>();

                string[] winningNumberCards = sections[0].Split(' ');
                foreach (string card in winningNumberCards)
                {
                    string cardNumbers = card.Trim();
                    if (cardNumbers != "")
                    {
                        //Console.WriteLine("Card:"+cardNumbers+"\n");
                        winningNumbers.Add(Int32.Parse(cardNumbers));
                    }
                }
                string[] myCardsList = sections[1].Split(' ');
                int winningCards = 0;
                int winningCardsPoints = 0;
                foreach (string card in myCardsList)
                {
                    string cardNumbers = card.Trim();
                    if (cardNumbers != "")
                    {
                        //Console.WriteLine("Card:" + cardNumbers + "\n");

                        myCards.Add(Int32.Parse(cardNumbers));
                        if (winningNumbers.Contains(Int32.Parse(cardNumbers)))
                        {
                            winningCards++;

                            //Console.WriteLine("Winning Number:" + cardNumbers + " for card "+ gameName+"\n");
                        }

                    }
                }
                for (int i = 1; i <= winningCards; i++)
                {
                    int newName = Int32.Parse(gameName) + i;
                    totalCardGroups[newName] = (totalCardGroups[newName] + (1 * totalCardGroups[Int32.Parse(gameName)]));
                    //Console.WriteLine("Adding a winning card to #:" + newName + " from game " + gameName + "\n");
                }


            }

            foreach (KeyValuePair<int, int> cards in totalCardGroups)
            {
                //Console.WriteLine("Card #:" + cards.Key + " total cards " + cards.Value + "\n");
                totalCards = totalCards + cards.Value;
            }

                output += "\nPart B: " + totalCards;

            /*
            int fullyContained = 0;
            int overlapped = 0;

            foreach (string line in lines)
            {
                string[] eitherSide = line.Split(',');
                string[] firstItems = eitherSide[0].Split('-');
                string[] secondItems = eitherSide[1].Split('-');

                List<int> firstItemsList = new List<int>();
                List<int> secondItemsList = new List<int>();

                for (int i = Int32.Parse(firstItems[0]); i <= Int32.Parse(firstItems[1]); i++)
                {
                    firstItemsList.Add(i);
                }

                for (int i = Int32.Parse(secondItems[0]); i <= Int32.Parse(secondItems[1]); i++)
                {
                    secondItemsList.Add(i);
                }

                if((firstItemsList.First() >= secondItemsList.First() && firstItemsList.Last() <= secondItemsList.Last()) || (secondItemsList.First() >= firstItemsList.First() && secondItemsList.Last() <= firstItemsList.Last()))
                {
                    fullyContained++;
                }

                if ((firstItemsList.First() >= secondItemsList.First() && firstItemsList.First() <= secondItemsList.Last()) || (secondItemsList.First() >= firstItemsList.First() && secondItemsList.First() <= firstItemsList.Last()))
                {
                    overlapped++;
                }

            }
            */

            //output = "Part A: " + fullyContained;
            //output += "\nPart B: " + overlapped;
        }

        public string output;
    }
}