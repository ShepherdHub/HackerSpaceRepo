using System;
using System.Collections.Generic;

namespace GameOfWar
{
    class Program
    {
        // The "#region" is a preproccessor command that lets VS know that you have information for it
        // but not the program. Use this to group code together so that you can fold it when you don't want
        // it cluttering your screen
        #region Variables

        static string player1Name = "player 1";
        static string player2Name = "player 2";

        static Random randNum = new Random();

        static Boolean gameIsRunning = true;
        static int player1Score = 0;
        static int player2Score = 0;

        static List<int> deck = new List<int>();

        static List<int> player1Hand = new List<int>();
        static List<int> player1Spoils = new List<int>();
        static List<int> player1Discard = new List<int>();

        static List<int> player2Hand = new List<int>();
        static List<int> player2Spoils = new List<int>();
        static List<int> player2Discard = new List<int>();

        #endregion
        static void Main(string[] args)
        {
            GetPlayerNames();
            //Pause();

            GetDeck();
            Shuffle(deck);

            DealCards();

            //PrintPlayersHands();

            do
            {
                Skirmish();
                CheckVictory();
            } while (gameIsRunning);

            PrintWinner();

            Pause();

        }

        private static void PrintWinner()
        {
            if (player1Score != 0 && player2Score == 0)
            {
                Console.WriteLine("Congratulations! " + player1Name + " was victorious!");
            }
            else if (player1Score == 0 && player2Score != 0)
            {
                Console.WriteLine("Congratulations! " + player2Name + " was victorious!");
            }
            else if (player1Score == 0 && player2Score == 0)
            {
                Console.WriteLine("The game ends in a tie!");
            }
        }

        private static void Skirmish()
        {
            Console.WriteLine("A skirmish begins!");
            
            Rehand(1);
            AddCardsToSpoils(1);

            int player1ActiveSpoil = player1Spoils[player1Spoils.Count - 1];
            int player2ActiveSpoil = player2Spoils[player2Spoils.Count - 1];
            
            //Print cards added to spoils
            Console.Write(player1Name+" plays ");
            PrintCard(player1ActiveSpoil);
            Console.WriteLine();
            Console.Write(player2Name + " plays ");
            PrintCard(player2ActiveSpoil);
            Console.WriteLine();
            

            //evaluate outcome

            if ((player1ActiveSpoil%13) > (player2ActiveSpoil%13))
            {
                //add spoils to player 1 discard, clear spoils
                AddSpoilsToDiscard(player1Discard);

                EvaluateScores();

                //display winner and scores
                Console.WriteLine(player1Name + " won the skirmish.");
                Console.WriteLine("Current Score:");
                Console.WriteLine(player1Name + ": " + player1Score + "\t\t" + player2Name + ": " + player2Score);

            }
            else if ((player1ActiveSpoil%13) < (player2ActiveSpoil%13))
            {
                //add spoils to player 2 discard, clear spoils
                AddSpoilsToDiscard(player2Discard);

                EvaluateScores();

                //display winner and scores
                Console.WriteLine(player2Name + " won the skirmish.");
                Console.WriteLine("Current Score:");
                Console.WriteLine(player1Name + ": " + player1Score + "\t\t" + player2Name + ": " + player2Score);
            }
            else
            {
                // TODO implement Battle logic
                Battle();
            }

        }

        private static void Battle()
        {
            Console.WriteLine("A battle has occured!");
            
            //check for dead man's hand
            if (!isDeadMansHand())
            {
                int bounty = GetBountySize();
                Rehand(bounty);
                AddCardsToSpoils(bounty);
                Skirmish();
            }

            EvaluateScores();
        }

        private static int GetBountySize()
        {
            int bountySize = 3;
            int p1CardsLeft = player1Hand.Count + player1Discard.Count;
            int p2CardsLeft = player2Hand.Count + player2Discard.Count;

            if (p1CardsLeft < 4 || p2CardsLeft < 4)
            {
                if (p1CardsLeft < p2CardsLeft) bountySize = p1CardsLeft - 1;
                else bountySize = p2CardsLeft - 1;
            }

            Console.WriteLine(bountySize + " cards were added to the spoils");

            return bountySize;
        }

        private static Boolean isDeadMansHand()
        {
            int p1CardsLeft = player1Hand.Count + player1Discard.Count;
            int p2CardsLeft = player2Hand.Count + player2Discard.Count;

            if (p1CardsLeft != 0 && p2CardsLeft == 0)
            {
                Console.WriteLine(player2Name + " drew a Dead Man's Hand!");
                AddSpoilsToDiscard(player1Discard);
                EvaluateScores();
                return true;
            }
            
            if (p1CardsLeft == 0 && p2CardsLeft != 0)
            {
                Console.WriteLine(player1Name + " drew a Dead Man's Hand!");
                AddSpoilsToDiscard(player2Discard);
                EvaluateScores();
                return true;
            }

            if (p1CardsLeft == 0 && p2CardsLeft == 0)
            {
                Console.WriteLine("There was a Double Dead Man's Hand!");
                player1Score = 0;
                player2Score = 0;
                return true;
            }

            return false;
        }

