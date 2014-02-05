using System;
using System.Collections.Generic;

namespace GameofWar
{
    class Program
    {
        #region Vaariables

        static string player1Name;
        static string player2Name;

        static List<int> deck;

        static List<int> player1Hand = new List<int>();
        static List<int> player1Spoils;
        static List<int> player1Discard;

        static List<int> player2Hand = new List<int>();
        static List<int> player2Spoils;
        static List<int> player2Discard;

        #endregion

        static void Main()
        {
            GetPlayerNames();
            Pause();

            GetDeck();

            DealCards();

            PrintPlayersHands();

            Pause();
        }

        private static void GetPlayerNames()
        {
            Console.WriteLine("Enter a name for player 1");
            player1Name = Console.ReadLine();

            Console.WriteLine("Enter a name for player 2");
            player2Name = Console.ReadLine();

        }

        private static void GetDeck()
        {
            deck = new List<int>();

            for (int i = 0; i < 52; i++)
            {
                deck.Add(i);
            }
        }

        private static void DealCards()
        {
            for (int i = 0; i < 26; i++)
            {
                player1Hand.Add(deck[2 * i]);
                player2Hand.Add(deck[2 * i + 1]);
            }

            deck.Clear();
        }

        private static void PrintCard(int card)
        {
            int rank = card % 13;
            int suit = card / 13;

            PrintRank(rank);

            Console.Write(" of ");

            PrintSuit(suit);

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
                    case 9:
                        Console.Write("J");
                        break;
                    case 10:
                        Console.Write("Q");
                        break;
                    case 11:
                        Console.Write("K");
                        break;
                    case 12:
                        Console.Write("A");
                        break;
                }
            }
        }

        private static void PrintPlayersHands()
        {
            Console.Clear();

            Console.WriteLine("Player 1's hand");

            foreach (int card in player1Hand)
            {
                PrintCard(card);

                Console.WriteLine();
            }

            Pause();

            Console.Clear();

            Console.WriteLine("Player 2's hand");

            foreach (int card in player2Hand)
            {
                PrintCard(card);

                Console.WriteLine();
            }
        }

        private static void Pause()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey();
        }
    }
}
  