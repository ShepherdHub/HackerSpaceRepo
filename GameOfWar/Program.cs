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
            //GetPlayerNames();
            Pause();

            GetDeck();
            ShuffleDeck();

            DealCards();

            PrintPlayersHands();

            Pause();

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

        private static void ShuffleDeck()
        {
            for(int i = 0; i < deck.Count; i++)
            {
                Swap(i, randNum.Next(i,deck.Count), deck);
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