        private static void CheckVictory()
        {
            if(player1Score == 0 || player2Score == 0) gameIsRunning = false;
		}
    

        private static void EvaluateScores()
        {
            player1Score = player1Hand.Count + player1Discard.Count + player1Spoils.Count;
            player2Score = player2Hand.Count + player2Discard.Count + player2Spoils.Count;
        }



        private static void AddSpoilsToDiscard(List<int> discard)
        {
            foreach (int card in player1Spoils)
            {
                discard.Add(card);
            }
            player1Spoils.Clear();
            foreach (int card in player2Spoils)
            {
                discard.Add(card);
            }
            player2Spoils.Clear();
        }

        private static void AddCardsToSpoils(int numCards)
        {
            for (int i = 0; i < numCards; i++)
            {
                player1Spoils.Add(player1Hand[0]);
                player1Hand.RemoveAt(0);
            }

            for (int i = 0; i < numCards; i++)
            {
                player2Spoils.Add(player2Hand[0]);
                player2Hand.RemoveAt(0);
            }
        }

        private static void Rehand(int numCards)
        {
            if (player1Hand.Count < numCards)
            {
                Shuffle(player1Discard);
                foreach (int card in player1Discard)
                {
                    player1Hand.Add(card);
                }
                player1Discard.Clear();
                Console.WriteLine(player1Name + " rehand occured.");
            }

            if (player2Hand.Count < numCards)
            {
                Shuffle(player2Discard);
                foreach (int card in player2Discard)
                {
                    player2Hand.Add(card);
                }
                player2Discard.Clear();
                Console.WriteLine(player2Name + " rehand occured.");
            }
        }


        private static void GetPlayerNames() 
        {
            Console.WriteLine("Enter a name for player 1");
            player1Name = Console.ReadLine();

            if (player1Name.ToLower().Equals("developer"))
            {
                //enter developer mode
            }

            Console.WriteLine("Enter a name for player 2");
            player2Name = Console.ReadLine();
        }

        private static void GetDeck()
        {
            for (int i = 0; i < 52; i++)
            {
                deck.Add(i);
            }
        }

        private static void Shuffle(List<int> deckToShuffle)
        {
            for(int i = 0; i < deckToShuffle.Count; i++)
            {
                Swap(i, randNum.Next(i,deckToShuffle.Count), deckToShuffle);
            }

        }

        private static void Swap(int a, int b, List<int> switchDeck)
        {
            int temp = switchDeck[a];
            switchDeck[a] = switchDeck[b];
            switchDeck[b] = temp;
        }

        private static void DealCards()
        {
            int startHandSize = deck.Count/ 2;

            for (int i = 0; i < startHandSize; i++)
            {
                player1Hand.Add(deck[2 * i]);
                player2Hand.Add(deck[2 * i + 1]);
            }

            deck.Clear();
        }

        private static void PrintPlayersHands()
        {
            Console.Clear();
            Console.WriteLine(player1Name+"'s Hand:");

            foreach (int card in player1Hand)
            {
                PrintCard(card);
                Console.Write("\n");
            }

            Pause();

            Console.Clear();
            Console.WriteLine(player2Name + "'s Hand:");

            foreach (int card in player2Hand)
            {
                PrintCard(card);
                Console.Write("\n");
            }
        }

        private static void PrintCard(int card)
        {
            int rank = card % 13;
            int suit = card / 13;

            PrintRank(rank);
            Console.Write(" of ");
            PrintSuit(suit);
        }

        private static void PrintRank(int rank)
        {
            if (rank <= 8)
            {
                Console.Write(rank + 2);
            }
            else
            {
                switch (rank)
                {
                    case (9):
                        Console.Write("Jack");
                        break;
                    case (10):
                        Console.Write("Queen");
                        break;
                    case (11):
                        Console.Write("King");
                        break;
                    case (12):
                        Console.Write("Ace");
                        break;
                }

            }
        }
        
        private static void PrintSuit(int suit)
        {
            switch (suit)
            {
                case 0:
                    Console.Write("Spades");
                    break;
                case 1:
                    Console.Write("Hearts");
                    break;
                case 2:
                    Console.Write("Clubs");
                    break;
                case 3:
                    Console.Write("Diamonds");
                    break;
            }
        }
        
        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
