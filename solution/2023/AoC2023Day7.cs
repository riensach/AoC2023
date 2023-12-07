using System;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Collections.Immutable;
using static AoC2023.solution.AoCDay7;
using System.Xml.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AoC2023.solution
{
    public class AoCDay7
    {

        public AoCDay7(int selectedPart, string input)
        {
            string[] lines = input.Split(
                new string[] { Environment.NewLine },
                StringSplitOptions.None
            );
            Dictionary<string, int> hands = new Dictionary<string, int>();
            Dictionary<string, int> handsValue = new Dictionary<string, int>();
            Dictionary<string, int> cardScoringTable =
                new Dictionary<string, int>
                {{"1",1},{"2",2},{"3",3},{"4",4},
                {"5",5},{"6",6},{"7",7},{"8",8},{"9",9},
                {"10",10},{"T",11},
                {"J",1},{"Q",13},{"K",14},{"A",15}};

            foreach (string line in lines)
            {
                string[] commands = line.Split(' ');
                hands.Add(commands[0], Int32.Parse(commands[1]));
            }

            List<Hand> handList = new List<Hand>();

            foreach (KeyValuePair<string, int> hand in hands)
            {
                Dictionary<string, int> cardValues = new Dictionary<string, int>();
                foreach (var character in hand.Key)
                {
                    if (cardValues.ContainsKey(character.ToString()))
                    {
                        cardValues[character.ToString()] = cardValues[character.ToString()] + 1;
                    }
                    else
                    {
                        cardValues.Add(character.ToString(), 1);
                    }
                }

                int handType = 0;
                if (cardValues.Count() == 1)
                {
                    // Five of a kind - 10000000
                    handType = 7;
                }
                else if (cardValues.Count() == 2 && (cardValues.First().Value == 4 || cardValues.Last().Value == 4))
                {
                    //Four of a kind - 1000000
                    if(cardValues.First().Key == "J" || cardValues.Last().Key == "J")
                    {
                        handType = 7;
                    } else
                    {
                        handType = 6;
                    }
                        
                }
                else if (cardValues.Count() == 2 && (cardValues.First().Value == 3 || cardValues.Last().Value == 3))
                {
                    //Full House - 100000
                    if (cardValues.First().Key == "J" || cardValues.Last().Key == "J")
                    {
                        handType = 7;
                    } 
                    else
                    {
                        handType = 5;
                    }
                }
                else if (cardValues.Count() == 3 && (cardValues.First().Value == 3 || cardValues.Last().Value == 3 || cardValues.ElementAt(1).Value == 3) && (cardValues.First().Value == 1 || cardValues.Last().Value == 1 || cardValues.ElementAt(1).Value == 1))
                {
                    //Three of a kind - 10000
                    if (cardValues.First().Key == "J" || cardValues.Last().Key == "J" || cardValues.ElementAt(1).Key == "J")
                    {
                        handType = 6;
                    }
                    else
                    {
                        handType = 4;
                    }
                }
                else if (cardValues.Count() == 3 && (cardValues.First().Value == 2 || cardValues.Last().Value == 2 || cardValues.ElementAt(1).Value == 2) && (cardValues.First().Value == 1 || cardValues.Last().Value == 1 || cardValues.ElementAt(1).Value == 1))
                {
                    //Two Pair - 1000
                    if ((cardValues.First().Key == "J" && cardValues.First().Value == 2) || (cardValues.Last().Key == "J" && cardValues.Last().Value == 2) || (cardValues.ElementAt(1).Key == "J" && cardValues.ElementAt(1).Value == 2))
                    {
                        handType = 6;
                    }
                    else if ((cardValues.First().Key == "J" && cardValues.First().Value == 1) || (cardValues.Last().Key == "J" && cardValues.Last().Value == 1) || (cardValues.ElementAt(1).Key == "J" && cardValues.ElementAt(1).Value == 1))
                    {
                        handType = 5;
                    }
                    else
                    {
                        handType = 3;
                    }
                }
                else if (cardValues.Count() == 4)
                {
                    //One Pair - 100
                    if (cardValues.First().Key == "J" || cardValues.Last().Key == "J"|| cardValues.ElementAt(1).Key == "J" || cardValues.ElementAt(2).Key == "J")
                    {
                        handType = 4;
                    }
                    else
                    {
                        handType = 2;
                    }
                }
                else if (cardValues.Count() == 5)
                {
                    //High Card - 1
                    if (cardValues.First().Key == "J" || cardValues.Last().Key == "J" || cardValues.ElementAt(1).Key == "J" || cardValues.ElementAt(2).Key == "J" || cardValues.ElementAt(3).Key == "J")
                    {
                        handType = 2;
                    }
                    else
                    {
                        handType = 1;
                    }
                }

                handList.Add(new Hand()
                {
                    hand = hand.Key,
                    handType = handType,
                    handBid = hand.Value,
                    card1 = cardScoringTable[hand.Key.ElementAt(0).ToString().ToUpper()],
                    card2 = cardScoringTable[hand.Key.ElementAt(1).ToString().ToUpper()],
                    card3 = cardScoringTable[hand.Key.ElementAt(2).ToString().ToUpper()],
                    card4 = cardScoringTable[hand.Key.ElementAt(3).ToString().ToUpper()],
                    card5 = cardScoringTable[hand.Key.ElementAt(4).ToString().ToUpper()]
                });
            }
            foreach (Hand seedItem in handList)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(seedItem))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(seedItem);
                    Console.WriteLine("{0}={1}", name, value);
                }
            }

            var handLists = handList.OrderBy(p => p.handType).ThenBy(s => s.card1).ThenBy(s => s.card2).ThenBy(s => s.card3).ThenBy(s => s.card4).ThenBy(s => s.card5).ToList();
            int i = 1;
            int totalScore = 0;
            foreach (Hand handListItem in handLists)
            {
                Console.WriteLine("Scoring hand " + handListItem.hand + " with the hand score of " + handListItem.handBid + "\n");
                totalScore = totalScore + (handListItem.handBid * i);
                i++;
            }
            output = "Part A: " + totalScore;
            // wrong 254902501
            /*
                        foreach (KeyValuePair<string, int> hand in hands)
                        {

                            Dictionary<string, int> cardValues = new Dictionary<string, int>();
                            foreach (var character in hand.Key)
                            {
                                if (cardValues.ContainsKey(character.ToString()))
                                {
                                    cardValues[character.ToString()] = cardValues[character.ToString()] + 1;
                                }
                                else
                                {
                                    cardValues.Add(character.ToString(), 1);
                                }
                            }

                            int handScore = 0;
                            if (cardValues.Count() == 1)
                            {
                                // Five of a kind - 10000000
                                int index = (((int)char.Parse(cardValues.First().Key.ToUpper())) - 64) * 30;
                                handScore = 10000000 + index;
                                handsValue.Add(hand.Key, handScore);
                            }
                            else if (cardValues.Count() == 2 && (cardValues.First().Value == 4 || cardValues.Last().Value == 4))
                            {
                                //Four of a kind - 1000000
                                int index = 0;
                                int indexTwo = 0;
                                if (cardValues.First().Value == 4)
                                {
                                    index = cardScoringTable[cardValues.First().Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.Last().Key.ToUpper()];
                                }
                                else
                                {
                                    index = cardScoringTable[cardValues.Last().Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.First().Key.ToUpper()];
                                }
                                //handScore = 1000000 + index + indexTwo;
                                handScore = 1000000;
                                handsValue.Add(hand.Key, handScore);
                            }
                            else if (cardValues.Count() == 2 && (cardValues.First().Value == 3 || cardValues.Last().Value == 3))
                            {
                                //Full House - 100000
                                int index = 0;
                                int indexTwo = 0;
                                if (cardValues.First().Value == 3)
                                {
                                    index = cardScoringTable[cardValues.First().Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.Last().Key.ToUpper()];
                                }
                                else
                                {
                                    index = cardScoringTable[cardValues.Last().Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.First().Key.ToUpper()];
                                }
                                //handScore = 100000 + index + indexTwo;
                                handScore = 100000;
                                handsValue.Add(hand.Key, handScore);
                            }
                            else if (cardValues.Count() == 3 && (cardValues.First().Value == 3 || cardValues.Last().Value == 3 || cardValues.ElementAt(1).Value == 3) && (cardValues.First().Value == 1 || cardValues.Last().Value == 1 || cardValues.ElementAt(1).Value == 1))
                            {
                                //Three of a kind - 10000
                                int index = 0;
                                int indexTwo = 0;
                                int indexThree = 0;
                                if (cardValues.First().Value == 3)
                                {
                                    index = cardScoringTable[cardValues.First().Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.Last().Key.ToUpper()];
                                    indexThree = cardScoringTable[cardValues.ElementAt(1).Key.ToUpper()];
                                }
                                else if (cardValues.Last().Value == 3)
                                {
                                    index = cardScoringTable[cardValues.Last().Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.First().Key.ToUpper()];
                                    indexThree = cardScoringTable[cardValues.ElementAt(1).Key.ToUpper()];
                                }
                                else
                                {
                                    index = cardScoringTable[cardValues.ElementAt(1).Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.First().Key.ToUpper()];
                                    indexThree = cardScoringTable[cardValues.Last().Key.ToUpper()];
                                }
                                if (indexTwo > indexThree)
                                {
                                    //handScore = 10000 + index + (indexTwo * 30) + indexThree;
                                    handScore = 10000;
                                }
                                else
                                {
                                    //handScore = 10000 + index + indexTwo + (indexThree * 30);
                                    handScore = 10000;
                                }
                                Console.WriteLine("scores:" + index + ":" + indexTwo + ":" + cardValues.ElementAt(1).Key.ToUpper() + "\n");
                                handsValue.Add(hand.Key, handScore);
                            }
                            else if (cardValues.Count() == 3 && (cardValues.First().Value == 2 || cardValues.Last().Value == 2 || cardValues.ElementAt(1).Value == 2) && (cardValues.First().Value == 1 || cardValues.Last().Value == 1 || cardValues.ElementAt(1).Value == 1))
                            {
                                //Two Pair - 1000
                                int index = 0;
                                int indexTwo = 0;
                                int indexThree = 0;
                                if (cardValues.First().Value == 2 && cardValues.Last().Value == 2)
                                {
                                    index = cardScoringTable[cardValues.First().Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.Last().Key.ToUpper()] * 30;
                                    indexThree = cardScoringTable[cardValues.ElementAt(1).Key.ToUpper()];
                                }
                                else if (cardValues.First().Value == 2)
                                {
                                    index = cardScoringTable[cardValues.First().Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.ElementAt(1).Key.ToUpper()] * 30;
                                    indexThree = cardScoringTable[cardValues.Last().Key.ToUpper()];
                                }
                                else
                                {
                                    index = cardScoringTable[cardValues.Last().Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.ElementAt(1).Key.ToUpper()] * 30;
                                    indexThree = cardScoringTable[cardValues.First().Key.ToUpper()];
                                }
                                //handScore = 1000 + index + indexTwo + indexThree;
                                handScore = 1000;
                                handsValue.Add(hand.Key, handScore);
                            }
                            else if (cardValues.Count() == 4)
                            {
                                //One Pair - 100
                                int index = 0;
                                int indexTwo = 0;
                                int indexThree = 0;
                                int indexFour = 0;
                                if (cardValues.First().Value == 2)
                                {
                                    index = cardScoringTable[cardValues.First().Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.ElementAt(1).Key.ToUpper()];
                                    indexThree = cardScoringTable[cardValues.Last().Key.ToUpper()];
                                    indexFour = cardScoringTable[cardValues.ElementAt(2).Key.ToUpper()];
                                }
                                else if (cardValues.Last().Value == 2)
                                {
                                    index = cardScoringTable[cardValues.Last().Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.ElementAt(1).Key.ToUpper()];
                                    indexThree = cardScoringTable[cardValues.First().Key.ToUpper()];
                                    indexFour = cardScoringTable[cardValues.ElementAt(2).Key.ToUpper()];
                                }
                                else if (cardValues.ElementAt(1).Value == 2)
                                {
                                    index = cardScoringTable[cardValues.ElementAt(1).Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.Last().Key.ToUpper()];
                                    indexThree = cardScoringTable[cardValues.First().Key.ToUpper()];
                                    indexFour = cardScoringTable[cardValues.ElementAt(2).Key.ToUpper()];
                                }
                                else
                                {
                                    index = cardScoringTable[cardValues.ElementAt(2).Key.ToUpper()] * 30;
                                    indexTwo = cardScoringTable[cardValues.Last().Key.ToUpper()];
                                    indexThree = cardScoringTable[cardValues.First().Key.ToUpper()];
                                    indexFour = cardScoringTable[cardValues.ElementAt(1).Key.ToUpper()];
                                }
                                //handScore = 100 + index + indexTwo + indexThree + indexFour;
                                handScore = 100;
                                handsValue.Add(hand.Key, handScore);
                            }
                            else if (cardValues.Count() == 5)
                            {
                                //High Card - 1
                                int index = 0;
                                int indexTwo = 0;
                                int indexThree = 0;
                                int indexFour = 0;
                                int indexFive = 0;
                                index = ((int)char.Parse(cardValues.ElementAt(0).Key.ToUpper())) - 64;
                                indexTwo = ((int)char.Parse(cardValues.ElementAt(1).Key.ToUpper())) - 64;
                                indexThree = ((int)char.Parse(cardValues.ElementAt(2).Key.ToUpper())) - 64;
                                indexFour = ((int)char.Parse(cardValues.ElementAt(3).Key.ToUpper())) - 64;
                                indexFive = ((int)char.Parse(cardValues.ElementAt(4).Key.ToUpper())) - 64;
                                //handScore = 1 + index + indexTwo + indexThree + indexFour + indexFive;
                                handScore = 1;
                                handsValue.Add(hand.Key, handScore);
                            }

                            /*
                            Console.WriteLine("Hand " + hand.Key + " splits into: \n");
                            foreach (KeyValuePair<string, int> seedItem in cardValues)
                            {
                                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(seedItem))
                                {
                                    string name = descriptor.Name;
                                    object value = descriptor.GetValue(seedItem);
                                    Console.WriteLine("{0}={1}", name, value);
                                }
                            }

                        }
                        var sortedKeyValuePairs = handsValue.OrderBy(x => x.Value).ToList();
                         i = 1;
                         totalScore = 0;

                        foreach (KeyValuePair<string, int> sortedKeyValuePair in sortedKeyValuePairs)
                        {
                            Console.WriteLine("Scoring hand " + sortedKeyValuePair.Key + " with the hand score of " + hands[sortedKeyValuePair.Key] + "\n");
                            totalScore = totalScore + (hands[sortedKeyValuePair.Key] * i);
                            i++;
                        }
                */
            //output = "Part A: " + totalScore;
        }

        public class Hand
        {
            public int card1 { get; set; }
            public int card2 { get; set; }
            public int card3 { get; set; }
            public int card4 { get; set; }
            public int card5 { get; set; }
            public int handType { get; set; }
            public int handBid { get; set; }
            public string hand { get; set; }
           
        }

        public string output;
    }
}